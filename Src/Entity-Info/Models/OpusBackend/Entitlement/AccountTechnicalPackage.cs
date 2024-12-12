namespace EntityInfoService.Models.OpusBackend.Entitlement
{
    // table entitlement.account_technical_pkg
    public class AccountTechnicalPackage
    {
        // user_id                       INT(10)           not null,
        public long UserId { get; set; }

        // tech_package_id               INT(10)           not null,
        public int TechPackageId { get; set; }

        // views_number                  INT(10)           null,
        public int? ViewsNumber { get; set; } = null;

        // tech_package_value            VARCHAR(50)       null,
        public string? TechPackageValue { get; set; } = null;

        // creation_date                 DATETIME(19)      null,
        public DateTime? CreationDate { get; set; }

        // validity_period               DATETIME(19)      null,
        public DateTime? ValidityPeriod { get; set; } = null;

        // crm_account_id                VARCHAR(100)      not null,
        public string CrmAccountId { get; set; } = string.Empty;

        // account_technical_pkg_id      BIGINT(19) auto_increment  primary key,
        public long AccountTechnicalPkgId { get; set; }

        // update_date                   DATETIME(19)      null,
        public DateTime? UpdateDate { get; set; } = null;

        // solution_offer_id             INT(10) default 0 not null,
        public int SolutionOfferId { get; set; } = 0;

        // pt_sequence_id                INT(10)           null,
        public int? PtSequenceId { get; set; } = null;

        // commerce_model                VARCHAR(50)       null,
        public string? CommerceModel { get; set; } = null;

        // asset_id                      INT(10)           null,
        public string? AssetId { get; set; } = null;

        // is_locked                     CHAR              null,
        public char? IsLocked { get; set; }

        // technical_package_external_id VARCHAR(255)      null,
        public string? TechnicalPacakgeExternalId { get; set; } = null;
    }
}
