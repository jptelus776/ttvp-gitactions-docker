using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.Entitlement;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    public class EntitlementDB
    {
        private static readonly string _schemaName = "entitlement";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Main);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(EntitlementDB));
        public List<AccountCommercialPackage> AccountCommercialPackages { get; set; } = new List<AccountCommercialPackage>();
        public List<AccountTechnicalPackage> AccountTechnicalPackages { get; set; } = new List<AccountTechnicalPackage>();

        public static DbResultList<AccountCommercialPackage> GetAccountCommercialPackages(long userId)
        {
            string tableName = "account_commercial_pkg";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<AccountCommercialPackage>();

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
                        while (reader.Read())
                        {
                            var acp = new AccountCommercialPackage();
                            acp.AccountCommercialPkgIdentifier = reader.GetInt32(reader.GetOrdinal("account_commercial_pkg_identifier"));
                            acp.UserId = reader.IsDBNull(reader.GetOrdinal("user_id")) ? null : reader.GetInt32(reader.GetOrdinal("user_id"));
                            acp.CommercialPackageId = reader.IsDBNull(reader.GetOrdinal("commercial_package_id")) ? null : reader.GetInt64(reader.GetOrdinal("commercial_package_id"));
                            acp.ValidityPeriod = reader.IsDBNull(reader.GetOrdinal("validity_period")) ? null : reader.GetDateTime(reader.GetOrdinal("validity_period"));
                            acp.CreatedDate = reader.IsDBNull(reader.GetOrdinal("created_date")) ? null : reader.GetDateTime(reader.GetOrdinal("created_date"));
                            acp.UpdateDate = reader.IsDBNull(reader.GetOrdinal("updated_date")) ? null : reader.GetDateTime(reader.GetOrdinal("updated_date"));
                            acp.ExternalCommercialPackageId = reader.IsDBNull(reader.GetOrdinal("external_commerical_pacakge_id")) ? null : reader.GetString(reader.GetOrdinal("external_commerical_pacakge_id"));
                            acp.SolutionOfferType = reader.IsDBNull(reader.GetOrdinal("solution_offer_type")) ? null : reader.GetString(reader.GetOrdinal("solution_offer_type"));

                            result.Records.Add(acp);
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

            return result;
        }

        public static DbResultList<AccountTechnicalPackage> GetAccountTechnicalPackages(long userId)
        {
            string tableName = "account_technical_pkg";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<AccountTechnicalPackage>();

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
                                var atp = new AccountTechnicalPackage();
                                atp.AccountTechnicalPkgId = reader.GetInt64(reader.GetOrdinal("account_technical_pkg_id"));
                                atp.UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                                atp.CrmAccountId = reader.GetString(reader.GetOrdinal("crm_account_id"));
                                atp.TechPackageId = reader.GetInt32(reader.GetOrdinal("tech_package_id"));
                                atp.ViewsNumber = reader.IsDBNull(reader.GetOrdinal("views_number")) ? null : reader.GetInt32(reader.GetOrdinal("views_number"));
                                atp.TechPackageValue = reader.IsDBNull(reader.GetOrdinal("tech_package_value")) ? null : reader.GetString(reader.GetOrdinal("tech_package_value"));
                                atp.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                atp.ValidityPeriod = reader.IsDBNull(reader.GetOrdinal("validity_period")) ? null : reader.GetDateTime(reader.GetOrdinal("validity_period"));
                                atp.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                atp.SolutionOfferId = reader.GetInt32(reader.GetOrdinal("solution_offer_id"));
                                atp.PtSequenceId = reader.IsDBNull(reader.GetOrdinal("pt_sequence_id")) ? null : reader.GetInt32(reader.GetOrdinal("pt_sequence_id"));
                                atp.CommerceModel = reader.IsDBNull(reader.GetOrdinal("commerce_model")) ? null : reader.GetString(reader.GetOrdinal("commerce_model"));
                                atp.AssetId = reader.IsDBNull(reader.GetOrdinal("asset_id")) ? null : reader.GetString(reader.GetOrdinal("asset_id"));
                                atp.IsLocked = reader.IsDBNull(reader.GetOrdinal("is_locked")) ? null : reader.GetChar(reader.GetOrdinal("is_locked"));
                                atp.TechnicalPacakgeExternalId = reader.IsDBNull(reader.GetOrdinal("technical_package_external_id")) ? null : reader.GetString(reader.GetOrdinal("technical_package_external_id"));
                                result.Records.Add(atp);
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
