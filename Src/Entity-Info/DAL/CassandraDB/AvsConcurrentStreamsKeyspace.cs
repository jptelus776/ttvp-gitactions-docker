using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.AvsConcurrentStreams;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class AvsConcurrentStreamsKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "avs_concurrent_streams";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(AvsConcurrentStreamsKeyspace));

        public List<SubscriberStreamUsage> SubscriberStreamUsages { get; set; } = new List<SubscriberStreamUsage>();

        public static DbResultList<SubscriberStreamUsage> GetSubscriberStreamUsages(string crmAccountId)
        {
            var result = new DbResultList<SubscriberStreamUsage>();
            string tableName = "subscriberstreamusage";
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
                    var query = "SELECT * FROM " + tableName + " where crmaccountid=?";
                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(crmAccountId);

                    _logger.Information(@"Executing {query} with crmAccountId={crmAccountId}", query, crmAccountId);
                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var ssu = new SubscriberStreamUsage();
                        ssu.CrmAccountId = row.GetValue<string>("crmaccountid");
                        ssu.StreamSessionId = row.GetValue<Guid>("streamsessionid");
                        ssu.Property = row.GetValue<string>("property");
                        ssu.SessionInfo = row.GetValue<Dictionary<string, string>>("sessioninfo");
                        ssu.UserName = row.GetValue<string>("username");
                        result.Records.Add(ssu);
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
            return CassandraHelper.GetSchemaVersions(_session, "avs_concurrent_streams", "avs_version", "telus_db_version");
        }
    }
}
