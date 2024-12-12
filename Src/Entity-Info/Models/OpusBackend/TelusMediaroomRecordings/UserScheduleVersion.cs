namespace EntityInfoService.Models.OpusBackend.TelusMediaroomRecordings
{
    // table telus_mediaroom_recordings.user_schedule_version
    public class UserScheduleVersion
    {
        // id  bigint auto_increment     primary key,
        public long Id { get; set; }

        // crm_account_id      VARCHAR(45)                            not null,
        public string? CrmAccountId { get; set; } = null;

        // scheduled_version     bigint     default 0                 null,
        public long? ScheduledVersion { get; set; } = 0;

        // creation_date       DATETIME(19) default CURRENT_TIMESTAMP null,
        public DateTime? CreationDate { get; set; } = null;

        // last_refreshed_date datetime   default CURRENT_TIMESTAMP null,
        public DateTime? LastRefreshedDate { get; set; } = null;

        // is_get_in_progress    tinyint(1) default 0                 null,
        public Byte? IsGetInProgress { get; set; } = null;

        // locked_time datetime(6)                          null,
        public DateTime? LockedTime { get; set; } = null;

        // get_call_updated_date datetime                             null
        public DateTime? GetCallUpdatedDate { get; set; } = null;
    }

}
