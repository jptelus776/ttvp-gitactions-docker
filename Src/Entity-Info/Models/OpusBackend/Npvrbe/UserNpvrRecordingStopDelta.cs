namespace EntityInfoService.Models.OpusBackend.Npvrbe
{
    // table npvrbe.usernpvrrecordingstopdelta
    public class UserNpvrRecordingStopDelta
    {
        // RECORDID               BIGINT(19)              not null 
        public long RecordId { get; set; }

        // USERID                 BIGINT(19)              not null 
        public long UserId { get; set; }

        // USERNAME               VARCHAR(150)            not null 
        public string UserName { get; set; } = string.Empty;

        // MARKEDFORDELETE        TINYINT(3)   default 0  not null 
        public byte MarkedForDelete { get; set; }

        // USERSTOPDELTA          BIGINT(19)              not null 
        public long UserStopDelta { get; set; }

        // USERSTARTDELTA         BIGINT(19)              not null 
        public long UserStartDelta { get; set; }

        // ACTUALRECORDINGSECONDS BIGINT(19)              not null 
        public long ActualRecordingSeconds { get; set; }

        // CREATION_DATE          DATETIME(19) default CURRENT_TIMESTAMP   not null 
        public DateTime CreationDate { get; set; }

        // UPDATE_DATE            DATETIME(19) default CURRENT_TIMESTAMP   not null 
        public DateTime UpdateDate { get; set; }
    }
}
