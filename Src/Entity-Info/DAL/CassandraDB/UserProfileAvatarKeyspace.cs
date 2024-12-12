using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.UserProfileAvatar;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class UserProfileAvatarKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "user_profile_avatar";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(UserProfileAvatarKeyspace));

        public static Avatar? GetAvatar(string id)
        {
            Avatar? avtr = null;
            string tableName = "avatar";
            try
            {
                var cluster = CassandraHelper.Cluster;

                if (cluster != null && _session == null)
                {
                    _session = cluster.Connect(_schemaName);
                    if (_session == null)
                    {
                        _logger.Error(@"Not able to connect to schema {_schemaName}", _schemaName);
                        return null;
                    }

                    _logger.Information(@"Connected to {_schemaName} keyspace.", _schemaName);
                }
                else
                {
                    _logger.Information(@"Reusing exisitng connection to {_schemaName}", _schemaName);
                }

                if (_session != null)
                {
                    var query = "SELECT * FROM " + tableName + " where id=?";
                    _session.UserDefinedTypes.Define(UdtMap.For<avatar_display_image_json>());

                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(id);

                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        avtr = new Avatar();
                        avtr.Id = row.GetValue<string>("id");
                        avtr.Category = row.GetValue<string>("category");
                        avtr.CreatedDate = row.GetValue<DateTime>("created_date");
                        avtr.DisplayImage = row.GetValue<List<avatar_display_image_json>>("display_image");
                        avtr.DisplayName = row.GetValue<string>("display_name");
                        avtr.LastUpdatedDate = row.GetValue<DateTime>("last_updated_date");
                        avtr.Status = row.GetValue<string>("status");
                        avtr.Type = row.GetValue<string>("type");
                        break;
                    }
                }
            }
            catch (QueryExecutionException ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
            }

            return avtr;
        }

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return CassandraHelper.GetSchemaVersions(_session, "user_profile_avatar", "telus_db_version");
        }
    }
}
