using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.UserEntitlement;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class UserEntitlementKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "user_entitlement";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(UserEntitlementKeyspace));
        public List<UserEntitlementProfile> UserEntitlementProfile { get; set; } = new List<UserEntitlementProfile>();

        public static DbResultList<UserEntitlementProfile> GetUserEntitlementProfiles(long userId)
        {
            var result = new DbResultList<UserEntitlementProfile>();
            string tableName = "user_entitlement_profile";

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
                    _session.UserDefinedTypes.Define(UdtMap.For<ent_prof_json>());

                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(userId);

                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var uep = new UserEntitlementProfile();

                        uep.UserId = row.GetValue<long>("user_id");
                        uep.EntitlementProfileJson = row.GetValue<IEnumerable<ent_prof_json>>("entitlement_profile_json");
                        uep.CreatedDate = row.GetValue<DateTime>("created_date");
                        uep.UpdatedDate = row.GetValue<DateTime>("updated_date");

                        result.Records.Add(uep);
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
            return CassandraHelper.GetSchemaVersions(_session, "user_entitlement", "telus_db_version");
        }
    }
}
