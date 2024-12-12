using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.WebSocketService;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class WebSocketServiceKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "web_socket_service";
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(WebSocketServiceKeyspace));

        public static WssToken? GetWssToken(string connctionId, string platform)
        {
            WssToken? ws = null;
            string tableName = "wss_tokens";
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
                    var query = "SELECT * FROM " + tableName + " where connectionid=? and platform=?";

                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(connctionId, platform);

                    var rows = _session.Execute(boundStmt);
                    if (rows.Count() > 0)
                    {
                        var row = rows.FirstOrDefault();
                        if (row != null)
                        {
                            ws = new WssToken();
                            ws.ConnectionId = row.GetValue<string>("connectionid");
                            ws.Platform = row.GetValue<string>("platform");
                            ws.Token = row.GetValue<string>("wss_token");
                        }
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

            return ws;
        }

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return CassandraHelper.GetSchemaVersions(_session, "web_socket_service", "avs_version");
        }
    }
}
