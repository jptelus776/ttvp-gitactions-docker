namespace EntityInfoService.Models.OpusBackend
{
    public class TableRecordCount
    {
        public string Schema { get; set; } = string.Empty;
        public string Table { get; set; } = string.Empty;
        public long Count { get; set; } = 0;
        public long ElapsedTime { get; set; } = 0;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
