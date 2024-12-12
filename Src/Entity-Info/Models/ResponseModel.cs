namespace EntityInfoService.Models
{
    public class ResponseModel
    {
        public string Mode { get; set; } = "Verbose";
        public Object? Result { get; set; } = null;
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Summary { get; set; } = new List<string>();
        public Exception? Exception { get; set; } = null;
        public string ExecutionTime { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
