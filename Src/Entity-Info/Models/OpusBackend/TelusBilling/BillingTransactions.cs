namespace EntityInfoService.Models.OpusBackend.TelusBilling
{
    // table telus_billing.billing_transactions
    public class BillingTransaction
    {
        // sequence_id         INT(10)        not null         primary key,
        public int SequenceId { get; set; }

        // crm_account_id      VARCHAR(100)   not null,
        public string CrmAccountId { get; set; } = string.Empty;

        // external_id         VARCHAR(255)   not null,
        public string ExternalId { get; set; } = string.Empty;

        // external_content_id VARCHAR(255)   not null,
        public string ExternalContentId { get; set; } = string.Empty;

        // start_date          DATETIME(19)   not null,
        public DateTime StartDate { get; set; }

        // end_date            DATETIME(19)   not null,
        public DateTime EndDate { get; set; }

        // purchase_date       DATETIME(19)   not null,
        public DateTime PurchaseDate { get; set; }

        // asset_type          VARCHAR(20)    not null,
        public string AssetType { get; set; } = string.Empty;

        // status              VARCHAR(10)    not null,
        public string Status { get; set; } = string.Empty;

        // price               DECIMAL(10, 2) not null,
        public decimal Price { get; set; }

        // title               VARCHAR(500)   not null,
        public string Title { get; set; } = string.Empty;

        // pc_level            VARCHAR(255)   not null,
        public string PcLevel { get; set; } = string.Empty;

        // platform            VARCHAR(20)    not null,
        public string Platform { get; set; } = string.Empty;

        // rental_period       INT(10)        not null,
        public int RentalPeriod { get; set; }

        // video_type          VARCHAR(5)     not null,
        public string VideoType { get; set; } = string.Empty;

        // creation_date       DATETIME(19)   not null,
        public DateTime CreationDate { get; set; }

        // update_date         DATETIME(19)   null,
        public DateTime? UpdateDate { get; set; } = null;

        // property            VARCHAR(255)   null,
        public string? Property { get; set; } = null;

        // show_id             VARCHAR(50)    null,
        public string? ShowId { get; set; } = null;

        // call_letter         VARCHAR(10)    null	
        public string? CallLetter { get; set; } = null;
    }

}
