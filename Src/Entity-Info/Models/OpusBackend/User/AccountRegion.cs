namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.account_region
    public class AccountRegion
    {
        // account_region_id  int auto_increment	primary key,
        public int AccountRegionId { get; set; }

        // user_id            int          not null,
        public long UserId { get; set; }

        // region_id          int          not null,
        public int RegionId { get; set; }

        // region_name        varchar(100) null,
        public string? RegionName { get; set; } = null;

        // geo_country        varchar(200) null,
        public string? GeoCountry { get; set; } = null;

        // geo_state          varchar(200) null,
        public string? GeoState { get; set; } = null;

        // geo_province       varchar(200) null,
        public string? GeoProvince { get; set; } = null;

        // geo_city           varchar(200) null,
        public string? GeoCity { get; set; } = null;

        // geo_zip_code       varchar(10)  null,
        public string? GeoZipCode { get; set; }

        // geo_country_code   char(2)      null,
        public string? GeoCountryCode { get; set; } = null;

        // geo_state_code     char(20)     null,
        public string? GeoStateCode { get; set; } = null;

        // geo_province_code  varchar(20)  null,
        public string? GeoProvinceCode { get; set; } = null;

        // bill_country       varchar(200) null,
        public string? BillCountry { get; set; } = null;

        // bill_state         varchar(200) null,
        public string? BillState { get; set; } = null;

        // bill_province      varchar(200) null,
        public string? BillProvince { get; set; } = null;

        // bill_city          varchar(200) null,
        public string? BillCity { get; set; } = null;

        // bill_zip_code      varchar(10)  null,
        public string? BillZipCode { get; set; } = null;

        // bill_country_code  char(2)      null,
        public string? BillCountryCode { get; set; } = null;

        // bill_state_code    char(20)     null,
        public string? BillStateCode { get; set; } = null;

        // bill_province_code varchar(20)  null,
        public string? BillProvinceCode { get; set; } = null;

        // creation_date      datetime     null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date        datetime     null,
        public DateTime? UpdateDate { get; set; } = null;
    }
}
