using EntityInfoService.DAL.CassandraDB;
using EntityInfoService.DAL.MySql;
using EntityInfoService.Models;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.User;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;

namespace EntityInfoService.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(UserController));

        [HttpGet("{userId}")]
        public IActionResult GetUser([FromRoute] string userId, [FromQuery] string dbNames = "all", [FromQuery] string idType = "uid")
        {
            bool result = long.TryParse(userId, out var uid);

            if (dbNames.ToLower().StartsWith("all")) dbNames = "all";
            var table = "all";
            var databases = dbNames.ToLower().Split(',');
            var opusUser = new OpusUser();
            DbResultList<CrmAccount>? crmAccount = null;
            DbResultList<AccountUser>? accountUser = null;
            var model = new ResponseModel();
            model.Result = opusUser;

            Stopwatch sw = Stopwatch.StartNew();

            switch (idType.ToLower())
            {
                case "uid":
                    {
                        if (result)
                        {
                            opusUser.UserId = uid;
                            _logger.Information("Retrieving crmAccount based on user_id : {user_id}", userId);
                            accountUser = UserDB.GetAccountUser(uid);
                            if (accountUser.Records.Count > 0)
                            {
                                crmAccount = UserDB.GetCrmAccount(uid);
                                if (crmAccount.Records.Count > 0)
                                {
                                    opusUser.CrmAccountId = crmAccount.Records[0].CrmAccountId;
                                }
                            }
                        }
                        else
                        {
                            var error = new ErrorResponseModel(400, string.Empty, "UserId must be numeric or pass the idType=cid", string.Empty);
                            return BadRequest(error);
                        }
                    }
                    break;
                case "cid":
                    {
                        opusUser.CrmAccountId = userId;
                        _logger.Information("Retrieving crmAccount based on crm_account_id : {crm_account_id}", userId);
                        crmAccount = UserDB.GetCrmAccount(opusUser.CrmAccountId);
                        if (crmAccount.Records.Count > 0)
                        {
                            accountUser = UserDB.GetAccountUser(crmAccount.Records[0].UserId);
                            if (accountUser.Records.Count > 0)
                            {
                                opusUser.UserId = uid = crmAccount.Records[0].UserId;
                            }
                        }
                    }
                    break;
                default:
                    {
                        var error = new ErrorResponseModel(400, string.Empty, "Invalid user id type. Valid values are `uid` or `cid`", string.Empty);
                        return BadRequest(error);
                    }
            }

            foreach (var db in databases)
            {
                #region MySQL schemas
                // User Database
                if (db.Equals("all") || db.Equals("user"))
                {
                    if (opusUser.UserDB == null) opusUser.UserDB = new UserDB();

                    if (accountUser != null && (table.Equals("all") || table.Equals("account_user")))
                    {
                        opusUser.UserDB.AccountUser = accountUser.Records;
                        ProcessDbResult(model, accountUser.Errors, accountUser.Summary, accountUser.Exception);
                    }

                    if (crmAccount != null && (table.Equals("all") || table.Equals("crm_account")))
                    {
                        opusUser.UserDB.CrmAccount = crmAccount.Records;
                        ProcessDbResult(model, crmAccount.Errors, crmAccount.Summary, crmAccount.Exception);

                        if (crmAccount.Records.Count > 1)
                        {
                            model.Errors.Add($"Multiple CrmAccountId found for userid : {uid}");
                        }
                    }

                    if (table.Equals("all") || table.Equals("account_region"))
                    {
                        var accountRegion = UserDB.GetAccountRegion(uid);
                        opusUser.UserDB.AccountRegion = accountRegion.Records;
                        ProcessDbResult(model, accountRegion.Errors, accountRegion.Summary, accountRegion.Exception);
                    }

                    if (table.Equals("all") || table.Equals("account_attribute"))
                    {
                        var accountAttributes = UserDB.GetAccountAttributes(uid);
                        opusUser.UserDB.AccountAttributes = accountAttributes.Records;
                        ProcessDbResult(model, accountAttributes.Errors, accountAttributes.Summary, accountAttributes.Exception);
                    }

                    if (table.Equals("all") || table.Equals("account_user_profile"))
                    {
                        var accountUserProfiles = UserDB.GetAccountUserProfiles(uid);
                        opusUser.UserDB.AccountUserProfiles = accountUserProfiles.Records;
                        ProcessDbResult(model, accountUserProfiles.Errors, accountUserProfiles.Summary, accountUserProfiles.Exception);
                    }

                    if (table.Equals("all") || table.Equals("user_ticket"))
                    {
                        var userTickets = UserDB.GetUserTickets(uid);
                        opusUser.UserDB.UserTickets = userTickets.Records;
                        ProcessDbResult(model, userTickets.Errors, userTickets.Summary, userTickets.Exception);
                    }

                    if (table.Equals("all") || table.Equals("reserve_account_cancel"))
                    {
                        var reserveAccountCancel = UserDB.GetReserveAccountCancel(uid);
                        opusUser.UserDB.ReserveAccountCancel = reserveAccountCancel.Records;
                        ProcessDbResult(model, reserveAccountCancel.Errors, reserveAccountCancel.Summary, reserveAccountCancel.Exception);
                    }
                }

                // Authentication Database
                if (db.Equals("all") || db.Equals("authentication"))
                {
                    if (opusUser.AuthenticationDB == null) opusUser.AuthenticationDB = new AuthenticationDB();

                    if (table.Equals("all") || table.Equals("account_credential"))
                    {
                        var accountCredential = AuthenticationDB.GetAccountCredential(uid);
                        opusUser.AuthenticationDB.AccountCredential = accountCredential.Records;
                        ProcessDbResult(model, accountCredential.Errors, accountCredential.Summary, accountCredential.Exception);
                    }

                    if (table.Equals("all") || table.Equals("account_device"))
                    {
                        var accountDevices = AuthenticationDB.GetAccountDevices(uid);
                        opusUser.AuthenticationDB.AccountDevices = accountDevices.Records;
                        ProcessDbResult(model, accountDevices.Errors, accountDevices.Summary, accountDevices.Exception);
                    }
                }

                // Entitlement Database
                if (db.Equals("all") || db.Equals("entitlement"))
                {
                    if (opusUser.EntitlementDB == null) opusUser.EntitlementDB = new EntitlementDB();

                    if (table.Equals("all") || table.Equals("account_commercial_pkg"))
                    {

                        var acctCommPkgs = EntitlementDB.GetAccountCommercialPackages(uid);
                        opusUser.EntitlementDB.AccountCommercialPackages = acctCommPkgs.Records;
                        ProcessDbResult(model, acctCommPkgs.Errors, acctCommPkgs.Summary, acctCommPkgs.Exception);
                    }

                    if (table.Equals("all") || table.Equals("account_technical_pkg"))
                    {

                        var acctTechPkgs = EntitlementDB.GetAccountTechnicalPackages(uid);
                        opusUser.EntitlementDB.AccountTechnicalPackages = acctTechPkgs.Records;
                        ProcessDbResult(model, acctTechPkgs.Errors, acctTechPkgs.Summary, acctTechPkgs.Exception);
                    }
                }

                // Purchase database
                if (db.Equals("all") || db.Equals("purchase"))
                {
                    if (opusUser.PurchaseDB == null) opusUser.PurchaseDB = new PurchaseDB();

                    if (table.Equals("all") || table.Equals("purchase_transaction"))
                    {

                        var purchaseTransactions = PurchaseDB.GetPurchaseTransactions(uid);
                        opusUser.PurchaseDB.PurchaseTransactions = purchaseTransactions.Records;
                        ProcessDbResult(model, purchaseTransactions.Errors, purchaseTransactions.Summary, purchaseTransactions.Exception);
                    }
                }

                // NPVRbe database
                if (db.Equals("all") || db.Equals("npvrbe"))
                {
                    if (opusUser.NpvrbeDB == null) opusUser.NpvrbeDB = new NpvrbeDB();

                    if (table.Equals("all") || table.Equals("reportuserrecord"))
                    {

                        var reportUserRecords = NpvrbeDB.GetReportUserRecords(uid);
                        opusUser.NpvrbeDB.ReportUserRecords = reportUserRecords.Records;
                        ProcessDbResult(model, reportUserRecords.Errors, reportUserRecords.Summary, reportUserRecords.Exception);
                    }

                    if (table.Equals("all") || table.Equals("userchannels"))
                    {

                        var userChannels = NpvrbeDB.GetUserChannels(uid);
                        opusUser.NpvrbeDB.UserChannels = userChannels.Records;
                        ProcessDbResult(model, userChannels.Errors, userChannels.Summary, userChannels.Exception);
                    }

                    if (table.Equals("all") || table.Equals("usernpvrrecording"))
                    {

                        var userNpvrRecordings = NpvrbeDB.GetUserNpvrRecordings(uid);
                        opusUser.NpvrbeDB.UserNpvrRecordings = userNpvrRecordings.Records;
                        ProcessDbResult(model, userNpvrRecordings.Errors, userNpvrRecordings.Summary, userNpvrRecordings.Exception);
                    }

                    if (table.Equals("all") || table.Equals("usernpvrrecordingstopdelta"))
                    {

                        var userNpveRecordingStopDeltas = NpvrbeDB.GetUserNpvrRecordingStopDelta(uid);
                        opusUser.NpvrbeDB.UserNpvrRecordingStopDeltas = userNpveRecordingStopDeltas.Records;
                        ProcessDbResult(model, userNpveRecordingStopDeltas.Errors, userNpveRecordingStopDeltas.Summary, userNpveRecordingStopDeltas.Exception);
                    }

                    if (table.Equals("all") || table.Equals("usernpvrseriesrecording"))
                    {

                        var userNpvrSeriesRecordings = NpvrbeDB.GeUserNpvrSeriesRecordings(uid);
                        opusUser.NpvrbeDB.UserNpvrSeriesRecordings = userNpvrSeriesRecordings.Records;
                        ProcessDbResult(model, userNpvrSeriesRecordings.Errors, userNpvrSeriesRecordings.Summary, userNpvrSeriesRecordings.Exception);
                    }
                }

                // Getting records based on CrmAccountId from MySql tables.
                if (!string.IsNullOrEmpty(opusUser.CrmAccountId))
                {
                    // OneUX database
                    if (db.Equals("all") || db.Equals("oneux"))
                    {
                        if (opusUser.OneUxDB == null) opusUser.OneUxDB = new OneUxDB();

                        if (table.Equals("all") || table.Equals("prikboard"))
                        {

                            var prikboards = OneUxDB.GetPrikboards(opusUser.CrmAccountId);
                            opusUser.OneUxDB.Prikboards = prikboards.Records;
                            ProcessDbResult(model, prikboards.Errors, prikboards.Summary, prikboards.Exception);
                        }
                    }

                    // Telus Billing Database
                    if (db.Equals("all") || db.Equals("telus_billing"))
                    {
                        if (opusUser.TelusBillingDB == null) opusUser.TelusBillingDB = new TelusBillingDB();

                        if (table.Equals("all") || table.Equals("billing_transaction"))
                        {

                            var billingTransactions = TelusBillingDB.GetBillingTransactions(opusUser.CrmAccountId);
                            opusUser.TelusBillingDB.BillingTransactions = billingTransactions.Records;
                            ProcessDbResult(model, billingTransactions.Errors, billingTransactions.Summary, billingTransactions.Exception);
                        }
                    }

                    // Npvrbe
                    if (db.Equals("all") || db.Equals("npvrbe"))
                    {
                        if (opusUser.NpvrbeDB == null) opusUser.NpvrbeDB = new NpvrbeDB();

                        if (table.Equals("all") || table.Equals("npvruserdetails"))
                        {
                            var npvrUserDetails = NpvrbeDB.GetNpvrUserDetails(opusUser.CrmAccountId);
                            opusUser.NpvrbeDB.NpvrUserDetails = npvrUserDetails.Records;
                            ProcessDbResult(model, npvrUserDetails.Errors, npvrUserDetails.Summary, npvrUserDetails.Exception);
                        }
                    }

                    // Mediaroom Recording database
                    if (db.Equals("all") || db.Equals("telus_mediaroom_recording"))
                    {
                        if (opusUser.TelusMediaroomRecordingDB == null) opusUser.TelusMediaroomRecordingDB = new TelusMediaroomRecordingDB();

                        if (opusUser.TelusMediaroomRecordingDB == null) opusUser.TelusMediaroomRecordingDB = new TelusMediaroomRecordingDB();

                        if (table.Equals("all") || table.Equals("users"))
                        {

                            var mrUsers = TelusMediaroomRecordingDB.GetAccountSyncStatus(opusUser.CrmAccountId);
                            opusUser.TelusMediaroomRecordingDB.AccountSyncStatus = mrUsers.Records;
                            ProcessDbResult(model, mrUsers.Errors, mrUsers.Summary, mrUsers.Exception);
                        }

                        if (table.Equals("all") || table.Equals("recording_definitions"))
                        {
                            var recordingDefinitions = TelusMediaroomRecordingDB.GetRecordingDefinitions(opusUser.CrmAccountId);
                            opusUser.TelusMediaroomRecordingDB.RecordingDefinitions = recordingDefinitions.Records;
                            ProcessDbResult(model, recordingDefinitions.Errors, recordingDefinitions.Summary, recordingDefinitions.Exception);
                        }

                        if (table.Equals("all") || table.Equals("use_schedule_version"))
                        {
                            var userScheduleVersions = TelusMediaroomRecordingDB.GetUserScheduleVersions(opusUser.CrmAccountId);
                            opusUser.TelusMediaroomRecordingDB.UserScheduleVersions = userScheduleVersions.Records;
                            ProcessDbResult(model, userScheduleVersions.Errors, userScheduleVersions.Summary, userScheduleVersions.Exception);
                        }
                    }
                }
                #endregion

                #region Cassandra keyspaces
                // AVS_User_Password_History keyspace
                if (db.Equals("all") || db.Equals("avs_user_password_history"))
                {
                    if (opusUser.AvsUserPasswordHisotryKeyspace == null) opusUser.AvsUserPasswordHisotryKeyspace = new AvsUserPasswordHisotryKeyspace();

                    if (table.Equals("all") || table.Equals("user_password_history"))
                    {
                        var userPasswordHisotry = AvsUserPasswordHisotryKeyspace.GetUserPasswordHistory(uid);
                        opusUser.AvsUserPasswordHisotryKeyspace.UserPasswordHistory = userPasswordHisotry.Records;
                        ProcessDbResult(model, userPasswordHisotry.Errors, userPasswordHisotry.Summary, userPasswordHisotry.Exception);
                    }
                }

                // Token_Management
                if (db.Equals("all") || db.Equals("token_management"))
                {
                    if (opusUser.TokenManagementKeyspace == null) opusUser.TokenManagementKeyspace = new TokenManagementKeyspace();

                    if (table.Equals("all") || table.Equals("user_refresh_token"))
                    {
                        var userRefreshTokens = TokenManagementKeyspace.GetUserRefreshTokens(uid);
                        opusUser.TokenManagementKeyspace.UserRefreshTokens = userRefreshTokens.Records;
                        ProcessDbResult(model, userRefreshTokens.Errors, userRefreshTokens.Summary, userRefreshTokens.Exception);
                    }
                }

                // User_Entitlement keyspace
                if (db.Equals("all") || db.Equals("user_entitlement"))
                {
                    if (opusUser.UserEntitlementKeyspace == null) opusUser.UserEntitlementKeyspace = new UserEntitlementKeyspace();

                    if (table.Equals("all") || table.Equals("user_entitlement_profile"))
                    {
                        var userEntitlementProfiles = UserEntitlementKeyspace.GetUserEntitlementProfiles(uid);
                        opusUser.UserEntitlementKeyspace.UserEntitlementProfile = userEntitlementProfiles.Records;
                        ProcessDbResult(model, userEntitlementProfiles.Errors, userEntitlementProfiles.Summary, userEntitlementProfiles.Exception);
                    }
                }

                if (!string.IsNullOrEmpty(opusUser.CrmAccountId))
                {
                    // AVS_Bookmark keyspace 
                    if (db.Equals("all") || db.Equals("avs_bookmark"))
                    {
                        if (opusUser.AvsBookmarkKeyspace == null) opusUser.AvsBookmarkKeyspace = new AvsBookmarkKeyspace();

                        if (table.Equals("all") || table.Equals("bookmarks"))
                        {
                            var bookmarks = AvsBookmarkKeyspace.GetBookmarks(opusUser.CrmAccountId);
                            opusUser.AvsBookmarkKeyspace.Bookmarks = bookmarks.Records;
                            ProcessDbResult(model, bookmarks.Errors, bookmarks.Summary, bookmarks.Exception);
                        }

                        if (table.Equals("all") || table.Equals("watchhistory"))
                        {
                            if (opusUser.AvsBookmarkKeyspace == null) opusUser.AvsBookmarkKeyspace = new AvsBookmarkKeyspace();

                            var watchHistory = AvsBookmarkKeyspace.GetWatchHistory(opusUser.CrmAccountId);
                            opusUser.AvsBookmarkKeyspace.WatchHistory = watchHistory.Records;
                            ProcessDbResult(model, watchHistory.Errors, watchHistory.Summary, watchHistory.Exception);
                        }
                    }

                    // AVS_Concurrent_Streams keyspace
                    if (db.Equals("all") || db.Equals("avs_concurrrent_streams"))
                    {
                        if (table.Equals("all") || table.Equals("subscriberstreamusage"))
                        {
                            if (opusUser.AvsConcurrentStreamsKeyspace == null) opusUser.AvsConcurrentStreamsKeyspace = new AvsConcurrentStreamsKeyspace();

                            var subscriberStreamUsages = AvsConcurrentStreamsKeyspace.GetSubscriberStreamUsages(opusUser.CrmAccountId);
                            opusUser.AvsConcurrentStreamsKeyspace.SubscriberStreamUsages = subscriberStreamUsages.Records;
                            ProcessDbResult(model, subscriberStreamUsages.Errors, subscriberStreamUsages.Summary, subscriberStreamUsages.Exception);
                        }
                    }

                    // User Keyspace
                    if (db.Equals("all") || db.Equals("user"))
                    {
                        if (opusUser.UserKeyspace == null) opusUser.UserKeyspace = new UserKeyspace();

                        if (table.Equals("all") || table.Equals("workflow"))
                        {
                            var workflows = UserKeyspace.GetWorkflows(opusUser.CrmAccountId);
                            opusUser.UserKeyspace.Workflows = workflows.Records;
                            ProcessDbResult(model, workflows.Errors, workflows.Summary, workflows.Exception);
                        }
                    }
                }
                #endregion
            }
            #region Result
            sw.Stop();

            model.ExecutionTime = sw.ElapsedMilliseconds.ToString() + " ms";
            #endregion

            return Ok(model);
        }
        private void ProcessDbResult(ResponseModel rm, List<string> errors, List<string> summary, Exception? ex)
        {
            rm.Errors.AddRange(errors);
            rm.Summary.AddRange(summary);
            rm.Exception = ex;
        }
    }
}
