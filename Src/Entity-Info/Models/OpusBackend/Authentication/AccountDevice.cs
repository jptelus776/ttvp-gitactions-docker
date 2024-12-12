namespace EntityInfoService.Models.OpusBackend.Authentication
{
    //authentication.account_device
    public class AccountDevice
    {
        // user_id       INT(10)                 not null,
        public long UserId { get; set; }

        // device_id     VARCHAR(128) default '' not null,
        public string DeviceId { get; set; } = string.Empty;

        // type_id       INT(10)                 null,
        public long? TypeId { get; set; } = null;

        // platform_id   VARCHAR(50)             null,
        public string? PlatformId { get; set; } = null;

        // creation_date DATETIME(19)            null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date   DATETIME(19)            null,
        public DateTime? UpdateDate { get; set; } = null;

        // secret_key    VARCHAR(255)            null,
        public string? SecretKey { get; set; } = null;
    }
}
