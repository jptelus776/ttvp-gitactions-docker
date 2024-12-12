namespace EntityInfoService.Models.OpusBackend.TokenManagement
{
    // table token_management.user_refresh_token
    // primary key (clientid, scope, user_id, device_id, platform)
    public class UserRefreshToken
    {
        // clientid      text,
        public string ClientId { get; set; } = string.Empty;

        // scope         text,
        public string Scope { get; set; } = string.Empty;

        // user_id       bigint,
        public long UserId { get; set; }

        // device_id     text,
        public string DeviceId { get; set; } = string.Empty;

        // platform      text,
        public string Platform { get; set; } = string.Empty;

        // access_token  text,
        public string? AccessToken { get; set; } = null;

        // refresh_token text,    
        public string? RefreshToken { get; set; } = null;
    }
}
