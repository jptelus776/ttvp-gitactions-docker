namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.user_ticket
    public class UserTicket
    {
        // ticket_number        int(22) auto_increment        primary key,
        public long TicketNumber { get; set; }

        // user_id              int           null,
        public long? UserId { get; set; } = null;

        // name                 varchar(256)  null,
        public string? Name { get; set; } = null;

        // email                varchar(255)  null,
        public string? Email { get; set; } = null;

        // claim_type           varchar(70)   null,
        public string? ClaimType { get; set; } = null;

        // message              varchar(1000) null,
        public string? Message { get; set; } = null;

        // creation_date        datetime      null,
        public DateTime? CreationDate { get; set; } = null;

        // update_date          datetime      null,
        public DateTime? UpdateDate { get; set; } = null;

        // status_id            int           null,
        public int? StatusId { get; set; } = null;

        // user_status_name     varchar(100)  null,
        public string? UserStatusName { get; set; } = null;

        // subscription_details varchar(255)  null,
        public string? SubscriptionDetails { get; set; } = null;

        // additional_info      varchar(4000) null
        public string? AdditionalInfo { get; set; } = null;
    }
}
