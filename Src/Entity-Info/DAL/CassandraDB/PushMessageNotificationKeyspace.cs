using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.PushMessageNotification;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class PushMessageNotificationKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "push_message_notification";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(PushMessageNotificationKeyspace));
        public List<FailedMessage> FailedMessages { get; set; } = new List<FailedMessage>();

        public static DbResultList<FailedMessage> GetFailedMessages(long deviceId)
        {
            var result = new DbResultList<FailedMessage>();
            string tableName = "failed_message";
            try
            {
                var cluster = CassandraHelper.Cluster;

                if (cluster != null && _session == null)
                {
                    _session = cluster.Connect(_schemaName);
                    if (_session == null)
                    {
                        _logger.Error(@"Not able to connect to schema {_schemaName}", _schemaName);
                        result.Errors.Add($"Not able to connect to schema {_schemaName}");
                        return result;
                    }

                    _logger.Information(@"Connected to {_schemaName} keyspace.", _schemaName);
                }
                else
                {
                    _logger.Information(@"Reusing exisitng connection to {_schemaName}", _schemaName);
                }

                if (_session != null)
                {
                    var query = "SELECT * FROM " + tableName + " where user_id=?";
                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(deviceId);

                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var fm = new FailedMessage();
                        fm.Id = row.GetValue<Guid>("id");
                        fm.ConnectionId = row.GetValue<string>("connectionid");
                        fm.Platform = row.GetValue<string>("platform");
                        fm.DeviceId = row.GetValue<string>("deviceid");
                        fm.MessageId = row.GetValue<long>("messageid");
                        fm.TriggerName = row.GetValue<string>("triggername");
                        result.Records.Add(fm);
                    }
                }
            }
            catch (QueryExecutionException ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                CassandraHelper.ProcessException(result, ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                CassandraHelper.ProcessException(result, ex);
            }

            return result;
        }

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return CassandraHelper.GetSchemaVersions(_session, "push_message_notification", "avs_version");
        }
    }
}
