using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.TelusMediaroomRecordings;
using MySql.Data.MySqlClient;
using Serilog;
using System.Data;
using System.Text;

namespace EntityInfoService.DAL.MySql
{
    public class TelusMediaroomRecordingDB
    {
        private static readonly string _schemaName = "telus_mediaroom_recordings";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Lpvr);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(TelusMediaroomRecordingDB));
        public List<RecordingDefinition> RecordingDefinitions { get; set; } = new List<RecordingDefinition>();
        public List<AccountSyncStatus> AccountSyncStatus { get; set; } = new List<AccountSyncStatus>();
        public List<UserScheduleVersion> UserScheduleVersions { get; set; } = new List<UserScheduleVersion>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmAccountId"></param>
        /// <returns></returns>
        public static DbResultList<RecordingDefinition> GetRecordingDefinitions(string crmAccountId)
        {
            string tableName = "recording_definitions";
            string query = "select * from " + tableName + " where crm_account_id=?";
            var result = new DbResultList<RecordingDefinition>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter param = new MySqlParameter("crm_account_id", MySqlDbType.String);
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
                                var rd = new RecordingDefinition();
                                rd.RecordingDefinitionId = reader.GetInt64(reader.GetOrdinal("recording_definition_id"));
                                rd.MrRecdefGuid = reader.IsDBNull(reader.GetOrdinal("mr_recdef_guid")) ? null : reader.GetString(reader.GetOrdinal("mr_recdef_guid"));
                                rd.CrmAccountId = reader.IsDBNull(reader.GetOrdinal("crm_account_id")) ? null : reader.GetString(reader.GetOrdinal("crm_account_id"));
                                rd.ChannelNumber = reader.IsDBNull(reader.GetOrdinal("channel_number")) ? null : reader.GetInt32(reader.GetOrdinal("channel_number"));
                                rd.FrequencyCode = reader.IsDBNull(reader.GetOrdinal("frequency_code")) ? null : reader.GetByte(reader.GetOrdinal("frequency_code"));
                                rd.KeepUntil = reader.IsDBNull(reader.GetOrdinal("keep_until")) ? null : reader.GetByte(reader.GetOrdinal("keep_until"));                                
                                rd.StateCode = reader.IsDBNull(reader.GetOrdinal("state_code")) ? null : reader.GetByte(reader.GetOrdinal("state_code"));
                                rd.StationId = reader.IsDBNull(reader.GetOrdinal("station_id")) ? null : reader.GetString(reader.GetOrdinal("station_id"));
                                rd.Title = reader.IsDBNull(reader.GetOrdinal("title")) ? null : reader.GetString(reader.GetOrdinal("title"));
                                rd.UtcStartTime = reader.IsDBNull(reader.GetOrdinal("utc_start_time")) ? null : reader.GetString(reader.GetOrdinal("utc_start_time"));
                                rd.AirtimeDomain = reader.IsDBNull(reader.GetOrdinal("airtime_domain")) ? null : reader.GetByte(reader.GetOrdinal("airtime_domain"));                                
                                rd.SeriesId = reader.IsDBNull(reader.GetOrdinal("series_id")) ? null : reader.GetString(reader.GetOrdinal("series_id"));
                                rd.ShowType = reader.IsDBNull(reader.GetOrdinal("show_type")) ? null : reader.GetByte(reader.GetOrdinal("show_type"));                                
                                rd.CreatedTime = reader.IsDBNull(reader.GetOrdinal("created_time")) ? null : reader.GetDateTime(reader.GetOrdinal("created_time"));
                                rd.UpdatedTime = reader.IsDBNull(reader.GetOrdinal("updated_time")) ? null : reader.GetDateTime(reader.GetOrdinal("updated_time"));

                                result.Records.Add(rd);
                            }

                            reader.Close();
                        }

                        conn.Close();

                        // Populate now recording for each recording definition
                        var recordings = GetMediaroomRecordings(result);
                        foreach (var rd in result.Records)
                        {
                            foreach(var rec in recordings.Records)
                            {
                                if (rd.RecordingDefinitionId == rec.RecordingDefinitionId)
                                {
                                    rd.Recordings.Add(rec);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with server {server}, schema {schema} and table {tableName}. Message: {message}", conn.DataSource, _schemaName, tableName, ex.Message, ex);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with server {server}, schema {schema} and table {tableName}. Message: {message}", conn.DataSource, _schemaName, tableName, ex.Message, ex);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        public static DbResultList<MediaroomRecording> GetMediaroomRecordings(DbResultList<RecordingDefinition> rdList)
        {
            if(rdList == null || rdList.Records.Count == 0)
            {
                return new DbResultList<MediaroomRecording>();
            }

            // Get list of recording Id's
            StringBuilder sb = new StringBuilder();
            foreach (var record in rdList.Records)
            {
                if (sb.Length == 0)
                {
                    sb.Append(record.RecordingDefinitionId.ToString());
                }
                else
                {
                    sb.Append(",");
                    sb.Append(record.RecordingDefinitionId.ToString());
                }
            }

            // Get all recordings for specified recording id's
            string tableName = "recordings";
            string query = $"select * from {tableName} where recording_definition_id in ({sb})";
            var result = new DbResultList<MediaroomRecording>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var rd = new MediaroomRecording();
                                rd.RecordingId = reader.GetInt64(reader.GetOrdinal("recording_id"));
                                rd.RecordingDefinitionId = reader.IsDBNull(reader.GetOrdinal("recording_definition_id")) ? null : reader.GetInt64(reader.GetOrdinal("recording_definition_id"));
                                rd.MrRecGuid = reader.IsDBNull(reader.GetOrdinal("mr_rec_guid")) ? null : reader.GetString(reader.GetOrdinal("mr_rec_guid"));
                                rd.DurationSeconds = reader.IsDBNull(reader.GetOrdinal("duration_seconds")) ? null : reader.GetInt32(reader.GetOrdinal("duration_seconds"));
                                rd.HardEndPadSeconds = reader.IsDBNull(reader.GetOrdinal("hard_end_pad_seconds")) ? null : reader.GetInt32(reader.GetOrdinal("hard_end_pad_seconds"));
                                rd.KeepUntill = reader.IsDBNull(reader.GetOrdinal("keep_untill")) ? null : reader.GetByte(reader.GetOrdinal("keep_untill"));
                                rd.StateCode = reader.IsDBNull(reader.GetOrdinal("state_code")) ? null : reader.GetByte(reader.GetOrdinal("state_code"));
                                rd.UtcStartTime = reader.IsDBNull(reader.GetOrdinal("utc_starttime")) ? null : reader.GetString(reader.GetOrdinal("utc_starttime"));
                                rd.ProgramId = reader.IsDBNull(reader.GetOrdinal("program_id")) ? null : reader.GetString(reader.GetOrdinal("program_id"));
                                rd.CreatedTime = reader.IsDBNull(reader.GetOrdinal("created_time")) ? null : reader.GetDateTime(reader.GetOrdinal("created_time"));
                                rd.UpdatedTime = reader.IsDBNull(reader.GetOrdinal("updated_time")) ? null : reader.GetDateTime(reader.GetOrdinal("updated_time"));
                                rd.Title = reader.IsDBNull(reader.GetOrdinal("title")) ? null : reader.GetString(reader.GetOrdinal("title"));
                                rd.Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString(reader.GetOrdinal("description"));
                                rd.RecordedKeepUntil = reader.IsDBNull(reader.GetOrdinal("recorded_keep_untill")) ? null : reader.GetByte(reader.GetOrdinal("keep_untill"));
                                rd.UtcActualStartTime = reader.IsDBNull(reader.GetOrdinal("utc_actual_starttime")) ? null : reader.GetString(reader.GetOrdinal("utc_actual_starttime"));
                                rd.UtcActualEndTime = reader.IsDBNull(reader.GetOrdinal("utc_actual_endtime")) ? null : reader.GetString(reader.GetOrdinal("utc_actual_endtime"));

                                result.Records.Add(rd);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with server {server}, schema {schema} and table {tableName}. Message: {message}", conn.DataSource, _schemaName, tableName, ex.Message, ex);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with server {server}, schema {schema} and table {tableName}. Message: {message}", conn.DataSource, _schemaName, tableName, ex.Message, ex);
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
        public static DbResultList<AccountSyncStatus> GetAccountSyncStatus(string crmAccountId)
        {
            string tableName = "account_sync_status";
            string query = "select * from " + tableName + " where crm_account_id=?";
            var result = new DbResultList<AccountSyncStatus>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {

                    MySqlParameter param = new MySqlParameter("crm_account_id", MySqlDbType.String);
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
                                var mru = new AccountSyncStatus();
                                mru.AccountSyncStatusId = reader.GetInt64(reader.GetOrdinal("account_sync_status_id"));
                                mru.CrmAccountId = reader.GetString(reader.GetOrdinal("crm_account_id"));
                                mru.CreationDate = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                mru.LastSyncDate = reader.IsDBNull(reader.GetOrdinal("last_sync_date")) ? null : reader.GetDateTime(reader.GetOrdinal("last_sync_date"));

                                result.Records.Add(mru);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with server {server}, schema {schema} and table {tableName}. Message: {message}", conn.DataSource, _schemaName, tableName, ex.Message, ex);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with server {server}, schema {schema} and table {tableName}. Message: {message}", conn.DataSource, _schemaName, tableName, ex.Message, ex);
                        MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmAccountId"></param>
        /// <returns></returns>
        public static DbResultList<UserScheduleVersion> GetUserScheduleVersions(string crmAccountId)
        {
            string tableName = "user_schedule_version";
            string query = "select * from " + tableName + " where crm_account_id=?";
            var result = new DbResultList<UserScheduleVersion>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter param = new MySqlParameter("crm_account_id", MySqlDbType.String);
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
                                var usv = new UserScheduleVersion();
                                usv.Id = reader.GetInt64(reader.GetOrdinal("id"));
                                usv.CrmAccountId = reader.GetString(reader.GetOrdinal("crm_account_id"));
                                usv.ScheduledVersion = reader.IsDBNull(reader.GetOrdinal("scheduled_version")) ? null : reader.GetInt32(reader.GetOrdinal("scheduled_version"));
                                usv.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                usv.LastRefreshedDate = reader.IsDBNull(reader.GetOrdinal("last_refreshed_date")) ? null : reader.GetDateTime(reader.GetOrdinal("last_refreshed_date"));
                                usv.IsGetInProgress = reader.IsDBNull(reader.GetOrdinal("is_get_in_progress")) ? null : reader.GetByte(reader.GetOrdinal("is_get_in_progress"));
                                usv.LockedTime = reader.IsDBNull(reader.GetOrdinal("locked_time")) ? null : reader.GetDateTime(reader.GetOrdinal("locked_time"));
                                usv.GetCallUpdatedDate = reader.IsDBNull(reader.GetOrdinal("get_call_updated_date")) ? null : reader.GetDateTime(reader.GetOrdinal("get_call_updated_date"));

                                result.Records.Add(usv);
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
            return MySqlHelper.GetSchemaVersions(_connectionString, _schemaName, "telus_db_version");
        }
    }
}
