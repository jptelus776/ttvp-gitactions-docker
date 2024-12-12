using EntityInfoService.Models.OpusBackend.WebSocketService;

namespace EntityInfoService.Models.OpusBackend.PushMessageNotification
{
    //table push_message_notification.failed_message
    //     primary key (id, connectionid, platform)
    public class FailedMessage
    {
        // id           uuid,
        public Guid Id { get; set; } = Guid.Empty;

        // connectionid text,
        public string ConnectionId { get; set; } = string.Empty;

        // platform     text,
        public string Platform { get; set; } = string.Empty;

        // deviceid     text,
        public string DeviceId { get; set; } = string.Empty;

        // messageid    bigint,
        public long MessageId { get; set; }

        // triggername  text,
        public string TriggerName { get; set; } = string.Empty;

        public WssToken? WssToken { get; set; } = null;
    }


}
