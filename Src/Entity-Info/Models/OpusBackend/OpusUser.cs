using EntityInfoService.DAL.CassandraDB;
using EntityInfoService.DAL.MySql;

namespace EntityInfoService.Models.OpusBackend
{
    public class OpusUser
    {
        public long UserId { get; set; } = 0;
        public string CrmAccountId { get; set; } = string.Empty;

        #region MySql Databases
        public UserDB? UserDB { get; set; } = null;
        public AuthenticationDB? AuthenticationDB { get; set; } = null;

        public EntitlementDB? EntitlementDB { get; set; } = null;

        public OneUxDB? OneUxDB { get; set; } = null;

        public PurchaseDB? PurchaseDB { get; set; } = null;

        public TelusBillingDB? TelusBillingDB { get; set; } = null;

        public NpvrbeDB? NpvrbeDB { get; set; } = null;

        public TelusMediaroomRecordingDB? TelusMediaroomRecordingDB { get; set; } = null;

        #endregion

        #region Cassandra Keyspaces
        public AvsBookmarkKeyspace? AvsBookmarkKeyspace { get; set; } = null;

        public AvsConcurrentStreamsKeyspace? AvsConcurrentStreamsKeyspace { get; set; } = null;

        public AvsUserPasswordHisotryKeyspace? AvsUserPasswordHisotryKeyspace { get; set; } = null;

        public TokenManagementKeyspace? TokenManagementKeyspace { get; set; } = null;

        public UserEntitlementKeyspace? UserEntitlementKeyspace { get; set; } = null;

        public UserKeyspace? UserKeyspace { get; set; } = null;
        #endregion
    }
}
