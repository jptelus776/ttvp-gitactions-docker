namespace EntityInfoService.Models.OpusBackend.AvsConcurrentStreams
{
    //table avs_concurrent_streams.subscriberstreamusage
    // primary key (crmaccountid, streamsessionid)
    public class SubscriberStreamUsage
    {
        // crmaccountid    text,
        public string CrmAccountId { get; set; } = string.Empty;

        // streamsessionid uuid,
        public Guid StreamSessionId { get; set; } = Guid.Empty;

        // property        text,
        public string Property { get; set; } = string.Empty;

        // sessioninfo     map<text, text>,
        public Dictionary<string, string> SessionInfo { get; set; } = new Dictionary<string, string>();

        // username        text,
        public string UserName { get; set; } = string.Empty;
    }
}
