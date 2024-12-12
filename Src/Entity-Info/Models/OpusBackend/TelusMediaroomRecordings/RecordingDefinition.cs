namespace EntityInfoService.Models.OpusBackend.TelusMediaroomRecordings
{
    // table telus_mediaroom_recordings.recording_definitions
    public class RecordingDefinition
    {
        // recording_definition_id bigint auto_increment  primary key,
        public long RecordingDefinitionId { get; set; }

        // mr_recdef_guid          varchar(100) null,
        public string? MrRecdefGuid { get; set; } = null;

        // crm_account_id          varchar(100) null,
        public string? CrmAccountId { get; set; } = null;

        // channel_number          int          null,
        public int? ChannelNumber { get; set; } = null;

        // frequency_code          tinyint(2)   null,
        public byte? FrequencyCode { get; set; } = null;

        // keep_until              tinyint(2)   null,
        public byte? KeepUntil { get; set; } = null;

        // state_code              tinyint(2)   null,
        public byte? StateCode { get; set; } = null;

        // station_id              varchar(50)  null,
        public string? StationId { get; set; } = null;

        // title                   varchar(255) null,
        public string? Title { get; set; } = null;

        // utc_start_time          varchar(45)  null,
        public string? UtcStartTime { get; set; } = null;

        // airtime_domain          tinyint(2)   null,
        public byte? AirtimeDomain { get; set; } = null;

        // series_id               varchar(50)  null,
        public string? SeriesId { get; set; } = null;

        // show_type               tinyint(2)   null,
        public byte? ShowType { get; set; } = null;

        // created_time            datetime     null,
        public DateTime? CreatedTime { get; set; } = null;

        // updated_time            datetime     null,
        public DateTime? UpdatedTime { get; set; } = null;

        public List<MediaroomRecording> Recordings { get; set; } = new List<MediaroomRecording>();
    }
}
