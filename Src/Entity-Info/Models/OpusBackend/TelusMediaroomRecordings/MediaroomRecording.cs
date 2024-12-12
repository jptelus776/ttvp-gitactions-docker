namespace EntityInfoService.Models.OpusBackend.TelusMediaroomRecordings
{
    // table telus_mediaroom_recordings.recordings
    public class MediaroomRecording
    {
        // recording_id            bigint auto_increment primary key,
        public long RecordingId { get; set; }

        // recording_definition_id bigint       null,
        public long? RecordingDefinitionId { get; set; } = null;

        // mr_rec_guid             varchar(100) null,
        public string? MrRecGuid { get; set; } = null;

        // duration_seconds        int          null,
        public int? DurationSeconds { get; set; } = null;

        // hard_end_pad_seconds    int          null,
        public int? HardEndPadSeconds { get; set; } = null;

        // keep_untill             tinyint(2)   null,
        public byte? KeepUntill { get; set; } = null;

        // state_code              tinyint(2)   null,
        public byte? StateCode { get; set; } = null;

        // utc_starttime           varchar(45)  null,
        public string? UtcStartTime { get; set; } = null;

        // program_id              varchar(50)  null,
        public string? ProgramId { get; set; } = null;

        // created_time            datetime     null,
        public DateTime? CreatedTime { get; set; } = null;

        // updated_time            datetime     null,
        public DateTime? UpdatedTime { get; set; } = null;

        // title                   varchar(255) null,
        public string? Title { get; set; } = null;

        // description             text         null,
        public string? Description { get; set; } = null;

        // recorded_keep_untill    tinyint      null,
        public byte? RecordedKeepUntil { get; set; } = null;

        // utc_actual_starttime    varchar(45)  null,
        public string? UtcActualStartTime { get; set; } = null;

        // utc_actual_endtime      varchar(45)  null,
        public string? UtcActualEndTime { get; set; } = null;
    }
}
