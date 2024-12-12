using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.Npvrbe;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    public class NpvrbeDB
    {
        private static readonly string _schemaName = "npvrbe";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Npvr);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(NpvrbeDB));

        /// <summary>
        /// 
        /// </summary>
        public List<NpvrUserDetail> NpvrUserDetails { get; set; } = new List<NpvrUserDetail>();

        /// <summary>
        /// 
        /// </summary>
        public List<ReportUserRecord> ReportUserRecords { get; set; } = new List<ReportUserRecord>();

        /// <summary>
        /// 
        /// </summary>
        public List<UserChannel> UserChannels { get; set; } = new List<UserChannel>();

        /// <summary>
        /// 
        /// </summary>
        public List<UserNpvrRecording> UserNpvrRecordings { get; set; } = new List<UserNpvrRecording>();

        /// <summary>
        /// 
        /// </summary>
        public List<UserNpvrRecordingStopDelta> UserNpvrRecordingStopDeltas { get; set; } = new List<UserNpvrRecordingStopDelta>();

        /// <summary>
        /// .
        /// </summary>
        public List<UserNpvrSeriesRecording> UserNpvrSeriesRecordings { get; set; } = new List<UserNpvrSeriesRecording>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmAccountId"></param>
        /// <returns></returns>
        public static DbResultList<NpvrUserDetail> GetNpvrUserDetails(string crmAccountId)
        {
            string tableName = "npvruserdetails";
            string query = "select * from " + tableName + " where ACCOUNTID=?";
            var result = new DbResultList<NpvrUserDetail>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {

                    MySqlParameter param = new MySqlParameter("ACCOUNTID", MySqlDbType.String);
                    param.Value = crmAccountId;
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var nud = new NpvrUserDetail();
                                nud.Id = reader.GetInt64(reader.GetOrdinal("ID"));
                                nud.AccountId = reader.GetString(reader.GetOrdinal("ACCOUNTID"));
                                nud.ContextId = reader.GetString(reader.GetOrdinal("CONTEXTID"));
                                nud.ConcurrentRecordingsLimit = reader.IsDBNull(reader.GetOrdinal("CONCURRENTRECORDINGSLIMIT")) ? null : reader.GetByte(reader.GetOrdinal("CONCURRENTRECORDINGSLIMIT"));
                                nud.TotalMinutes = reader.GetInt32(reader.GetOrdinal("TOTALMINUTES"));
                                nud.NpvrKeepDays = reader.IsDBNull(reader.GetOrdinal("NPVRKEEPDAYS")) ? null : reader.GetInt16(reader.GetOrdinal("NPVRKEEPDAYS"));
                                nud.EnabledSeries = reader.GetByte(reader.GetOrdinal("ENABLEDSERIES"));
                                nud.CreationDate = reader.GetDateTime(reader.GetOrdinal("CREATION_DATE"));
                                nud.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UPDATE_DATE"));
                                result.Records.Add(nud);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<UserChannel> GetUserChannels(long userId)
        {
            string tableName = "userchannels";
            string query = "select * from " + tableName + " where USERID=?";
            var result = new DbResultList<UserChannel>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter param = new MySqlParameter("USERID", MySqlDbType.Int64);
                    param.Value = userId;
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var uc = new UserChannel();
                                uc.UserId = reader.GetInt64(reader.GetOrdinal("USERID"));
                                uc.ChannelId = reader.GetInt64(reader.GetOrdinal("CHANNELID"));
                                uc.CreationDate = reader.GetDateTime(reader.GetOrdinal("CREATION_DATE"));
                                uc.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UPDATE_DATE"));
                                result.Records.Add(uc);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<ReportUserRecord> GetReportUserRecords(long userId)
        {
            string tableName = "reportuserrecord";
            string query = "select * from " + tableName + " where USERID=?";
            var result = new DbResultList<ReportUserRecord>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter param = new MySqlParameter("USERID", MySqlDbType.Int64);
                    param.Value = userId;
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var rur = new ReportUserRecord();
                                rur.Id = reader.GetInt32(reader.GetOrdinal("id"));
                                rur.RecordId = reader.GetInt64(reader.GetOrdinal("RECORDID"));
                                rur.UserId = reader.GetInt64(reader.GetOrdinal("USERID"));
                                rur.UserStartDelta = reader.GetInt64(reader.GetOrdinal("USERSTARTDELTA"));
                                rur.UserStopDelta = reader.GetInt64(reader.GetOrdinal("USERSTOPDELTA"));
                                rur.IsFailed = reader.GetByte(reader.GetOrdinal("ISFAILED"));
                                rur.Status = reader.GetString(reader.GetOrdinal("STATUS"));
                                rur.StatusDetails = reader.GetString(reader.GetOrdinal("STATUSDETAILS"));
                                rur.DeleteWhenSpaceNeeded = reader.GetByte(reader.GetOrdinal("DELETEWHENSPACENEEDED"));
                                rur.MarkedForDelete = reader.GetByte(reader.GetOrdinal("MARKEDFORDELETE"));
                                rur.ActualRecordingSeconds = reader.GetInt64(reader.GetOrdinal("ACTUALRECORDINGSECONDS"));
                                rur.EpisodeScope = reader.IsDBNull(reader.GetOrdinal("EPISODESCOPE")) ? null : reader.GetString(reader.GetOrdinal("EPISODESCOPE"));
                                rur.SeriesId = reader.IsDBNull(reader.GetOrdinal("SERIESID")) ? null : reader.GetInt64(reader.GetOrdinal("SERIESID"));
                                rur.ChannelId = reader.GetInt64(reader.GetOrdinal("CHANNELID"));
                                rur.SeriesDelteWhenSpaceNeeded = reader.IsDBNull(reader.GetOrdinal("SERIES_DELETEWHENSPACENEEDED")) ? null : reader.GetByte(reader.GetOrdinal("SERIES_DELETEWHENSPACENEEDED"));
                                rur.ChannelBound = reader.IsDBNull(reader.GetOrdinal("CHANNELBOUND")) ? null : reader.GetByte(reader.GetOrdinal("CHANNELBOUND"));
                                rur.EpisodesToKeep = reader.IsDBNull(reader.GetOrdinal("EPISODESTOKEEP")) ? null : reader.GetInt16(reader.GetOrdinal("EPISODESTOKEEP"));
                                rur.SeriesInfo = reader.IsDBNull(reader.GetOrdinal("SERIESREFNO")) ? null : reader.GetString(reader.GetOrdinal("SERIESREFNO"));
                                rur.Title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? null : reader.GetString(reader.GetOrdinal("TITLE"));
                                rur.ChannelRefNumber = reader.IsDBNull(reader.GetOrdinal("CHANNELREFNUMBER")) ? null : reader.GetString(reader.GetOrdinal("CHANNELREFNUMBER"));
                                rur.ProgramExternaId = reader.GetString(reader.GetOrdinal("PROGRAMEXTERNALID"));
                                rur.ProgramDuration = reader.GetInt16(reader.GetOrdinal("PROGRAMDURATION"));
                                rur.ProgramStartTime = reader.GetInt64(reader.GetOrdinal("PROGRAMSTARTTIME"));
                                rur.RecordingStartTime = reader.GetInt64(reader.GetOrdinal("RECORDINGSTARTTIME"));
                                rur.EnableNpvrTrickPlay = reader.IsDBNull(reader.GetOrdinal("ENABLENPVRTRICKPLAY")) ? null : reader.GetByte(reader.GetOrdinal("ENABLENPVRTRICKPLAY"));
                                rur.EnableNpvrSkipJump = reader.IsDBNull(reader.GetOrdinal("ENABLENPVRSKIPJUMP")) ? null : reader.GetByte(reader.GetOrdinal("ENABLENPVRSKIPJUMP"));
                                rur.EventUpdateStatus = reader.IsDBNull(reader.GetOrdinal("EVENTUPDATESTATUS")) ? null : reader.GetString(reader.GetOrdinal("EVENTUPDATESTATUS"));
                                rur.ScheduleId = reader.IsDBNull(reader.GetOrdinal("SCHEDULEID")) ? null : reader.GetString(reader.GetOrdinal("SCHEDULEID"));
                                rur.CreationDate = reader.GetDateTime(reader.GetOrdinal("CREATION_DATE"));
                                rur.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UPDATE_DATE"));
                                result.Records.Add(rur);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<UserNpvrRecording> GetUserNpvrRecordings(long userId)
        {
            string tableName = "usernpvrrecording";
            string query = "select * from " + tableName + " where USERID=?";
            var result = new DbResultList<UserNpvrRecording>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {

                    MySqlParameter param = new MySqlParameter("USERID", MySqlDbType.Int64);
                    param.Value = userId;
                    cmd.Parameters.Add(param);
                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var unr = new UserNpvrRecording();
                                unr.RecordId = reader.GetInt64(reader.GetOrdinal("RECORDID"));
                                unr.UserId = reader.GetInt64(reader.GetOrdinal("USERID"));
                                unr.UserStartDelta = reader.GetInt64(reader.GetOrdinal("USERSTARTDELTA"));
                                unr.IsFailed = reader.GetByte(reader.GetOrdinal("ISFAILED"));
                                unr.Status = reader.GetString(reader.GetOrdinal("STATUS"));
                                unr.StatusDetails = reader.GetString(reader.GetOrdinal("STATUSDETAILS"));
                                unr.DeleteWhenSpaceNeeded = reader.GetByte(reader.GetOrdinal("DELETEWHENSPACENEEDED"));
                                unr.SeriesId = reader.IsDBNull(reader.GetOrdinal("SERIESID")) ? null : reader.GetInt64(reader.GetOrdinal("SERIESID"));
                                unr.UserStopDelta = reader.GetInt64(reader.GetOrdinal("USERSTOPDELTA"));
                                unr.MarkedForDelete = reader.GetByte(reader.GetOrdinal("MARKEDFORDELETE"));
                                unr.ActualRecordingSeconds = reader.GetInt64(reader.GetOrdinal("ACTUALRECORDINGSECONDS"));
                                unr.CreationDate = reader.GetDateTime(reader.GetOrdinal("CREATION_DATE"));
                                unr.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UPDATE_DATE"));
                                unr.UserName = reader.IsDBNull(reader.GetOrdinal("USERNAME")) ? null : reader.GetString(reader.GetOrdinal("USERNAME"));
                                result.Records.Add(unr);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<UserNpvrRecordingStopDelta> GetUserNpvrRecordingStopDelta(long userId)
        {
            string tableName = "usernpvrrecordingstopdelta";
            string query = "select * from " + tableName + " where USERID=?";

            var result = new DbResultList<UserNpvrRecordingStopDelta>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {

                    MySqlParameter param = new MySqlParameter("USERID", MySqlDbType.Int64);
                    param.Value = userId;
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var unrsd = new UserNpvrRecordingStopDelta();
                                unrsd.RecordId = reader.GetInt64(reader.GetOrdinal("RECORDID"));
                                unrsd.UserId = reader.GetInt64(reader.GetOrdinal("USERID"));
                                unrsd.UserName = reader.GetString(reader.GetOrdinal("USERNAME"));
                                unrsd.MarkedForDelete = reader.GetByte(reader.GetOrdinal("MARKEDFORDELETE"));
                                unrsd.UserStopDelta = reader.GetInt64(reader.GetOrdinal("USERSTOPDELTA"));
                                unrsd.UserStartDelta = reader.GetInt64(reader.GetOrdinal("USERSTARTDELTA"));
                                unrsd.ActualRecordingSeconds = reader.GetInt64(reader.GetOrdinal("ACTUALRECORDINGSECONDS"));
                                unrsd.CreationDate = reader.GetDateTime(reader.GetOrdinal("CREATION_DATE"));
                                unrsd.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UPDATE_DATE"));
                                result.Records.Add(unrsd);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<UserNpvrSeriesRecording> GeUserNpvrSeriesRecordings(long userId)
        {
            string tableName = "usernpvrrecordingstopdelta";
            string query = "select * from " + tableName + " where USERID=?";
            var result = new DbResultList<UserNpvrSeriesRecording>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {

                    MySqlParameter param = new MySqlParameter("USERID", MySqlDbType.Int64);
                    param.Value = userId;
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var unsr = new UserNpvrSeriesRecording();
                                unsr.SeriesId = reader.GetInt64(reader.GetOrdinal("SERIESID"));
                                unsr.UserId = reader.GetInt64(reader.GetOrdinal("USERID"));
                                unsr.ChannelId = reader.GetInt64(reader.GetOrdinal("CHANNELID"));
                                unsr.DeleteWhenSpaceNeeded = reader.GetByte(reader.GetOrdinal("DELETEWHENSPACENEEDED"));
                                unsr.ChannelBound = reader.GetByte(reader.GetOrdinal("CHANNELBOUND"));
                                unsr.EpisodeScope = reader.GetString(reader.GetOrdinal("EPISODESCOPE"));
                                unsr.EpisodeToKeep = reader.GetInt16(reader.GetOrdinal("EPISODESTOKEEP"));
                                unsr.ScheduledTimestamp = reader.GetDateTime(reader.GetOrdinal("SCHEDULETIMESTAMP"));
                                unsr.MarkedForDelete = reader.GetByte(reader.GetOrdinal("MARKEDFORDELETE"));
                                unsr.CreationDate = reader.GetDateTime(reader.GetOrdinal("CREATION_DATE"));
                                unsr.UpdateDate = reader.GetDateTime(reader.GetOrdinal("UPDATE_DATE"));
                                unsr.UserName = reader.IsDBNull(reader.GetOrdinal("USERNAME")) ? null : reader.GetString(reader.GetOrdinal("USERNAME"));
                                result.Records.Add(unsr);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return MySqlHelper.GetSchemaVersions(_connectionString, _schemaName, "avs_version", "telus_db_version");
        }
    }
}
