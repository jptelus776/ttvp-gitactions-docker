using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.OneUx;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    public class OneUxDB
    {
        private static readonly string _schemaName = "oneux";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Main);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(OneUxDB));
        public List<Prikboard> Prikboards { get; set; } = new List<Prikboard>();
        public List<PrikboardUser> Users { get; set; } = new List<PrikboardUser>();
        public List<PrikboardItem> PrikboardItems { get; set; } = new List<PrikboardItem>();

        public static DbResultList<Prikboard> GetPrikboards(string crmAccountId)
        {
            string tableName = "prikbord";
            string query = "select * from " + tableName + " where SUBSCRIBERACCOUNTNUMBER=?";
            var result = new DbResultList<Prikboard>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                using (var cmd = new MySqlCommand(query, conn))
                {
                    MySqlParameter param = new MySqlParameter("SUBSCRIBERACCOUNTNUMBER", MySqlDbType.String);
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
                                var pb = new Prikboard();
                                pb.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                                pb.Title = reader.GetString(reader.GetOrdinal("TITLE"));
                                pb.SubscriberAccountNumber = reader.IsDBNull(reader.GetOrdinal("SUBSCRIBERACCOUNTNUMBER")) ? null : reader.GetString(reader.GetOrdinal("SUBSCRIBERACCOUNTNUMBER"));
                                pb.ParentalRating = reader.IsDBNull(reader.GetOrdinal("PARENTALRATING")) ? null : reader.GetString(reader.GetOrdinal("PARENTALRATING"));
                                pb.PosterFile = reader.IsDBNull(reader.GetOrdinal("POSTERFILE")) ? null : reader.GetString(reader.GetOrdinal("POSTERFILE"));
                                pb.LastUpdatedDateTime = reader.GetDateTime(reader.GetOrdinal("LASTUPDATEDATETIME"));

                                result.Records.Add(pb);
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
            return MySqlHelper.GetSchemaVersions(_connectionString, _schemaName, "avs_version");
        }
    }
}
