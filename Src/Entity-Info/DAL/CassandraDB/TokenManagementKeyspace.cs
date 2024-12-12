using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.TokenManagement;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class TokenManagementKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "token_management";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(TokenManagementKeyspace));
        public List<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();

        public static DbResultList<UserRefreshToken> GetUserRefreshTokens(long userId)
        {
            var result = new DbResultList<UserRefreshToken>();
            string tableName = "user_refresh_token";
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
                    var boundStmt = prepStmt.Bind(userId);

                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var urt = new UserRefreshToken();
                        urt.ClientId = row.GetValue<string>("clientid");
                        urt.Scope = row.GetValue<string>("scope");
                        urt.UserId = row.GetValue<long>("user_id");
                        urt.DeviceId = row.GetValue<string>("device_id");
                        urt.Platform = row.GetValue<string>("platform");
                        urt.AccessToken = row.IsNull("access_token") ? null : row.GetValue<string>("access_token");
                        urt.RefreshToken = row.IsNull("refresh_token") ? null : row.GetValue<string>("refresh_token");
                        result.Records.Add(urt);
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
            return CassandraHelper.GetSchemaVersions(_session, "token_management", "avs_version");
        }
    }
}
