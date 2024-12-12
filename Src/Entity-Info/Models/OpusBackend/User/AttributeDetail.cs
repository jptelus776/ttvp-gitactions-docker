namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.attribute_detail
    public class AttributeDetail
    {
        //attribute_detail_id          int auto_increment   primary key,
        public int AttributeDetailId { get; set; }

        //attribute_detail_name        varchar(100) not null,
        public string AttributeDetailName { get; set; } = string.Empty;

        //attribute_detail_description varchar(255) null,
        public string? AttributeDetailDescription { get; set; } = null;

        //default_attribute_value      varchar(255) null,
        public string? DefaultAttributeValue { get; set; } = null;

        //creation_date                datetime     null,
        public DateTime? CreationDate { get; set; } = null;

        //update_date                  datetime     null,
        public DateTime? UpdateDate { get; set; } = null;

        //is_custom                    varchar(1)   null,
        public char? IsCustom { get; set; } = null;

        //attribute_label              varchar(100) null,
        public string? AttributeLabel { get; set; } = null;

        //data_type                    varchar(50)  null,
        public string? DataType { get; set; } = null;

        //data_max_length              int          null,
        public int? DataMaxLength { get; set; } = null;

        //is_editable_by_end_user      varchar(1)   null,
        public char? IsEditableByEndUser { get; set; } = null;

        //is_editable_by_operator      varchar(1)   null,
        public char? IsEditableByOperator { get; set; } = null;

        //status_id                    int          null,
        public char? StatusId { get; set; } = null;

        //allowed_values               varchar(500) null,
        public string? AllowedValues { get; set; } = null;

        //is_mandatory                 varchar(1)   null,
        public char? IsMandatory { get; set; } = null;

        //is_visible_by_end_user       varchar(1)   null,
        public char? IsVisibileByEndUser { get; set; } = null;

        //is_only_for_master_account   varchar(1)   null,
        public char? IsOnlyForMasterAccount { get; set; } = null;

        //is_masked                    varchar(1)   null,
        public char? IsMasked { get; set; } = null;

        //is_for_recommendation        varchar(1)   null,
        public char? IsForRecommendations { get; set; } = null;

        //is_for_analytics             varchar(1)   null,
        public char? IsForAnalytics { get; set; } = null;
    }

}
