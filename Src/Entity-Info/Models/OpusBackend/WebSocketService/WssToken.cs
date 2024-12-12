namespace EntityInfoService.Models.OpusBackend.WebSocketService
{
    // table web_socket_service.wss_tokens
    //   primary key (connectionid, platform)
    public class WssToken
    {
        // connectionid text,
        public string ConnectionId { get; set; } = string.Empty;

        // platform     text,
        public string Platform { get; set; } = string.Empty;

        // wss_token    text,
        public string Token { get; set; } = string.Empty;

    }
}
