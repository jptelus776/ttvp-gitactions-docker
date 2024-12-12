namespace EntityInfoService.Models.OpusBackend.User
{
    // table user.workflow
    //     primary key (username, job_id)
    public class Workflow
    {
        // username       text,
        public string UserName { get; set; } = string.Empty;

        // job_id         text,
        public string JobId { get; set; } = string.Empty;

        // created_at     timestamp,
        public DateTime CreatedAt { get; set; }

        // job_status     text,
        public string JobStatus { get; set; } = string.Empty;

        // job_type       text,
        public string JobType { get; set; } = string.Empty;

        // pubsub_message text,
        public string PubSubMessage { get; set; } = string.Empty;
    }
}
