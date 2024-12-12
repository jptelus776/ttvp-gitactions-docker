namespace EntityInfoService.Models.OpusBackend.AvsUserPasswordHistory
{
    // table avs_user_password_history.user_password_history
    // primary key (userid, createdate, credential)

    public class UserPasswordHistry
    {
        // userid               bigint,
        public long UserId { get; set; }

        // createdate           timestamp,
        public DateTime? CreateDate { get; set; } = null;

        // credential           text,
        public string Credential { get; set; } = string.Empty;

        // encryption_algorithm text,
        public string EncryptionAlgorithm { get; set; } = string.Empty;

        // saltkey              text,
        public string SaltKey { get; set; } = string.Empty;
    }
}
