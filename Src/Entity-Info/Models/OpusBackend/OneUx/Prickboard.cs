namespace EntityInfoService.Models.OpusBackend.OneUx
{
    //table oneux.prikbord
    public class Prikboard
    {
        // ID                      INT(10) auto_increment    primary key,
        public long Id { get; set; }

        // TITLE                   VARCHAR(30)  not null,
        public string Title { get; set; } = string.Empty;

        // SUBSCRIBERACCOUNTNUMBER VARCHAR(100) null,
        public string? SubscriberAccountNumber { get; set; } = null; //This is crmAccountId

        // PARENTALRATING          VARCHAR(200) null,
        public string? ParentalRating { get; set; } = null;

        // POSTERFILE              VARCHAR(100) null,
        public string? PosterFile { get; set; } = null;

        // LASTUPDATEDATETIME      DATETIME(19) not null,
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
