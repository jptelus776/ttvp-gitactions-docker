namespace EntityInfoService.Models.OpusBackend.Npvrbe
{
    // table npvrbe.usernpvrrecording
    //    primary key (USERID, RECORDID, USERSTARTDELTA)
    public class UserNpvrRecording
    {
        // RECORDID               BIGINT(19)                             not null,
        public long RecordId { get; set; }

        // USERID                 BIGINT(19)                             not null,
        public long UserId { get; set; }

        // USERSTARTDELTA         BIGINT(19)                             not null,
        public long UserStartDelta { get; set; }

        // ISFAILED               TINYINT(3)                             not null,
        public byte IsFailed { get; set; }

        // STATUS                 VARCHAR(20)                            not null,
        public string Status { get; set; } = string.Empty;

        // STATUSDETAILS          VARCHAR(500)                           not null,
        public string StatusDetails { get; set; } = string.Empty;

        // DELETEWHENSPACENEEDED  TINYINT(3)                             not null,
        public byte DeleteWhenSpaceNeeded { get; set; }

        // SERIESID               BIGINT(19)                             null,
        public long? SeriesId { get; set; } = null;

        // USERSTOPDELTA          BIGINT(19)                             not null,
        public long UserStopDelta { get; set; }

        // MARKEDFORDELETE        TINYINT(3)   default 0                 not null,
        public byte MarkedForDelete { get; set; }

        // ACTUALRECORDINGSECONDS BIGINT(19)                             not null,
        public long ActualRecordingSeconds { get; set; }

        // CREATION_DATE          DATETIME(19) default CURRENT_TIMESTAMP not null,
        public DateTime CreationDate { get; set; }

        // UPDATE_DATE            DATETIME(19) default CURRENT_TIMESTAMP not null,
        public DateTime UpdateDate { get; set; }

        // USERNAME               VARCHAR(1000)                          null,
        public string? UserName { get; set; } = null;
    }
}
