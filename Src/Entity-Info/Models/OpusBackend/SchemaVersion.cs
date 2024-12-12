namespace EntityInfoService.Models.OpusBackend
{
    // table *.avs_version, *.telus_db_version
    // primary key (avs_release, avs_last_incremental, avs_start_incremental, creation_date)
    public class SchemaVersion
    {
        public string SchemaName { get; set; } = string.Empty;

        public string TableName { get; set; } = "avs_version";

        public string DbType { get; set; } = "MySql";

        // avs_release           text,
        public string? AvsRelease { get; set; } = null;

        // avs_last_incremental  int,
        public int? AvsLastIncremental { get; set; } = null;

        // avs_start_incremental int,
        public int? AvsStartIncremental { get; set; } = null;

        // creation_date         timestamp,
        public DateTime? CreationDate { get; set; } = null;

        // update_date           timestamp,
        public DateTime? UpdateDate { get; set; } = null;
    }
}
