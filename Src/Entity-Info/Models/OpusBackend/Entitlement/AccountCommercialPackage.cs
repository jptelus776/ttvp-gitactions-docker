namespace EntityInfoService.Models.OpusBackend.Entitlement
{
    // create table entitlement.account_commercial_pkg
    public class AccountCommercialPackage
    {
        // account_commercial_pkg_identifier INT(10) auto_increment	primary key,
        public int AccountCommercialPkgIdentifier { get; set; }

        // user_id                           INT(10)      null,
        public int? UserId { get; set; } = null;

        // commercial_package_id             BIGINT(19)   null,
        public long? CommercialPackageId { get; set; } = null;

        // validity_period                   DATETIME(19) null,
        public DateTime? ValidityPeriod { get; set; } = null;

        // created_date                      DATETIME(19) null,
        public DateTime? CreatedDate { get; set; } = null;

        // updated_date                      DATETIME(19) null,
        public DateTime? UpdateDate { get; set; } = null;

        // external_commerical_pacakge_id    VARCHAR(255) null,
        public string? ExternalCommercialPackageId { get; set; } = null;

        // solution_offer_type               VARCHAR(20)  null
        public string? SolutionOfferType { get; set; } = null;
    }
}
