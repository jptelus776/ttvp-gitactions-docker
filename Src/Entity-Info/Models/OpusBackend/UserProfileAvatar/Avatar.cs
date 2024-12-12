namespace EntityInfoService.Models.OpusBackend.UserProfileAvatar
{
    // table user_profile_avatar.avatar
    public class Avatar
    {
        // id                text primary key,
        public string Id { get; set; } = string.Empty;

        // category          text,
        public string Category { get; set; } = string.Empty;

        // created_date      timestamp,
        public DateTime? CreatedDate { get; set; } = null;

        // display_image     list<frozen<avatar_display_image_json>>, //map 
        public List<avatar_display_image_json> DisplayImage { get; set; } = new List<avatar_display_image_json>();

        // display_name      text,
        public string DisplayName { get; set; } = string.Empty;

        // last_updated_date timestamp,
        public DateTime? LastUpdatedDate { get; set; } = null;

        // status            text,
        public string Status { get; set; } = string.Empty;

        // type              text
        public string Type { get; set; } = string.Empty;
    }

    public class avatar_display_image_json
    {
        public string image_external_id { get; set; } = string.Empty;
        public string image_ratio { get; set; } = string.Empty;
        public string image_url { get; set; } = string.Empty;
    }
}
