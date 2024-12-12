namespace EntityInfoService.Models.OpusBackend.Npvrbe
{
    // table npvrbe.npvruserdetails
    public class NpvrUserDetail
    {
        // ID                        BIGINT(19) auto_increment        primary key,
        public long Id { get; set; }

        // ACCOUNTID                 VARCHAR(100)                           not null,
        public string AccountId { get; set; } = string.Empty;

        // CONTEXTID                 VARCHAR(15)                            not null,
        public string ContextId { get; set; } = string.Empty;

        // CONCURRENTRECORDINGSLIMIT TINYINT(3)                             null,
        public byte? ConcurrentRecordingsLimit { get; set; } = null;

        // TOTALMINUTES              INT(10)                                not null,
        public int TotalMinutes { get; set; }

        // NPVRKEEPDAYS              SMALLINT(5)                            null,
        public short? NpvrKeepDays { get; set; } = null;

        // ENABLEDSERIES             TINYINT(3)   default 0                 not null,
        public byte EnabledSeries { get; set; } = 0;

        // CREATION_DATE             DATETIME(19) default CURRENT_TIMESTAMP not null,
        public DateTime CreationDate { get; set; }

        // UPDATE_DATE               DATETIME(19) default CURRENT_TIMESTAMP not null
        public DateTime UpdateDate { get; set; }
    }
}
