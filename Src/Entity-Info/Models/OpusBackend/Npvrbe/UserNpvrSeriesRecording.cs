namespace EntityInfoService.Models.OpusBackend.Npvrbe
{
    // table npvrbe.usernpvrseriesrecording
    //    primary key (SERIESID, USERID, CHANNELID)
    public class UserNpvrSeriesRecording
    {
        // SERIESID              BIGINT(19)                                 not null,
        public long SeriesId { get; set; }

        // USERID                BIGINT(19)                                 not null,
        public long UserId { get; set; }

        // CHANNELID             BIGINT(19)                                 not null,
        public long ChannelId { get; set; }

        // DELETEWHENSPACENEEDED TINYINT(3)                                 not null,
        public byte DeleteWhenSpaceNeeded { get; set; }

        // CHANNELBOUND          TINYINT(3)                                 not null,
        public byte ChannelBound { get; set; }

        // EPISODESCOPE          VARCHAR(8)                                 not null,
        public string EpisodeScope { get; set; } = string.Empty;

        // EPISODESTOKEEP        SMALLINT(5)                                not null,
        public short EpisodeToKeep { get; set; }

        // SCHEDULETIMESTAMP     TIMESTAMP(26) default CURRENT_TIMESTAMP(6) not null,
        public DateTime ScheduledTimestamp { get; set; }

        // MARKEDFORDELETE       TINYINT(3)    default 0                    not null,
        public byte MarkedForDelete { get; set; }

        // CREATION_DATE         DATETIME(19)  default CURRENT_TIMESTAMP    not null,
        public DateTime CreationDate { get; set; }

        // UPDATE_DATE           DATETIME(19)  default CURRENT_TIMESTAMP    not null,
        public DateTime UpdateDate { get; set; }

        // USERNAME              VARCHAR(1000)                              null,
        public string? UserName { get; set; } = null;
    }
}
