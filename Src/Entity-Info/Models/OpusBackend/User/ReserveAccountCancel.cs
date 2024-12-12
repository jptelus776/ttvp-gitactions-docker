namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.reserve_account_cancel
    public class ReserveAccountCancel
    {
        // ID            int auto_increment	        primary key,
        public int Id { get; set; }

        // USER_ID       int                                 not null,
        public long UserId { get; set; }

        // CREATION_DATE timestamp default CURRENT_TIMESTAMP not null
        public DateTime CreationDate { get; set; }
    }
}
