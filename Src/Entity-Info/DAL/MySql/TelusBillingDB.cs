using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.TelusBilling;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    public class TelusBillingDB
    {
        private static readonly string _schemaName = "telus_billing";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Main);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(TelusBillingDB));
        public List<BillingTransaction> BillingTransactions { get; set; } = new List<BillingTransaction>();

        public static DbResultList<BillingTransaction> GetBillingTransactions(string crmAccountId)
        {
            string tableName = "billing_transactions";
            string query = "select * from " + tableName + " where crm_account_id=?";
            var result = new DbResultList<BillingTransaction>();

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
                                var bt = new BillingTransaction();
                                bt.SequenceId = reader.GetInt32(reader.GetOrdinal("sequence_id"));
                                bt.CrmAccountId = reader.GetString(reader.GetOrdinal("crm_account_id"));
                                bt.ExternalId = reader.GetString(reader.GetOrdinal("external_id"));
                                bt.ExternalContentId = reader.GetString(reader.GetOrdinal("external_content_id"));
                                bt.StartDate = reader.GetDateTime(reader.GetOrdinal("start_date"));
                                bt.EndDate = reader.GetDateTime(reader.GetOrdinal("end_date"));
                                bt.PurchaseDate = reader.GetDateTime(reader.GetOrdinal("purchase_date"));
                                bt.AssetType = reader.GetString(reader.GetOrdinal("asset_type"));
                                bt.Status = reader.GetString(reader.GetOrdinal("status"));
                                bt.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                                bt.Title = reader.GetString(reader.GetOrdinal("title"));
                                bt.PcLevel = reader.GetString(reader.GetOrdinal("pc_level"));
                                bt.Platform = reader.GetString(reader.GetOrdinal("platform"));
                                bt.RentalPeriod = reader.GetInt32(reader.GetOrdinal("rental_period"));
                                bt.VideoType = reader.GetString(reader.GetOrdinal("video_type"));
                                bt.CreationDate = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                bt.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                bt.Property = reader.IsDBNull(reader.GetOrdinal("property")) ? null : reader.GetString(reader.GetOrdinal("property"));
                                bt.ShowId = reader.IsDBNull(reader.GetOrdinal("show_id")) ? null : reader.GetString(reader.GetOrdinal("show_id"));
                                bt.CallLetter = reader.IsDBNull(reader.GetOrdinal("call_letter")) ? null : reader.GetString(reader.GetOrdinal("call_letter"));

                                result.Records.Add(bt);
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
