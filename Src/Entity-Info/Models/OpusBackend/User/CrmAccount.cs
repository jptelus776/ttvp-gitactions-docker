namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.crm_account
    public class CrmAccount
    {
        // crm_account_id         varchar(100)           not null primary key,
        public string CrmAccountId { get; set; } = string.Empty;

        // user_id                int                    not null,
        public long UserId { get; set; }

        // smartcard_number       varchar(50)            null,
        public string? SmartCardNumber { get; set; } = null;

        // name                   varchar(256)           null,
        public string? Name { get; set; } = null;

        // surname                varchar(256)           null,
        public string? Surname { get; set; } = null;

        // birth_date             datetime               null,
        public DateTime? BirthDate { get; set; } = null;

        // mobile_phone           varchar(50)            null,
        public string? MobilePhone { get; set; } = null;

        // email                  varchar(255)           null,
        public string? Email { get; set; } = null;

        // cap                    varchar(50)            null,
        public string? Cap { get; set; } = null;

        // sesso                  varchar(1)             null,
        public char? SeSso { get; set; } = null;

        // registrazione_web      varchar(50)            null,
        public string? RegstrazioneWeb { get; set; } = null;

        // consenso_pctvottv      varchar(1) default 'Y' null,
        public char? ConsensoPctvOttv { get; set; } = null;

        // smartcard_payment_type varchar(50)            null,
        public string? SmartCardPaymentType { get; set; } = null;

        // smartcard_status       varchar(50)            null,
        public string? SmartCardStatus { get; set; } = null;

        // stato_morosita         varchar(50)            null,
        public string? StatoMorosita { get; set; } = null;

        // article_id             varchar(50)            null,
        public string? ArticleId { get; set; } = null;

        // vip                    varchar(1)             null,
        public char? VIP { get; set; } = null;

        // creation_date          datetime               null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date            datetime               null,  
        public DateTime? UpdateDate { get; set; } = null;

        // customer_code          varchar(100)           null,
        public string? CustomerCode { get; set; } = null;

        // activation_date        datetime               null,
        public DateTime? ActivationDate { get; set; } = null;

        // deactivation_date      datetime               null,
        public DateTime? DeactivationDate { get; set; } = null;

        // account_type           varchar(255)           null,
        public string? AccountType { get; set; } = null;
    }
}
