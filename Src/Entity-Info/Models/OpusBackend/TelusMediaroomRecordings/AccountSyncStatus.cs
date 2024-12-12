namespace EntityInfoService.Models.OpusBackend.TelusMediaroomRecordings
{
    // table telus_mediaroom_recordings.account_sync_status
    public class AccountSyncStatus
    {
        // account_sync_status_id bigint auto_increment         primary key,
        public long AccountSyncStatusId { get; set; }

        // crm_account_id VARCHAR(45)                            not null,
        public string CrmAccountId { get; set; } = string.Empty;

        // creation_date          datetime default CURRENT_TIMESTAMP not null,
        public DateTime CreationDate { get; set; }

        // last_sync_date         datetime                           null,
        public DateTime? LastSyncDate { get; set; } = null;
    }
}
