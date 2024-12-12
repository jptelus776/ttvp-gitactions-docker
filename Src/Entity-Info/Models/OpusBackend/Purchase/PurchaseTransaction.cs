namespace EntityInfoService.Models.OpusBackend.Purchase
{
    // table purchase.purchase_transaction
    public class PurchaseTransaction
    {
        // sequence_id                    INT(10) auto_increment primary key,
        public int SequenceId { get; set; }

        // creation_date                  DATETIME(19)           null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date                    DATETIME(19)           null,
        public DateTime? UpdateDate { get; set; } = null;

        // currency                       VARCHAR(3)             null,
        public string? Currency { get; set; } = null;

        // original_price                 DECIMAL(10, 2)         null,
        public decimal? OriginalPrice { get; set; } = null;

        // transaction_price              DECIMAL(10, 2)         null,
        public decimal? TransactionPrice { get; set; } = null;

        // start_date                     DATETIME(19)           null,
        public DateTime? StartDate { get; set; } = null;

        // end_date                       DATETIME(19)           null,
        public DateTime? EndDate { get; set; } = null;

        // user_id                        INT(10)                null,
        public int? UserId { get; set; } = null;

        // item_id                        INT(10)                null,
        public int? ItemId { get; set; } = null;

        // item_type                      INT(10)                null,
        public int? ItemType { get; set; } = null;

        // state                          INT(10)                null,
        public int? State { get; set; } = null;

        // token                          VARCHAR(100)           null,
        public string? Token { get; set; } = null;

        // transaction_id                 VARCHAR(100)           null,
        public string? TransactionId { get; set; } = null;

        // refund_transaction_id          VARCHAR(100)           null,
        public string? RefundTransactionId { get; set; } = null;

        // pgw_status                     VARCHAR(200)           null,
        public string? PgwStatus { get; set; }

        // payment_type_id                INT(10)                null,
        public int? PaymentTypeId { get; set; } = null;

        // payer_id                       VARCHAR(100)           null,
        public string? PayerId { get; set; } = null;

        // refund_date                    DATETIME(19)           null,
        public DateTime? RefundDate { get; set; } = null;

        // refund_description             VARCHAR(200)           null,
        public string? RefundDescription { get; set; } = null;

        // refund_price                   DECIMAL(10, 2)         null,
        public decimal? RefundPrice { get; set; }

        // parent_item_id                 INT(10)                null,
        public int? ParentItemId { get; set; } = null;

        // ipn_tnx_id                     VARCHAR(100)           null,
        public string? IpnTnxId { get; set; } = null;

        // last_ipn_date                  DATETIME(19)           null,
        public DateTime? LastIpnDate { get; set; } = null;

        // ipn_tnx_type                   VARCHAR(100)           null,
        public string? IpnTnxType { get; set; } = null;

        // recurring_profile_id           VARCHAR(50)            null,
        public string? RecurringProfileId { get; set; } = null;

        // recurring_original_price       DECIMAL(10, 2)         null,
        public decimal? RecurringOriginalPrice { get; set; } = null;

        // recurring_transaction_price    DECIMAL(10, 2)         null,
        public decimal? RecurringTransactionPrice { get; set; } = null;

        // notes                          VARCHAR(256)           null,
        public string? Notes { get; set; } = null;

        // promotion_name                 VARCHAR(50)            null,
        public string? PromotionName { get; set; } = null;

        // is_adult                       VARCHAR(1) default 'N' not null,
        public string IsAdult { get; set; } = "N";

        // platform_name                  VARCHAR(50)            null,
        public string? PlatformName { get; set; } = null;

        // device_id                      VARCHAR(128)           null,
        public string? DeviceId { get; set; } = null;

        // device_type                    VARCHAR(50)            null,
        public string? DeviceType { get; set; } = null;

        // service_name                   VARCHAR(50)            null,
        public string? ServiceName { get; set; } = null;

        // crm_account_id                 VARCHAR(100)           null,
        public string? CrmAccountId { get; set; } = null;

        // purchase_channel               VARCHAR(50)            null,
        public string? PurchaseChannel { get; set; } = null;

        // deactivation_date              DATETIME(19)           null,
        public DateTime? DeactivationDate { get; set; } = null;

        // provider_name                  VARCHAR(256)           null,
        public string? ProviderName { get; set; } = null;

        // asset_id                       INT(10)                null,
        public int? AssetId { get; set; } = null;

        // grace_period_end_date          DATETIME(19)           null,
        public DateTime? GracePeriodEndDate { get; set; } = null;

        // frequency_type                 VARCHAR(255)           null,
        public string? FrequencyType { get; set; } = null;

        // frequency_value                BIGINT(19)             null,
        public long? FrequencyValue { get; set; } = null;

        // switch_from_sequence_id        INT(10)                null,
        public int? SwitchFromSequenceId { get; set; } = null;

        // trial_end_date                 DATETIME(19)           null,
        public DateTime? TrialEndDate { get; set; } = null;

        // contract_start_date            DATETIME(19)           null,
        public DateTime? ContractStartDate { get; set; } = null;

        // contract_end_date              DATETIME(19)           null,
        public DateTime? ContractEndDate { get; set; } = null;

        // termination_date               DATETIME(19)           null,
        public DateTime? TerminationDate { get; set; } = null;

        // cancellation_reason            VARCHAR(250)           null,
        public string? CancellationReason { get; set; } = null;

        // voucher_campaign_name          VARCHAR(50)            null,
        public string? VoucherCampaignName { get; set; } = null;

        // termination_reason             VARCHAR(250)           null,
        public string? TransactionReason { get; set; } = null;

        // commercial_package_external_id VARCHAR(255)           null,
        public string? CommercialPackageExternalId { get; set; } = null;

        // external_voucher_code          VARCHAR(255)           null,
        public string? ExternalVoucherCode { get; set; } = null;

        // user_ip_address                VARCHAR(50)            null,
        public string? UserIpAddress { get; set; } = null;

        // deactivate_channel             VARCHAR(50)            null,
        public string? DeactivateChannel { get; set; } = null;

        // trc_id                         BIGINT(19)             null,
        public long? TrcId { get; set; } = null;

        // trc_name                       VARCHAR(100)           null,
        public string? TrcName { get; set; } = null;

        // rc_pc_id                       BIGINT(19)             null,
        public long? RcPcId { get; set; } = null;

        // rc_pc_name                     VARCHAR(150)           null,
        public string? RcPcName { get; set; } = null;

        // nrc_pc_id                      BIGINT(19)             null,
        public long? NrcPcId { get; set; } = null;

        // nrc_pc_name                    VARCHAR(150)           null,
        public string? NrcPcName { get; set; } = null;

        // property                       VARCHAR(100)           null,
        public string? Property { get; set; } = null;

        // retailer_id                    VARCHAR(128)           null
        public string? RetailerId { get; set; } = null;
    }
}
