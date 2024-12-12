namespace EntityInfoService.Models.OpusBackend
{
    public class DbResultBase
    {
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Summary { get; set; } = new List<string>();
        public Exception? Exception { get; set; } = null;
    }
}
