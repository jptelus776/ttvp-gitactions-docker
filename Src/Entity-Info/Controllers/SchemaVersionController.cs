using EntityInfoService.DAL.CassandraDB;
using EntityInfoService.DAL.MySql;
using EntityInfoService.Models;
using EntityInfoService.Models.OpusBackend;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;

namespace EntityInfoService.Controllers
{
    [Route("api/v1/schema-versions")]
    [ApiController]
    public class SchemaVersionController : ControllerBase
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(SchemaVersionController));

        [HttpGet]
        public IActionResult GetSchemaVersions()
        {
            Stopwatch sw = Stopwatch.StartNew();
            ResponseModel rm = new ResponseModel();
            List<SchemaVersion> versions = new List<SchemaVersion>();

            ProcessDbResult(rm, versions, AuthenticationDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, ConfigurationDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, ContentPlaybackDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, CreDataNormalizationDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, DeviceManagerDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, DeviceResourceManagerDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, EntitlementDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, EntitlementProfileGenDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, ExportUserDataDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, NpvrbeDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, NpvrMediatorDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, OneUxDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, OttManagementDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, OttTechnicalCatalogueDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, PushMessageNotificationDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, PurchaseDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, ReportDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, RpgwDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, SoftwareUpgradeDB.GetSchemaVersions());
            // ProcessDbResult(rm, versions, SpringBatchDB.GetSchemaVersions());    // TODO: Check if this database exist in higher environment.
            ProcessDbResult(rm, versions, TechnicalCatalogueDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, TelusBillingDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, TelusClientFilesGenerationDB.GetSchemaVersions());
            // ProcessDbResult(rm, versions, TelusEasDB.GetSchemaVersions());       // TODO: telsu_db_version doesn't exist. Get Jira Id from Moh K.
            ProcessDbResult(rm, versions, TelusMediaroomRecordingDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, UserDB.GetSchemaVersions());
            ProcessDbResult(rm, versions, VodBulkIngestDB.GetSchemaVersions());
            // ProcessDbResult(rm, versions, WebCmsProductDB.GetSchemaVersions());  // TODO: avs_version table doesn't exist.

            ProcessDbResult(rm, versions, AvsBookmarkKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, AvsConcurrentStreamsKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, AvsUserPasswordHisotryKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, GroupDefinitionKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, PushMessageNotificationKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, TokenManagementKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, UserEntitlementKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, UserKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, UserProfileAvatarKeyspace.GetSchemaVersions());
            ProcessDbResult(rm, versions, WebSocketServiceKeyspace.GetSchemaVersions());

            rm.Result = versions;
            sw.Stop();

            rm.ExecutionTime = sw.ElapsedMilliseconds.ToString() + " ms";
            return Ok(rm);
        }

        [HttpGet("{schema}")]
        public IActionResult GetSchemaVersion(string schema)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ResponseModel rm = new ResponseModel();
            List<SchemaVersion> versions = new List<SchemaVersion>();

            _logger.Information("Recieved request - {requestMethod} {requestUrl}", Request.Method, Request.GetDisplayUrl());

            switch (schema.ToLower())
            {
                case "authentication":
                    ProcessDbResult(rm, versions, AuthenticationDB.GetSchemaVersions());
                    break;
                case "configuration":
                    ProcessDbResult(rm, versions, ConfigurationDB.GetSchemaVersions());
                    break;
                case "content_playback":
                    ProcessDbResult(rm, versions, ContentPlaybackDB.GetSchemaVersions());
                    break;
                case "cre_data_normalization":
                    ProcessDbResult(rm, versions, CreDataNormalizationDB.GetSchemaVersions());
                    break;
                case "device_manager":
                    ProcessDbResult(rm, versions, DeviceManagerDB.GetSchemaVersions());
                    break;
                case "device_resource_manager":
                    ProcessDbResult(rm, versions, DeviceResourceManagerDB.GetSchemaVersions());
                    break;
                case "entitlement":
                    ProcessDbResult(rm, versions, EntitlementDB.GetSchemaVersions());
                    break;
                case "entitlement_profile_gen":
                    ProcessDbResult(rm, versions, EntitlementProfileGenDB.GetSchemaVersions());
                    break;
                case "export_user_data":
                    ProcessDbResult(rm, versions, ExportUserDataDB.GetSchemaVersions());
                    break;
                case "npvrbe":
                    ProcessDbResult(rm, versions, NpvrbeDB.GetSchemaVersions());
                    break;
                case "npvr_mediator":
                    ProcessDbResult(rm, versions, NpvrMediatorDB.GetSchemaVersions());
                    break;
                case "oneux":
                    ProcessDbResult(rm, versions, OneUxDB.GetSchemaVersions());
                    break;
                case "ott_management":
                    ProcessDbResult(rm, versions, OttManagementDB.GetSchemaVersions());
                    break;
                case "ott_technical_catalogue":
                    ProcessDbResult(rm, versions, OttTechnicalCatalogueDB.GetSchemaVersions());
                    break;
                case "push_message_notification":
                    ProcessDbResult(rm, versions, PushMessageNotificationDB.GetSchemaVersions());
                    ProcessDbResult(rm, versions, PushMessageNotificationKeyspace.GetSchemaVersions());
                    break;
                case "purchase":
                    ProcessDbResult(rm, versions, PurchaseDB.GetSchemaVersions());
                    break;
                case "report":
                    ProcessDbResult(rm, versions, ReportDB.GetSchemaVersions());
                    break;
                case "rpgw":
                    ProcessDbResult(rm, versions, RpgwDB.GetSchemaVersions());
                    break;
                case "sofware_upgrade":
                    ProcessDbResult(rm, versions, SoftwareUpgradeDB.GetSchemaVersions());
                    break;
                case "spring_batch":
                    ProcessDbResult(rm, versions, SpringBatchDB.GetSchemaVersions());
                    break;
                case "technical_catalogue":
                    ProcessDbResult(rm, versions, TechnicalCatalogueDB.GetSchemaVersions());
                    break;
                case "telus_billing":
                    ProcessDbResult(rm, versions, TelusBillingDB.GetSchemaVersions());
                    break;
                case "telus_client_files_generation":
                    ProcessDbResult(rm, versions, TelusClientFilesGenerationDB.GetSchemaVersions());
                    break;
                case "telus_eas":
                    ProcessDbResult(rm, versions, PurchaseDB.GetSchemaVersions());
                    break;
                case "telus_mediaroom_recording":
                    ProcessDbResult(rm, versions, TelusMediaroomRecordingDB.GetSchemaVersions());
                    break;
                case "user":
                    ProcessDbResult(rm, versions, UserDB.GetSchemaVersions());
                    ProcessDbResult(rm, versions, UserKeyspace.GetSchemaVersions());
                    break;
                case "vod_bulk_ingest":
                    ProcessDbResult(rm, versions, VodBulkIngestDB.GetSchemaVersions());
                    break;
                case "web_cms_product":
                    ProcessDbResult(rm, versions, WebCmsProductDB.GetSchemaVersions());
                    break;
                case "avs_bookmark":
                    ProcessDbResult(rm, versions, AvsBookmarkKeyspace.GetSchemaVersions());
                    break;
                case "avs_conncurrent_streams":
                    ProcessDbResult(rm, versions, AvsConcurrentStreamsKeyspace.GetSchemaVersions());
                    break;
                case "avs_user_password_history":
                    ProcessDbResult(rm, versions, AvsUserPasswordHisotryKeyspace.GetSchemaVersions());
                    break;
                case "group_definition":
                    ProcessDbResult(rm, versions, GroupDefinitionKeyspace.GetSchemaVersions());
                    break;
                case "token_management":
                    ProcessDbResult(rm, versions, TokenManagementKeyspace.GetSchemaVersions());
                    break;
                case "user_entitlement":
                    ProcessDbResult(rm, versions, UserEntitlementKeyspace.GetSchemaVersions());
                    break;
                case "user_profile_avatar":
                    ProcessDbResult(rm, versions, UserProfileAvatarKeyspace.GetSchemaVersions());
                    break;
                case "web_socket_service":
                    ProcessDbResult(rm, versions, WebSocketServiceKeyspace.GetSchemaVersions());
                    break;
            }

            rm.Result = versions;
            sw.Stop();

            rm.ExecutionTime = sw.ElapsedMilliseconds.ToString() + " ms";
            return Ok(rm);
        }

        private static void ProcessDbResult(ResponseModel rm, List<SchemaVersion> versions, DbResultList<SchemaVersion> result)
        {
            versions.AddRange(result.Records);
            rm.Errors.AddRange(result.Errors);
            rm.Summary.AddRange(result.Summary);
            rm.Exception = result.Exception;
        }
    }
}
