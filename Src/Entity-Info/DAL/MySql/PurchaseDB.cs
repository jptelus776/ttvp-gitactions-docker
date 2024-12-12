using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.Purchase;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    public class PurchaseDB
    {
        private static readonly string _schemaName = "purchase";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Main);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(PurchaseDB));
        public List<PurchaseTransaction> PurchaseTransactions { get; set; } = new List<PurchaseTransaction>();

        public static DbResultList<PurchaseTransaction> GetPurchaseTransactions(long userId)
        {
            string tableName = "purchase_transaction";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<PurchaseTransaction>();

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
                                var pt = new PurchaseTransaction();
                                pt.SequenceId = reader.GetInt32(reader.GetOrdinal("sequence_id"));
                                pt.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                pt.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                pt.Currency = reader.IsDBNull(reader.GetOrdinal("currency")) ? null : reader.GetString(reader.GetOrdinal("currency"));
                                pt.OriginalPrice = reader.IsDBNull(reader.GetOrdinal("original_price")) ? null : reader.GetDecimal(reader.GetOrdinal("original_price"));
                                pt.TransactionPrice = reader.IsDBNull(reader.GetOrdinal("transaction_price")) ? null : reader.GetDecimal(reader.GetOrdinal("transaction_price"));
                                pt.StartDate = reader.IsDBNull(reader.GetOrdinal("start_date")) ? null : reader.GetDateTime(reader.GetOrdinal("start_date"));
                                pt.EndDate = reader.IsDBNull(reader.GetOrdinal("end_date")) ? null : reader.GetDateTime(reader.GetOrdinal("end_date"));
                                pt.UserId = reader.IsDBNull(reader.GetOrdinal("user_id")) ? null : reader.GetInt32(reader.GetOrdinal("user_id"));
                                pt.ItemId = reader.IsDBNull(reader.GetOrdinal("item_id")) ? null : reader.GetInt32(reader.GetOrdinal("item_id"));
                                pt.ItemType = reader.IsDBNull(reader.GetOrdinal("item_type")) ? null : reader.GetInt32(reader.GetOrdinal("item_type"));
                                pt.State = reader.IsDBNull(reader.GetOrdinal("state")) ? null : reader.GetInt32(reader.GetOrdinal("state"));
                                pt.Token = reader.IsDBNull(reader.GetOrdinal("token")) ? null : reader.GetString(reader.GetOrdinal("token"));
                                pt.TransactionId = reader.IsDBNull(reader.GetOrdinal("transaction_id")) ? null : reader.GetString(reader.GetOrdinal("transaction_id"));
                                pt.RefundTransactionId = reader.IsDBNull(reader.GetOrdinal("refund_transaction_id")) ? null : reader.GetString(reader.GetOrdinal("refund_transaction_id"));
                                pt.PgwStatus = reader.IsDBNull(reader.GetOrdinal("pgw_status")) ? null : reader.GetString(reader.GetOrdinal("pgw_status"));
                                pt.PaymentTypeId = reader.IsDBNull(reader.GetOrdinal("payment_type_id")) ? null : reader.GetInt32(reader.GetOrdinal("payment_type_id"));
                                pt.PayerId = reader.IsDBNull(reader.GetOrdinal("payer_id")) ? null : reader.GetString(reader.GetOrdinal("payer_id"));
                                pt.RefundDate = reader.IsDBNull(reader.GetOrdinal("refund_date")) ? null : reader.GetDateTime(reader.GetOrdinal("refund_date"));
                                pt.RefundDescription = reader.IsDBNull(reader.GetOrdinal("refund_description")) ? null : reader.GetString(reader.GetOrdinal("refund_description"));
                                pt.RefundPrice = reader.IsDBNull(reader.GetOrdinal("refund_price")) ? null : reader.GetDecimal(reader.GetOrdinal("refund_price"));
                                pt.ParentItemId = reader.IsDBNull(reader.GetOrdinal("parent_item_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_item_id"));
                                pt.IpnTnxId = reader.IsDBNull(reader.GetOrdinal("ipn_tnx_id")) ? null : reader.GetString(reader.GetOrdinal("ipn_tnx_id"));
                                pt.LastIpnDate = reader.IsDBNull(reader.GetOrdinal("last_ipn_date")) ? null : reader.GetDateTime(reader.GetOrdinal("last_ipn_date"));
                                pt.IpnTnxType = reader.IsDBNull(reader.GetOrdinal("ipn_tnx_type")) ? null : reader.GetString(reader.GetOrdinal("ipn_tnx_type"));
                                pt.RecurringProfileId = reader.IsDBNull(reader.GetOrdinal("recurring_profile_id")) ? null : reader.GetString(reader.GetOrdinal("recurring_profile_id"));
                                pt.RecurringOriginalPrice = reader.IsDBNull(reader.GetOrdinal("recurring_original_price")) ? null : reader.GetDecimal(reader.GetOrdinal("recurring_original_price"));
                                pt.RecurringTransactionPrice = reader.IsDBNull(reader.GetOrdinal("recurring_transaction_price")) ? null : reader.GetDecimal(reader.GetOrdinal("recurring_transaction_price"));
                                pt.Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"));
                                pt.PromotionName = reader.IsDBNull(reader.GetOrdinal("promotion_name")) ? null : reader.GetString(reader.GetOrdinal("promotion_name"));
                                pt.IsAdult = reader.IsDBNull(reader.GetOrdinal("is_adult")) ? "N" : reader.GetString(reader.GetOrdinal("is_adult"));
                                pt.PlatformName = reader.IsDBNull(reader.GetOrdinal("platform_name")) ? null : reader.GetString(reader.GetOrdinal("platform_name"));
                                pt.DeviceId = reader.IsDBNull(reader.GetOrdinal("device_id")) ? null : reader.GetString(reader.GetOrdinal("device_id"));
                                pt.DeviceType = reader.IsDBNull(reader.GetOrdinal("device_type")) ? null : reader.GetString(reader.GetOrdinal("device_type"));
                                pt.ServiceName = reader.IsDBNull(reader.GetOrdinal("service_name")) ? null : reader.GetString(reader.GetOrdinal("service_name"));
                                pt.CrmAccountId = reader.IsDBNull(reader.GetOrdinal("crm_account_id")) ? null : reader.GetString(reader.GetOrdinal("crm_account_id"));
                                pt.PurchaseChannel = reader.IsDBNull(reader.GetOrdinal("purchase_channel")) ? null : reader.GetString(reader.GetOrdinal("purchase_channel"));
                                pt.DeactivationDate = reader.IsDBNull(reader.GetOrdinal("deactivation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("deactivation_date"));
                                pt.ProviderName = reader.IsDBNull(reader.GetOrdinal("provider_name")) ? null : reader.GetString(reader.GetOrdinal("provider_name"));
                                pt.AssetId = reader.IsDBNull(reader.GetOrdinal("asset_id")) ? null : reader.GetInt32(reader.GetOrdinal("asset_id"));
                                pt.GracePeriodEndDate = reader.IsDBNull(reader.GetOrdinal("grace_period_end_date")) ? null : reader.GetDateTime(reader.GetOrdinal("grace_period_end_date"));
                                pt.FrequencyType = reader.IsDBNull(reader.GetOrdinal("frequency_type")) ? null : reader.GetString(reader.GetOrdinal("frequency_type"));
                                pt.FrequencyValue = reader.IsDBNull(reader.GetOrdinal("frequency_value")) ? null : reader.GetInt64(reader.GetOrdinal("frequency_value"));
                                pt.SwitchFromSequenceId = reader.IsDBNull(reader.GetOrdinal("switch_from_sequence_id")) ? null : reader.GetInt32(reader.GetOrdinal("switch_from_sequence_id"));
                                pt.TrialEndDate = reader.IsDBNull(reader.GetOrdinal("trial_end_date")) ? null : reader.GetDateTime(reader.GetOrdinal("trial_end_date"));
                                pt.ContractStartDate = reader.IsDBNull(reader.GetOrdinal("contract_start_date")) ? null : reader.GetDateTime(reader.GetOrdinal("contract_start_date"));
                                pt.ContractEndDate = reader.IsDBNull(reader.GetOrdinal("contract_end_date")) ? null : reader.GetDateTime(reader.GetOrdinal("contract_end_date"));
                                pt.TerminationDate = reader.IsDBNull(reader.GetOrdinal("termination_date")) ? null : reader.GetDateTime(reader.GetOrdinal("termination_date"));
                                pt.CancellationReason = reader.IsDBNull(reader.GetOrdinal("cancellation_reason")) ? null : reader.GetString(reader.GetOrdinal("cancellation_reason"));
                                pt.VoucherCampaignName = reader.IsDBNull(reader.GetOrdinal("voucher_campaign_name")) ? null : reader.GetString(reader.GetOrdinal("voucher_campaign_name"));
                                pt.TransactionReason = reader.IsDBNull(reader.GetOrdinal("termination_reason")) ? null : reader.GetString(reader.GetOrdinal("termination_reason"));
                                pt.CommercialPackageExternalId = reader.IsDBNull(reader.GetOrdinal("commercial_package_external_id")) ? null : reader.GetString(reader.GetOrdinal("commercial_package_external_id"));
                                pt.ExternalVoucherCode = reader.IsDBNull(reader.GetOrdinal("external_voucher_code")) ? null : reader.GetString(reader.GetOrdinal("external_voucher_code"));
                                pt.UserIpAddress = reader.IsDBNull(reader.GetOrdinal("user_ip_address")) ? null : reader.GetString(reader.GetOrdinal("user_ip_address"));
                                pt.DeactivateChannel = reader.IsDBNull(reader.GetOrdinal("deactivate_channel")) ? null : reader.GetString(reader.GetOrdinal("deactivate_channel"));
                                pt.TrcId = reader.IsDBNull(reader.GetOrdinal("trc_id")) ? null : reader.GetInt64(reader.GetOrdinal("trc_id"));
                                pt.TrcName = reader.IsDBNull(reader.GetOrdinal("trc_name")) ? null : reader.GetString(reader.GetOrdinal("trc_name"));
                                pt.RcPcId = reader.IsDBNull(reader.GetOrdinal("rc_pc_id")) ? null : reader.GetInt64(reader.GetOrdinal("rc_pc_id"));
                                pt.RcPcName = reader.IsDBNull(reader.GetOrdinal("rc_pc_name")) ? null : reader.GetString(reader.GetOrdinal("rc_pc_name"));
                                pt.NrcPcId = reader.IsDBNull(reader.GetOrdinal("nrc_pc_id")) ? null : reader.GetInt64(reader.GetOrdinal("nrc_pc_id"));
                                pt.NrcPcName = reader.IsDBNull(reader.GetOrdinal("nrc_pc_name")) ? null : reader.GetString(reader.GetOrdinal("nrc_pc_name"));
                                pt.Property = reader.IsDBNull(reader.GetOrdinal("property")) ? null : reader.GetString(reader.GetOrdinal("property"));
                                pt.RetailerId = reader.IsDBNull(reader.GetOrdinal("retailer_id")) ? null : reader.GetString(reader.GetOrdinal("retailer_id"));

                                result.Records.Add(pt);
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
