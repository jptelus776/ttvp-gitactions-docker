using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Utils;
using MySql.Data.MySqlClient;
using Serilog;
using System.Runtime.CompilerServices;

namespace EntityInfoService.DAL.MySql
{
    public class MySqlHelper
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(MySqlHelper));

        // MAIN Galera/CloudSQL instance
        public static string? MyMainHost { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_MAIN_HOST", "localhost", false);
        public static string? MyMainPort { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_MAIN_PORT", "3306", isRequired: false);
        public static string? MyMainUser { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_MAIN_USER", "", isRequired: false);
        public static string? MyMainPassword { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_MAIN_PASSWORD", "", isRequired: false);
        
        // NPVR Galera/CloudSQL instance
        public static string? MyNpvrHost { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_NPVR_HOST", "localhost", isRequired: false);
        public static string? MyNpvrPort { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_NPVR_PORT", "3306", isRequired: false);
        public static string? MyNpvrUser { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_NPVR_USER", "", isRequired: false);
        public static string? MyNpvrPassword { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_NPVR_PASSWORD", "", isRequired: false);

        // LPVR Galera/CloudSQL instance
        public static string? MyLpvrHost { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_LPVR_HOST", "localhost", isRequired: false);
        public static string? MyLpvrPort { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_LPVR_PORT", "3306", isRequired: false);        
        public static string? MyLpvrUser { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_LPVR_USER", "", isRequired: false);        
        public static string? MyLpvrPassword { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_LPVR_PASSWORD", "", isRequired: false);        

        public static string? MyUseSsl { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_USE_SSL", "False", isRequired: false);
        public static string? MyCertPath { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("MY_CERT_PATH", "", isRequired: false);

        public static void InitializeEnvVariables()
        {
            try
            {
                _logger.Information(@"MySQL Environment Variables : " +
                                    @"Main Host:{MyMainHost}, Main User: {MyMainUser}, Main Passwrod: {MyMainPassword}, Main Port: {MyMainPort}, " +
                                    @"NPVR Host: {MyNpvrHost},  NPVR User: {MyNpvrUser}, NPVR Passwrod: {MyNpvrPassword}, NPVR Port: {MyNpvrPort}, " +
                                    @"LPVR Host: {MyLpvrHost},  LPVR User: {MyLpvrUser}, LPVR Passwrod: {MyLpvrPassword}, LPVR Port: {MyLpvrPort}, " +
                                    @"Cert Path: {MyCertPath}, Use SSL: {MyUseSsl}",
                                    MyMainHost, MyMainUser, !string.IsNullOrEmpty(MyMainPassword) ? MyMainPassword.Replace(MyMainPassword, "****") : string.Empty, MyMainPort,
                                    MyNpvrHost, MyNpvrUser, !string.IsNullOrEmpty(MyNpvrPassword) ? MyNpvrPassword.Replace(MyNpvrPassword, "****") : string.Empty, MyLpvrPort,
                                    MyLpvrHost, MyLpvrUser, !string.IsNullOrEmpty(MyLpvrPassword) ? MyLpvrPassword.Replace(MyLpvrPassword, "****") : string.Empty, MyLpvrPort,
                                    MyCertPath, MyUseSsl);

            }
            catch (Exception ex)
            {
                _logger.Error("Error reading MySQL environment variables. Message - {message}", ex.Message);
            }
        }

        public static string GetConnectionString(string database, DbServerType serverType)
        {
            string _connstring = string.Empty;

            if (serverType == DbServerType.Main)
            {
                _connstring = $"Server={MyMainHost};Port={MyMainPort};Database={database};Uid={MyMainUser};Pwd={MyMainPassword};";
            }

            if (serverType == DbServerType.Npvr)
            {
                _connstring = $"Server={MyNpvrHost};Port={MyNpvrPort};Database={database};Uid={MyNpvrUser};Pwd={MyNpvrPassword};";
            }

            if (serverType == DbServerType.Lpvr)
            {
                _connstring = $"Server={MyLpvrHost};Port={MyLpvrPort};Database={database};Uid={MyLpvrUser};Pwd={MyLpvrPassword};";
            }

            if (!string.IsNullOrEmpty(MyUseSsl) && MyUseSsl.ToLower().Equals("true"))
            {
                _connstring = _connstring + "SSL Mode=Preferred;";
            }

            if (!string.IsNullOrEmpty(MyCertPath))
            {
                _connstring = _connstring + "";
            }

            return _connstring;
        }

        internal static DbResultList<SchemaVersion> GetSchemaVersions(string _connstring, string schema, params string[] tables)
        {
            DbResultList<SchemaVersion> result = new DbResultList<SchemaVersion>();

            foreach (string table in tables)
            {
                string query = $"select * from {table};";
                using (var conn = new MySqlConnection(_connstring))
                {
                    try
                    {
                        var cmd = new MySqlCommand(query, conn);
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var sv = new SchemaVersion();
                                sv.SchemaName = schema;
                                sv.TableName = table;

                                // The reason why this many try/catch is implemented becuase `telus_client_files_generation.avs_version and telus_db_version` columns are not consistent.

                                try
                                {
                                    sv.AvsRelease = reader.IsDBNull(reader.GetOrdinal("avs_release")) ? null : reader.GetString(reader.GetOrdinal("avs_release"));
                                } catch (Exception ex) {
                                    _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                                    sv.AvsRelease= null;
                                }

                                try
                                {
                                    sv.AvsLastIncremental = reader.IsDBNull(reader.GetOrdinal("avs_last_incremental")) ? null : reader.GetInt32(reader.GetOrdinal("avs_last_incremental"));
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                                    sv.AvsLastIncremental = null;
                                }

                                try
                                {
                                    sv.AvsStartIncremental = reader.IsDBNull(reader.GetOrdinal("avs_start_incremental")) ? null : reader.GetInt32(reader.GetOrdinal("avs_start_incremental"));
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                                    sv.AvsStartIncremental = null;
                                }

                                try
                                {
                                    sv.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                                    sv.CreationDate = null;
                                }
                                try
                                {
                                    sv.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                                    sv.UpdateDate = null;
                                }

                                result.Records.Add(sv);
                            }

                            reader.Close();
                        }

                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                        ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", schema, table, ex.Message);
                        ProcessException(conn, result, ex);
                    }
                }
            }

            return result;
        }

        internal static DbResultList<string> CheckConnectionStatus(string _connstring, string schema)
        {
            DbResultList<string> result = new DbResultList<string>();

            using (var conn = new MySqlConnection(_connstring))
            {
                try
                {
                    conn.Open();
                    _logger.Information("Connection successfully opened for {server}.{schema}", conn.Database, schema);
                    result.Records.Add($"Connection successfully opened for {conn.Database}.{schema}");
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    _logger.Error("Caught Exception while accessing {server}.{schema}. Message: {message}", conn.DataSource, conn.Database, ex.Message);
                    ProcessException(conn, result, ex);
                }
                catch (Exception ex)
                {
                    _logger.Error("Caught Exception while accessing {server}.{schema}. Message: {message}", conn.DataSource, conn.Database, ex.Message);
                    ProcessException(conn, result, ex);
                }
            }

            return result;
        }

        internal static void ProcessException(MySqlConnection connection, DbResultBase result, Exception ex, [CallerMemberName] string? memberName = null)
        {
            if (result != null)
            {
                string? db = string.Empty;
                if (connection != null)
                {
                    db = $"Database: {connection.DataSource}.{connection.Database}, ";
                }

                var msg = string.IsNullOrEmpty(memberName) ? $"{db}Message: {ex.Message}" : $"{db}Method: {memberName}, Message: {ex.Message}";
                result.Errors.Add(msg);
            }

            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        internal static DbResultList<string> GetSchemas(string _connstring)
        {
            DbResultList<string> result = new DbResultList<string>();

            using (var conn = new MySqlConnection(_connstring))
            {
                try
                {
                    var cmd = new MySqlCommand("show databases", conn);
                    conn.Open();
                    _logger.Information("Connection successfully opened for {server}", conn.Database);
                    result.Records.Add($"Connection successfully opened for {conn.Database}");
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Records.Add(reader.GetString(0));
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    _logger.Error("Caught Exception while accessing {server}. Message: {message}", conn.DataSource, ex.Message);
                    ProcessException(conn, result, ex);
                }
                catch (Exception ex)
                {
                    _logger.Error("Caught Exception while accessing {server}. Message: {message}", conn.DataSource, ex.Message);
                    ProcessException(conn, result, ex);
                }
            }

            return result;
        }

        internal static DbResultList<string> GetSchemaTables(string _connstring, string _schema)
        {
            DbResultList<string> result = new DbResultList<string>();

            using (var conn = new MySqlConnection(_connstring))
            {
                try
                {
                    var cmd = new MySqlCommand("show full tables where table_type != 'VIEW'", conn);
                    conn.Open();
                    _logger.Information("Connection successfully opened for {server}.{schema}", conn.Database, _schema);
                    result.Records.Add($"Connection successfully opened for {conn.Database}.{_schema}");
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Records.Add(reader.GetString(0));
                    }

                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    _logger.Error("Caught Exception while accessing {server}.{schema}. Message: {message}", conn.DataSource, conn.Database, ex.Message);
                    ProcessException(conn, result, ex);
                }
                catch (Exception ex)
                {
                    _logger.Error("Caught Exception while accessing {server}.{schema}. Message: {message}", conn.DataSource, conn.Database, ex.Message);
                    ProcessException(conn, result, ex);
                }
            }

            return result;
        }

        internal static DbResultList<TableRecordCount> GetTableRecordCount(string _connstring, string _schema, string _tableName)
        {
            DbResultList<TableRecordCount> result = new DbResultList<TableRecordCount>();

            using (var conn = new MySqlConnection(_connstring))
            {
                try
                {
                    var cmd = new MySqlCommand($"select count(*) as count from {_tableName}", conn);
                    conn.Open();
                    var cnt = cmd.ExecuteScalar();
                    var item = new TableRecordCount()
                    {
                        Schema = _schema,
                        Table = _tableName,
                        Count = (long)cnt,
                        ElapsedTime = 0,
                        Timestamp = DateTime.UtcNow
                    };

                    result.Records.Add(item);

                    _logger.Information("Connection successfully opened for {server}.{schema}", conn.Database, _schema);
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    _logger.Error("Caught Exception while accessing {server}.{schema}. Message: {message}", conn.DataSource, conn.Database, ex.Message);
                    ProcessException(conn, result, ex);
                }
                catch (Exception ex)
                {
                    _logger.Error("Caught Exception while accessing {server}.{schema}. Message: {message}", conn.DataSource, conn.Database, ex.Message);
                    ProcessException(conn, result, ex);
                }
            }

            return result;
        }
    }

    public enum DbServerType
    {
        Main,
        Npvr,
        Lpvr
    }
}
