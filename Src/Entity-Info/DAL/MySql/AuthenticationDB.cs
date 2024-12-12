using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.Authentication;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    public class AuthenticationDB
    {
        private static readonly string _schemaName = "authentication";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Main);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(AuthenticationDB));
        public List<AccountCredential> AccountCredential { get; set; } = new List<AccountCredential>();
        public List<AccountDevice> AccountDevices { get; set; } = new List<AccountDevice>();

        public static DbResultList<AccountCredential> GetAccountCredential(long userId)
        {
            var tableName = "account_credential";
            string query = "select * from " + tableName + " where user_id=?";

            var result = new DbResultList<AccountCredential>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = new MySqlCommand(query, conn);

                MySqlParameter param = new MySqlParameter("user_id", MySqlDbType.Int64);
                param.Value = userId;
                cmd.Parameters.Add(param);
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var ac = new AccountCredential();
                        ac.UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                        ac.UserName = reader.IsDBNull(reader.GetOrdinal("username")) ? null : reader.GetString(reader.GetOrdinal("username"));
                        ac.Credential = reader.IsDBNull(reader.GetOrdinal("credential")) ? null : reader.GetString(reader.GetOrdinal("credential"));
                        ac.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                        ac.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                        ac.EncryptionAlgorithm = reader.IsDBNull(reader.GetOrdinal("encryption_algorithm")) ? null : reader.GetString(reader.GetOrdinal("encryption_algorithm"));
                        ac.EncryptionSaltKey = reader.IsDBNull(reader.GetOrdinal("encryption_salt_key")) ? null : reader.GetString(reader.GetOrdinal("encryption_salt_key"));
                        ac.Blacklist = reader.IsDBNull(reader.GetOrdinal("blacklist")) ? null : reader.GetChar(reader.GetOrdinal("blacklist"));
                        ac.RetailerId = reader.IsDBNull(reader.GetOrdinal("retailer_id")) ? null : reader.GetString(reader.GetOrdinal("retailer_id"));
                        result.Records.Add(ac);
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

            return result;
        }

        public static DbResultList<AccountDevice> GetAccountDevices(long userId)
        {
            var result = new DbResultList<AccountDevice>();
            string tableName = "account_device";
            string query = "select * from " + tableName + " where user_id=?";

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter param = new MySqlParameter("user_id", MySqlDbType.Int64);
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
                                var ad = new AccountDevice();
                                ad.UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                                ad.DeviceId = reader.GetString(reader.GetOrdinal("device_id"));
                                ad.TypeId = reader.IsDBNull(reader.GetOrdinal("type_id")) ? null : reader.GetInt64(reader.GetOrdinal("type_id"));
                                ad.PlatformId = reader.IsDBNull(reader.GetOrdinal("platform_id")) ? null : reader.GetString(reader.GetOrdinal("platform_id"));
                                ad.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                ad.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                ad.SecretKey = reader.IsDBNull(reader.GetOrdinal("secret_key")) ? null : reader.GetString(reader.GetOrdinal("secret_key"));

                                result.Records.Add(ad);
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

        public static DbResultList<string> GetConnectionStatus()
        {
            return MySqlHelper.CheckConnectionStatus(_connectionString, _schemaName);
        }

        public static DbResultList<SchemaVersion> GetSchemaVersions()
        {
            return MySqlHelper.GetSchemaVersions(_connectionString, _schemaName, "avs_version");
        }
    }
}
