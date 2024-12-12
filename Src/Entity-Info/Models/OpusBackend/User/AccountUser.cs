namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.account_user
    public class AccountUser
    {
        //user_id                           int auto_increment          primary key,
        public long UserId { get; set; }

        // name varchar(255)               null,
        public string? Name { get; set; } = null;

        //status_id                         int                        not null,
        public int StatusId { get; set; }

        // party_description                 varchar(50)                null,        
        public string? PartyDescription { get; set; } = null;

        // consumption                       int(7)     default 0       not null
        public int Consumption { get; set; } = 0;

        // consumption_threshold             int(7)     default 1080000 not null,
        public int ConsumptionThreshold { get; set; } = 1080000;

        // purchase_value                    int(7)     default 0       not null,
        public int PurchaseValue { get; set; } = 0;

        // purchase_alerting                 int(7)     default 1600    not null,
        public int PurchaseAlerting { get; set; } = 1600;

        // creation_date                     datetime                   null,
        public DateTime? CreationDate { get; set; }

        // update_date                       datetime                   null,
        public DateTime? UpdateDate { get; set; }

        // smartcard_number                  varchar(50)                null,
        public string? SmartCardNumber { get; set; } = null;

        // sms_alerting                      varchar(1)                 null,
        public char? SmsAlerting { get; set; } = null;

        // blocco_acquisti                   varchar(1)                 null,
        public char? BloccoAcquisti { get; set; } = null;

        // flag_invio_sms_alerting           varchar(1) default 'N'     null,
        public char? FlagInvioSmsAlerting { get; set; } = 'N';

        // purchase_threshold_blocking       int(7)     default 2400    not null,
        public int PurchaseThresholdBlocking { get; set; } = 2400;

        // data_primo_accesso                datetime                   null,
        public DateTime? DataPrimoAccesso { get; set; } = null;

        // flag_invio_sms_blocking           varchar(1) default 'N'     null,
        public char? FlagInvioSmsBlocking { get; set; } = 'N';

        // stored_credit_card                varchar(1) default 'N'     null,
        public char? StoredCreditCard { get; set; } = 'N';

        // parent_user_id                    int                        null,
        public int? ParentUserId { get; set; } = null;

        // firstname                         varchar(255)               null,
        public string? FirstName { get; set; } = null;

        // surname                           varchar(255)               null,
        public string? Surname { get; set; } = null;

        // birth_date                        datetime                   null,
        public DateTime? BirthDate { get; set; } = null;

        // mobile_phone                      varchar(50)                null,
        public string? MobilePhone { get; set; } = null;

        // email                             varchar(255)               null,
        public string? Email { get; set; } = null;

        // zip_code                          varchar(50)                null,
        public string? ZipCode { get; set; } = null;

        // gender                            varchar(1)                 null,
        public char? Gender { get; set; } = null;

        // customer_code                     varchar(100)               null,
        public string? CustomerCode { get; set; } = null;

        // user_based_rec_enabled            varchar(1) default 'Y'     null,
        public char? UserBasedRecordingEnabled { get; set; } = 'Y';

        // ui_language_type                  varchar(255)               null,
        public string? UILanguageType { get; set; } = null;

        // audio_language_type               varchar(255)               null,
        public string? AudioLanguageType { get; set; } = null;

        // nickname                          varchar(255)               null,
        public string? NickName { get; set; } = null;

        // purchase_pin_wrong_attempts_count int        default 0       not null,
        public int PurchasePinWrongAttemptsCount { get; set; } = 0;

        // one_pin_flag                      char                       null,
        public char? OnePinFlag { get; set; } = null;

        // channel_hiding_enabled            char       default 'N'     not null,
        public char? ChannelHidingEnabled { get; set; } = 'N';

        // purchase_pin_enabled              char                       null,
        public char? PurchasePinEnabled { get; set; } = null;

        // parental_control_pin_enabled      char                       null,
        public char? ParentalControlPinEnabled { get; set; } = null;

        // preferred_epg_format              varchar(50)                null,
        public string? PreferredEpgFormat { get; set; } = null;

        // purchase_pin_salt_key             varchar(512)               null,
        public string? PurchasePinSaltKey { get; set; } = null;

        // purchase_pin_encryption           varchar(30)                null,
        public string? PurchasePinEncryption { get; set; } = null;

        // purchase_pin                      varchar(512)               null,
        public string? PurchasePin { get; set; } = null;

        // pc_pin_salt_key                   varchar(512)               null,
        public string? ParentalControlPinSaltKey { get; set; } = null;

        // pc_pin_encryption                 varchar(30)                null,
        public string? ParentalControlPinEncryption { get; set; } = null;

        // pc_pin                            varchar(512)               null,
        public string? ParentalControlPin { get; set; } = null;

        // pc_pin_wrong_attempts_count       int        default 0       not null,
        public int ParentalControlPinWrongAttemptsCount { get; set; } = 0;
    }
}

