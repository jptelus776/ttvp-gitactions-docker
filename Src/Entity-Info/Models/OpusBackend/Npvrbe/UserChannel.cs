namespace EntityInfoService.Models.OpusBackend.Npvrbe
{
    // table npvrbe.userchannels
    public class UserChannel
    {
        // USERID        BIGINT(19)                             not null,
        public long UserId { get; set; }

        // CHANNELID     BIGINT(19)                             not null,
        public long ChannelId { get; set; }

        // CREATION_DATE DATETIME(19) default CURRENT_TIMESTAMP not null,
        public DateTime CreationDate { get; set; }

        // UPDATE_DATE   DATETIME(19) default CURRENT_TIMESTAMP not null,
        public DateTime UpdateDate { get; set; }
    }
}
