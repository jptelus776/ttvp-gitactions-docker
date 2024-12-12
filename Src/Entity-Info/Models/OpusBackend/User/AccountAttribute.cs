namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.account_attribute
    public class AccountAttribute
    {
        //user_attribute_id    int auto_increment	 primary key,
        public long UserAttributeId { get; set; }

        //attribute_detail_id  int                                    not null,
        public int AttributeDetailId { get; set; }

        // user_id              int                                    not null,
        public long UserId { get; set; }

        // attribute_value      varchar(512)                           null,
        public string? AttributeValue { get; set; } = null;

        // creation_date        datetime default '2011-12-28 18:59:16' not null,
        public DateTime CreationDate { get; set; }

        // update_date          datetime default '2011-12-28 18:59:16' not null,
        public DateTime UpdateDate { get; set; }

        // encryption_algorithm varchar(10)                            null,
        public string? EncryptionAlgorithm { get; set; } = null;

        public AttributeDetail AttributeDetail { get; set; } = new AttributeDetail();

    }
}
