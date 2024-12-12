using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.AvsUserPasswordHistory;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class AvsUserPasswordHisotryKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "avs_user_password_history";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(AvsUserPasswordHisotryKeyspace));
        public List<UserPasswordHistry> UserPasswordHistory { get; set; } = new List<UserPasswordHistry>();

        public static DbResultList<UserPasswordHistry> GetUserPasswordHistory(long userId)
        {
            var result = new DbResultList<UserPasswordHistry>();
            string tableName = "user_password_history";

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
                    var query = "SELECT * FROM " + tableName + " where userid=?";

                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(userId);

                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var uph = new UserPasswordHistry();
                        uph.UserId = row.GetValue<long>("userid");
                        uph.CreateDate = row.GetValue<DateTime>("createdate");
                        uph.Credential = row.GetValue<string>("credential");
                        uph.EncryptionAlgorithm = row.GetValue<string>("encryption_algorithm");
                        uph.SaltKey = row.GetValue<string>("saltkey");
                        result.Records.Add(uph);
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
            return CassandraHelper.GetSchemaVersions(_session, "avs_user_password_history", "avs_version");
        }
    }
}
