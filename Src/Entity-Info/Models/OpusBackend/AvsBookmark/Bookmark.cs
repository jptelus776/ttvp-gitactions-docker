namespace EntityInfoService.Models.OpusBackend.AvsBookmark
{
    // table avs_bookmark.bookmarks
    // primary key (crmaccountid, assettype, assetid, bookmarksetid, bookmarkid)
    public class Bookmark
    {
        // crmaccountid      text,
        public string CrmAccountId { get; set; } = string.Empty;

        // assettype         text,
        public string AssetType { get; set; } = string.Empty;

        // assetid           text,
        public string AssetId { get; set; } = string.Empty;

        // bookmarksetid     uuid,
        public Guid BookmarkSetId { get; set; } = Guid.Empty;

        // bookmarkid        uuid,
        public Guid BookmarkId { get; set; } = Guid.Empty;

        // assetprivacy      text,
        public string AssetPrivacy { get; set; } = string.Empty;

        // bookmarktitle     text,
        public string BookmarkTitle { get; set; } = string.Empty;

        // bookmarktype      text,
        public string? BookmarkType { get; set; } = null;

        // createdby         text,
        public string? CreatedBy { get; set; } = null;

        // createddate       timestamp,
        public DateTime? CreatedDate { get; set; } = null;

        // devicereference   text,
        public string? DeviceReference { get; set; } = null;

        // episodenumber     int
        public int? EpisodeNumber { get; set; } = null;

        // externalcontentid text,
        public string? ExternalContentId { get; set; } = null;

        // season            int,
        public int? Season { get; set; }

        // startdeltatime    bigint,
        public long? StartDeltaTime { get; set; } = null;

        // uaid              text,
        public string? UaId { get; set; } = null;

        // uaseriesid        text,
        public string? UaSeriesId { get; set; } = null;

        // updateddate       timestamp,
        public DateTime? UpdatedDate { get; set; } = null;

        // username          text,
        public string? UserName { get; set; } = null;

    }
}
