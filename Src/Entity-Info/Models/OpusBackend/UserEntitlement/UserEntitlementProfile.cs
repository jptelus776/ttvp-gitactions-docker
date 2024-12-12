namespace EntityInfoService.Models.OpusBackend.UserEntitlement
{
    // table user_entitlement.user_entitlement_profile
    public class UserEntitlementProfile
    {
        // user_id                  bigint primary key,
        public long UserId { get; set; }

        // entitlement_profile_json list<frozen<ent_prof_json>>,
        public IEnumerable<ent_prof_json>? EntitlementProfileJson { get; set; } = null;

        // created_date             timestamp,
        public DateTime CreatedDate { get; set; }

        // updated_date             timestamp
        public DateTime UpdatedDate { get; set; }
    }


    public class ent_prof_json
    {
        public int tech_package_id { get; set; }
        public int? views_number { get; set; } = null;
        public string? tech_package_value { get; set; } = null;
        public DateTime? validity_period { get; set; } = null;
        public string? crm_account_id { get; set; } = null;
        public string? account_technical_pkg_id { get; set; } = null;
        public string? solution_offer_id { get; set; } = null;
        public string? pt_sequence_id { get; set; } = null;
        public string? commerce_model { get; set; } = null;
        public int? asset_id { get; set; } = null;
        public string? is_locked { get; set; } = null;
        public string? technical_package_external_id { get; set; } = null;
        public string? solution_offer_type { get; set; } = null;
        public string? external_commerical_pacakge_id { get; set; } = null;
        public string? isfreepreview { get; set; } = null;
        public DateTime? freepreview_start_date { get; set; } = null;
        public DateTime? freepreview_end_date { get; set; } = null;
    }

}
