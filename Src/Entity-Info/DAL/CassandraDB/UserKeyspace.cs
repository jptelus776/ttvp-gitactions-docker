using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.User;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class UserKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "user";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(UserKeyspace));
        public List<Workflow> Workflows { get; set; } = new List<Workflow>();

        public static DbResultList<Workflow> GetWorkflows(string crmAccountId)
        {
            var result = new DbResultList<Workflow>();
            string tableName = "workflow";
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
                    var query = "SELECT * FROM " + tableName + " where username=?";
                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(crmAccountId);

                    var rows = _session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var wf = new Workflow();
                        wf.UserName = row.GetValue<string>("username");
                        wf.JobId = row.GetValue<string>("job_id");
                        wf.CreatedAt = row.GetValue<DateTime>("created_at");
                        wf.JobStatus = row.GetValue<string>("job_status");
                        wf.JobType = row.GetValue<string>("job_type");
                        wf.PubSubMessage = row.GetValue<string>("pubsub_message");
                        result.Records.Add(wf);
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
            return CassandraHelper.GetSchemaVersions(_session, "user", "avs_version", "telus_db_version");
        }
    }
}
