namespace EntityInfoService.Models.OpusBackend.OneUx
{
    // table oneux.user
    public class PrikboardUser
    {
        // ID                        INT(10) auto_increment     primary key,
        public long Id { get; set; }

        // PINBOARDID                INT(10) not null,
        public long PinboardId { get; set; }

        // FAVORITEPINBOARDITEMID    INT(10) not null,
        public long FavoritePinBoardItemId { get; set; }

        // BOOKMARKPINBOARDITEMID    INT(10) null,
        public long? BookmarkPinboardItemId { get; set; } = null;

        // REMINDERPINBOARDITEMID    INT(10) null,
        public long? ReminderPinBoardItemId { get; set; } = null;

        // LASTWATCHEDPINBOARDITEMID INT(10) null
        public long? LastWatchedPinboardItemId { get; set; } = null;
    }
}
