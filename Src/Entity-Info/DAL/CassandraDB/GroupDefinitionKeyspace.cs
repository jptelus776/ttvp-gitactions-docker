using EntityInfoService.Models.OpusBackend;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class GroupDefinitionKeyspace
    {
        public static Cassandra.ISession? _session;
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(GroupDefinitionKeyspace));

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return CassandraHelper.GetSchemaVersions(_session, "avs_bookmark", "avs_version");
        }
    }
}
