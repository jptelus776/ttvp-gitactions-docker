using EntityInfoService.Models.OpusBackend.UserProfileAvatar;

namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.account_users_profile
    public class AccountUserProfile
    {
        // user_id                      int                                null,
        public long UserId { get; set; }

        // profile_name                 varchar(512)                       null,
        public string? ProfileName { get; set; } = null;

        // is_exit_proof_enabled        tinyint(1)                         null,
        public short? IsExitProofEnabled { get; set; } = null;

        // is_kids_profile              tinyint(1)                         null,
        public short? IsKidsProfile { get; set; } = null;

        // avatar_id                    varchar(512)                       null,
        public string? AvatarId { get; set; } = null;

        public Avatar? Avatar { get; set; } = null;

        // is_closed_captioning_enabled tinyint(1)                         null,
        public short? IsClosedCaptioningEnabled { get; set; } = null;

        // is_described_video_enabled   tinyint(1)                         null,
        public short? IsDescribedVideoEnabled { get; set; } = null;

        // is_entry_pin_enabled         tinyint(1)                         null,
        public short? IsEntryPinEnabled { get; set; } = null;

        // entry_pin                    varchar(255)                       null,
        public string? EntryPin { get; set; } = null;

        // creation_date                datetime default CURRENT_TIMESTAMP null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date                  datetime default CURRENT_TIMESTAMP null 
        public DateTime? UpdateDate { get; set; } = null;
    }
}
