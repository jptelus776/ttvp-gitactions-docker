using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.AvsBookmark;
using Serilog;

namespace EntityInfoService.DAL.CassandraDB
{
    public class AvsBookmarkKeyspace
    {
        public static Cassandra.ISession? _session;
        private static string _schemaName = "avs_bookmark";

        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(AvsBookmarkKeyspace));

        public List<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
        public List<WatchHistory> WatchHistory { get; set; } = new List<WatchHistory>();

        public static DbResultList<Bookmark> GetBookmarks(string crmAccountId)
        {
            var result = new DbResultList<Bookmark>();
            string tableName = "bookmarks";
            try
            {
                var cluster = CassandraHelper.Cluster;

                if (cluster != null && _session == null)
                {
                    _session = cluster.Connect(_schemaName);
                    if (_session == null)
                    {
                        result.Errors.Add($"Not able to connect to schema {_schemaName}");
                        return result;
                    }

                    _logger.Information(@"Connected to {_schemaName} keyspace.", _schemaName);
                }
                else
                {
                    _logger.Information(@"Reusing exisitng connection to {_schemaName}", _schemaName);
                }

                if (_session != null)
                {
                    var query = "SELECT * FROM " + tableName + " where crmaccountid=?";

                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(crmAccountId);

                    _logger.Information(@"Executing {query} with crmAccountId={crmAccountId}", query, crmAccountId);
                    var rows = _session.Execute(boundStmt);

                    foreach (var row in rows)
                    {
                        var bm = new Bookmark();

                        bm.CrmAccountId = row.GetValue<string>("crmaccountid");
                        bm.AssetType = row.GetValue<string>("assettype");
                        bm.AssetId = row.GetValue<string>("assetid");
                        bm.BookmarkSetId = row.GetValue<Guid>("bookmarksetid");
                        bm.BookmarkId = row.GetValue<Guid>("bookmarkid");
                        bm.AssetPrivacy = row.GetValue<string>("assetprivacy");
                        bm.BookmarkTitle = row.GetValue<string>("bookmarktitle");
                        bm.BookmarkType = row.IsNull("bookmarktype") ? null : row.GetValue<string>("bookmarktype");
                        bm.CreatedBy = row.IsNull("createdby") ? null : row.GetValue<string>("createdby");
                        bm.CreatedDate = row.IsNull("createddate") ? null : row.GetValue<DateTime>("createddate");
                        bm.DeviceReference = row.IsNull("devicereference") ? null : row.GetValue<string>("devicereference");
                        bm.EpisodeNumber = row.IsNull("episodenumber") ? null : row.GetValue<int>("episodenumber");
                        bm.ExternalContentId = row.IsNull("externalcontentid") ? null : row.GetValue<string>("externalcontentid");
                        bm.Season = row.IsNull("season") ? null : row.GetValue<int>("season");
                        bm.StartDeltaTime = row.IsNull("startdeltatime") ? null : row.GetValue<long>("startdeltatime");
                        bm.UaId = row.IsNull("uaid") ? null : row.GetValue<string>("uaid");
                        bm.UaSeriesId = row.IsNull("uaseriesid") ? null : row.GetValue<string>("uaseriesid");
                        bm.UpdatedDate = row.IsNull("updateddate") ? null : row.GetValue<DateTime>("updateddate");
                        bm.UserName = row.IsNull("username") ? null : row.GetValue<string>("username");

                        result.Records.Add(bm);
                    }
                }
            }
            catch (QueryExecutionException ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                CassandraHelper.ProcessException(result, ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                CassandraHelper.ProcessException(result, ex);
            }

            return result;
        }

        public static DbResultList<WatchHistory> GetWatchHistory(string crmAccountId)
        {
            var result = new DbResultList<WatchHistory>();
            string tableName = "watchhistory";
            try
            {
                var cluster = CassandraHelper.Cluster;

                if (cluster != null && _session == null)
                {
                    _session = cluster.Connect(_schemaName);
                    if (_session == null)
                    {
                        result.Errors.Add($"Not able to connect to schema {_schemaName}");
                        return result;
                    }

                    _logger.Information(@"Connected to {_schemaName} keyspace.", _schemaName);
                }
                else
                {
                    _logger.Information(@"Reusing exisitng connection to {_schemaName}", _schemaName);
                }

                if (_session != null)
                {
                    var query = "SELECT * FROM " + tableName + " where crmaccountid=?";

                    var prepStmt = _session.Prepare(query);
                    var boundStmt = prepStmt.Bind(crmAccountId);

                    _logger.Information(@"Executing {query} with crmAccountId={crmAccountId}", query, crmAccountId);
                    var rows = _session.Execute(boundStmt);

                    foreach (var row in rows)
                    {
                        var wh = new WatchHistory();

                        wh.CrmAccountId = row.GetValue<string>("crmaccountid");
                        wh.AssetType = row.GetValue<string>("assettype");
                        wh.AssetId = row.GetValue<string>("assetid");
                        wh.BookmarkSetId = row.GetValue<Guid>("bookmarksetid");
                        wh.BookmarkId = row.GetValue<Guid>("bookmarkid");
                        wh.AssetPrivacy = row.GetValue<string>("assetprivacy");
                        wh.BookmarkTitle = row.GetValue<string>("bookmarktitle");
                        wh.BookmarkType = row.IsNull("bookmarktype") ? null : row.GetValue<string>("bookmarktype");
                        wh.CreatedBy = row.IsNull("createdby") ? null : row.GetValue<string>("createdby");
                        wh.CreatedDate = row.IsNull("createddate") ? null : row.GetValue<DateTime>("createddate");
                        wh.DeviceReference = row.IsNull("devicereference") ? null : row.GetValue<string>("devicereference");
                        wh.EpisodeNumber = row.IsNull("episodenumber") ? null : row.GetValue<int>("episodenumber");
                        wh.ExternalContentId = row.IsNull("externalcontentid") ? null : row.GetValue<string>("externalcontentid");
                        wh.Season = row.IsNull("season") ? null : row.GetValue<int>("season");
                        wh.StartDeltaTime = row.IsNull("startdeltatime") ? null : row.GetValue<long>("startdeltatime");
                        wh.UaId = row.IsNull("uaid") ? null : row.GetValue<string>("uaid");
                        wh.UaSeriesId = row.IsNull("uaseriesid") ? null : row.GetValue<string>("uaseriesid");
                        wh.UpdatedDate = row.IsNull("updateddate") ? null : row.GetValue<DateTime>("updateddate");
                        wh.UserName = row.IsNull("username") ? null : row.GetValue<string>("username");

                        result.Records.Add(wh);
                    }
                }
            }
            catch (QueryExecutionException ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                CassandraHelper.ProcessException(result, ex);
            }
            catch (Exception ex)
            {
                _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                CassandraHelper.ProcessException(result, ex);
            }

            return result;
        }

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return CassandraHelper.GetSchemaVersions(_session, "avs_bookmark", "avs_version", "telus_db_version");
        }
    }
}
