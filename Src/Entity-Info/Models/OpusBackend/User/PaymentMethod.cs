namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.payment_type
    public class PaymentType
    {
        // payment_type_id              int                    not null        primary key,
        public int PaymentTypeId { get; set; }

        // creation_date                datetime               null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date                  datetime               null,    
        public DateTime? UpdateDate { get; set; } = null;

        // payment_method               varchar(200)           not null,
        public string PaymentMethod { get; set; } = string.Empty;

        // pin_required                 varchar(1) default 'N' not null,
        public char? PinRequired { get; set; } = null;

        // gateway_required             varchar(1) default 'N' not null,
        public char? GatewayRequired { get; set; } = null;

        // is_mass_provisioning_allowed char                   null
        public char? IsMassProvisioningAllowed { get; set; } = null;
    }
}
