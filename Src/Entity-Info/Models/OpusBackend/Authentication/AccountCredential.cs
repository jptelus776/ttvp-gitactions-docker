namespace EntityInfoService.Models.OpusBackend.Authentication
{    public class AccountCredential
    {
        // user_id              INT(10)      not null	primary key,
        public long UserId { get; set; }

        // username             VARCHAR(255) null,
        public string? UserName { get; set; } = null;

        // credential           VARCHAR(512) null,
        public string? Credential { get; set; } = null;

        // creation_date        DATETIME(19) null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date          DATETIME(19) null,
        public DateTime? UpdateDate { get; set; } = null;

        // encryption_algorithm VARCHAR(20)  null,
        public string? EncryptionAlgorithm { get; set; } = null;

        // encryption_salt_key  VARCHAR(512) null,
        public string? EncryptionSaltKey { get; set; } = null;

        // blacklist            VARCHAR(1)   null,
        public char? Blacklist { get; set; } = null;

        // retailer_id          VARCHAR(128) null,
        public string? RetailerId { get; set; } = null;
    }
}
