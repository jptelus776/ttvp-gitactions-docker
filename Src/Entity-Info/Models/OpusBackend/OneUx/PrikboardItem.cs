namespace EntityInfoService.Models.OpusBackend.OneUx
{
    // table oneux.prikborditems
    public class PrikboardItem
    {
        // ID                 INT(10) auto_increment	primary key,	
        public long Id { get; set; }

        // TITLE              VARCHAR(500)                null,
        public string? Title { get; set; } = null;

        // PARENTID           INT(10)                     null,
        public long ParentId { get; set; }

        // PRIKBORDID         INT(10)                     not null,
        public long PrickBoardId { get; set; }

        // PARENTALRATING     VARCHAR(200)                null,
        public string? ParentalRating { get; set; }

        // POSTERFILE         VARCHAR(100)                null,
        public string? PosterFile { get; set; } = null;

        // REFERENCETYPE      VARCHAR(20)      default '' not null,
        public string? ReferenceType { get; set; } = string.Empty;

        // DESCRIPTION        VARCHAR(100)                null,
        public string? Description { get; set; } = null;

        // REFERENCE          VARCHAR(100)                null,
        public string? Reference { get; set; } = null;

        // FOLDERTYPE         VARCHAR(10)                 null,
        public string? FolderType { get; set; } = null;

        // CONTENTTYPE        VARCHAR(10)                 null,
        public string? ContentType { get; set; } = null;

        // DEPTH              INT(10)                     not null,
        public long? Depth { get; set; } = null;

        // TOBEDELETED        INT(10) UNSIGNED default 0  null,
        public ulong? ToBeDeleted { get; set; } = 0;

        // REFERENCETIME      BIGINT(19)                  null,
        public long? ReferenctTime { get; set; } = null;

        // PREMINUTES         INT(10)                     null,
        public long? PreMinutes { get; set; } = null;

        // REFERENCECHANNEL   VARCHAR(30)                 null,
        public string? ReferenceChannel { get; set; } = null;

        // SUBREFERENCETYPE   VARCHAR(20)                 null,
        public string? SubReferenceType { get; set; } = null;

        // DATESHOWN          BIGINT(19)                  null,
        public long? DateShown { get; set; } = null;

        // STATUS             VARCHAR(20)                 null,
        public string? Status { get; set; } = null;

        // PUBLIC             VARCHAR(1)                  null,
        public string? Public { get; set; } = null;

        // RANDOMID           INT(10)                     null,
        public long? RandomId { get; set; } = null;

        // LASTUPDATEDATETIME DATETIME(19)                not null
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
