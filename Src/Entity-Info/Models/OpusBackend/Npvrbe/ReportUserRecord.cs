namespace EntityInfoService.Models.OpusBackend.Npvrbe
{
    // table npvrbe.reportuserrecord
    public class ReportUserRecord
    {
        // id                           INT(10) auto_increment        primary key,
        public int Id { get; set; }

        // RECORDID                     BIGINT(19)                             not null,
        public long RecordId { get; set; }

        // USERID                       BIGINT(19)                             not null,
        public long UserId { get; set; }

        // USERSTARTDELTA               BIGINT(19)                             not null,
        public long UserStartDelta { get; set; }

        // USERSTOPDELTA                BIGINT(19)                             not null,
        public long UserStopDelta { get; set; }

        // ISFAILED                     TINYINT(3)                             not null,
        public byte IsFailed { get; set; }

        // STATUS                       VARCHAR(20)                            not null,
        public string Status { get; set; } = string.Empty;

        // STATUSDETAILS                VARCHAR(500)                           not null,
        public string StatusDetails { get; set; } = string.Empty;

        // DELETEWHENSPACENEEDED        TINYINT(3)                             not null,
        public byte DeleteWhenSpaceNeeded { get; set; }

        // MARKEDFORDELETE              TINYINT(3)   default 0                 not null,
        public byte MarkedForDelete { get; set; } = 0;

        // ACTUALRECORDINGSECONDS       BIGINT(19)                             not null,
        public long ActualRecordingSeconds { get; set; }

        // EPISODESCOPE                 VARCHAR(8)                             null,
        public string? EpisodeScope { get; set; } = null;

        // SERIESID                     BIGINT(19)                             null,
        public long? SeriesId { get; set; } = null;

        // CHANNELID                    BIGINT(19)                             not null,
        public long ChannelId { get; set; }

        // SERIES_DELETEWHENSPACENEEDED TINYINT(3)                             null,
        public byte? SeriesDelteWhenSpaceNeeded { get; set; } = 0;

        // CHANNELBOUND                 TINYINT(3)                             null,
        public byte? ChannelBound { get; set; }

        // EPISODESTOKEEP               SMALLINT(5)                            null,
        public short? EpisodesToKeep { get; set; } = null;

        // SERIESREFNO                  VARCHAR(15)                            null,
        public string? SeriesInfo { get; set; } = null;

        // TITLE                        VARCHAR(200)                           null,
        public string? Title { get; set; }

        // CHANNELREFNUMBER             VARCHAR(50)                            null,
        public string? ChannelRefNumber { get; set; }

        // PROGRAMEXTERNALID            VARCHAR(20)                            not null,
        public string ProgramExternaId { get; set; } = string.Empty;

        // PROGRAMDURATION              SMALLINT(5)                            not null,
        public short ProgramDuration { get; set; }

        // PROGRAMSTARTTIME             BIGINT(19)                             not null,
        public long ProgramStartTime { get; set; }

        // RECORDINGSTARTTIME           BIGINT(19)                             not null,
        public long RecordingStartTime { get; set; }

        // ENABLENPVRTRICKPLAY          TINYINT(3)                             null,
        public byte? EnableNpvrTrickPlay { get; set; }

        // ENABLENPVRSKIPJUMP           TINYINT(3)                             null,
        public byte? EnableNpvrSkipJump { get; set; } = null;

        // EVENTUPDATESTATUS            VARCHAR(10)                            null,
        public string? EventUpdateStatus { get; set; } = null;

        // SCHEDULEID                   VARCHAR(255)                           null,
        public string? ScheduleId { get; set; }

        // CREATION_DATE                DATETIME(19) default CURRENT_TIMESTAMP not null,
        public DateTime CreationDate { get; set; }

        // UPDATE_DATE                  DATETIME(19) default CURRENT_TIMESTAMP not null
        public DateTime UpdateDate { get; set; }
    }
}
