using EntityInfoService.DAL.CassandraDB;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Models.OpusBackend.User;
using MySql.Data.MySqlClient;
using Serilog;

namespace EntityInfoService.DAL.MySql
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDB
    {
        private static readonly string _schemaName = "user";
        private static string _connectionString = MySqlHelper.GetConnectionString(_schemaName, DbServerType.Main);
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(UserDB));

        /// <summary>
        /// 
        /// </summary>
        public List<AccountUser> AccountUser { get; set; } = new List<AccountUser>();

        /// <summary>
        /// 
        /// </summary>
        public List<CrmAccount> CrmAccount { get; set; } = new List<CrmAccount>();

        /// <summary>
        /// 
        /// </summary>
        public List<AccountRegion> AccountRegion { get; set; } = new List<AccountRegion>();

        /// <summary>
        /// 
        /// </summary>
        public List<AccountUserProfile> AccountUserProfiles { get; set; } = new List<AccountUserProfile>();

        /// <summary>
        /// 
        /// </summary>
        public List<AccountAttribute> AccountAttributes { get; set; } = new List<AccountAttribute>();

        /// <summary>
        /// 
        /// </summary>
        public List<UserConsent> UserConsents { get; set; } = new List<UserConsent>();

        /// <summary>
        /// 
        /// </summary>
        public List<UserTicket> UserTickets { get; set; } = new List<UserTicket>();

        /// <summary>
        /// 
        /// </summary>
        public List<ReserveAccountCancel> ReserveAccountCancel { get; set; } = new List<ReserveAccountCancel>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<AccountUser> GetAccountUser(long userId)
        {
            string tableName = "account_user";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<AccountUser>();

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
                            reader.Read();

                            var user = new AccountUser();
                            user.UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                            user.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
                            user.StatusId = reader.GetInt32(reader.GetOrdinal("status_id"));
                            user.PartyDescription = reader.IsDBNull(reader.GetOrdinal("party_description")) ? null : reader.GetString(reader.GetOrdinal("party_description"));
                            user.Consumption = reader.GetInt32(reader.GetOrdinal("consumption"));
                            user.ConsumptionThreshold = reader.GetInt32(reader.GetOrdinal("consumption_threshold"));
                            user.PurchaseValue = reader.GetInt32(reader.GetOrdinal("purchase_value"));
                            user.PurchaseAlerting = reader.GetInt32(reader.GetOrdinal("purchase_alerting"));
                            user.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                            user.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                            user.SmartCardNumber = reader.IsDBNull(reader.GetOrdinal("smartcard_number")) ? null : reader.GetString(reader.GetOrdinal("smartcard_number"));
                            user.SmsAlerting = reader.IsDBNull(reader.GetOrdinal("sms_alerting")) ? null : reader.GetChar(reader.GetOrdinal("sms_alerting"));
                            user.BloccoAcquisti = reader.IsDBNull(reader.GetOrdinal("blocco_acquisti")) ? null : reader.GetChar(reader.GetOrdinal("blocco_acquisti"));
                            user.FlagInvioSmsAlerting = reader.IsDBNull(reader.GetOrdinal("flag_invio_sms_alerting")) ? null : reader.GetChar(reader.GetOrdinal("flag_invio_sms_alerting"));
                            user.PurchaseThresholdBlocking = reader.GetInt32(reader.GetOrdinal("purchase_threshold_blocking"));
                            user.DataPrimoAccesso = reader.IsDBNull(reader.GetOrdinal("data_primo_accesso")) ? null : reader.GetDateTime(reader.GetOrdinal("data_primo_accesso"));
                            user.FlagInvioSmsAlerting = reader.IsDBNull(reader.GetOrdinal("flag_invio_sms_blocking")) ? null : reader.GetChar(reader.GetOrdinal("flag_invio_sms_blocking"));
                            user.StoredCreditCard = reader.IsDBNull(reader.GetOrdinal("stored_credit_card")) ? null : reader.GetChar(reader.GetOrdinal("stored_credit_card"));
                            user.ParentUserId = reader.IsDBNull(reader.GetOrdinal("parent_user_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_user_id"));
                            user.FirstName = reader.IsDBNull(reader.GetOrdinal("firstname")) ? null : reader.GetString(reader.GetOrdinal("firstname"));
                            user.Surname = reader.IsDBNull(reader.GetOrdinal("surname")) ? null : reader.GetString(reader.GetOrdinal("surname"));
                            user.BirthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? null : reader.GetDateTime(reader.GetOrdinal("birth_date"));
                            user.MobilePhone = reader.IsDBNull(reader.GetOrdinal("mobile_phone")) ? null : reader.GetString(reader.GetOrdinal("mobile_phone"));
                            user.Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"));
                            user.ZipCode = reader.IsDBNull(reader.GetOrdinal("zip_code")) ? null : reader.GetString(reader.GetOrdinal("zip_code"));
                            user.Gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetChar(reader.GetOrdinal("gender"));
                            user.CustomerCode = reader.IsDBNull(reader.GetOrdinal("customer_code")) ? null : reader.GetString(reader.GetOrdinal("customer_code"));
                            user.UserBasedRecordingEnabled = reader.IsDBNull(reader.GetOrdinal("user_based_rec_enabled")) ? null : reader.GetChar(reader.GetOrdinal("user_based_rec_enabled"));
                            user.AudioLanguageType = reader.IsDBNull(reader.GetOrdinal("audio_language_type")) ? null : reader.GetString(reader.GetOrdinal("audio_language_type"));
                            user.NickName = reader.IsDBNull(reader.GetOrdinal("nickname")) ? null : reader.GetString(reader.GetOrdinal("nickname"));
                            user.PurchasePinWrongAttemptsCount = reader.GetInt32(reader.GetOrdinal("purchase_pin_wrong_attempts_count"));
                            user.OnePinFlag = reader.IsDBNull(reader.GetOrdinal("one_pin_flag")) ? null : reader.GetChar(reader.GetOrdinal("one_pin_flag"));
                            user.ChannelHidingEnabled = reader.IsDBNull(reader.GetOrdinal("channel_hiding_enabled")) ? null : reader.GetChar(reader.GetOrdinal("channel_hiding_enabled"));
                            user.PurchasePinEnabled = reader.IsDBNull(reader.GetOrdinal("purchase_pin_enabled")) ? null : reader.GetChar(reader.GetOrdinal("purchase_pin_enabled"));
                            user.ParentalControlPinEnabled = reader.IsDBNull(reader.GetOrdinal("parental_control_pin_enabled")) ? null : reader.GetChar(reader.GetOrdinal("parental_control_pin_enabled"));
                            user.PreferredEpgFormat = reader.IsDBNull(reader.GetOrdinal("preferred_epg_format")) ? null : reader.GetString(reader.GetOrdinal("preferred_epg_format"));
                            user.PurchasePinSaltKey = reader.IsDBNull(reader.GetOrdinal("purchase_pin_salt_key")) ? null : reader.GetString(reader.GetOrdinal("purchase_pin_salt_key"));
                            user.PurchasePinEncryption = reader.IsDBNull(reader.GetOrdinal("purchase_pin_encryption")) ? null : reader.GetString(reader.GetOrdinal("purchase_pin_encryption"));
                            user.PurchasePin = reader.IsDBNull(reader.GetOrdinal("purchase_pin")) ? null : reader.GetString(reader.GetOrdinal("purchase_pin"));
                            user.ParentalControlPinSaltKey = reader.IsDBNull(reader.GetOrdinal("pc_pin_salt_key")) ? null : reader.GetString(reader.GetOrdinal("pc_pin_salt_key"));
                            user.ParentalControlPinEncryption = reader.IsDBNull(reader.GetOrdinal("pc_pin_encryption")) ? null : reader.GetString(reader.GetOrdinal("pc_pin_encryption"));
                            user.ParentalControlPin = reader.IsDBNull(reader.GetOrdinal("pc_pin")) ? null : reader.GetString(reader.GetOrdinal("pc_pin"));
                            user.ParentalControlPinWrongAttemptsCount = reader.GetInt32(reader.GetOrdinal("pc_pin_wrong_attempts_count"));

                            result.Records.Add(user);

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
        public static DbResultList<CrmAccount> GetCrmAccount(long userId)
        {
            string tableName = "crm_account";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<CrmAccount>();

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
                            reader.Read();

                            var acct = new CrmAccount();
                            acct.CrmAccountId = reader.GetString(reader.GetOrdinal("crm_account_id"));
                            acct.UserId = reader.GetInt64(reader.GetOrdinal("user_id"));
                            acct.SmartCardNumber = reader.IsDBNull(reader.GetOrdinal("smartcard_number")) ? null : reader.GetString(reader.GetOrdinal("smartcard_number"));
                            acct.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
                            acct.Surname = reader.IsDBNull(reader.GetOrdinal("surname")) ? null : reader.GetString(reader.GetOrdinal("surname"));
                            acct.BirthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? null : reader.GetDateTime(reader.GetOrdinal("birth_date"));
                            acct.MobilePhone = reader.IsDBNull(reader.GetOrdinal("mobile_phone")) ? null : reader.GetString(reader.GetOrdinal("mobile_phone"));
                            acct.Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"));
                            acct.Cap = reader.IsDBNull(reader.GetOrdinal("cap")) ? null : reader.GetString(reader.GetOrdinal("cap"));
                            acct.SeSso = reader.IsDBNull(reader.GetOrdinal("sesso")) ? null : reader.GetChar(reader.GetOrdinal("sesso"));
                            acct.RegstrazioneWeb = reader.IsDBNull(reader.GetOrdinal("registrazione_web")) ? null : reader.GetString(reader.GetOrdinal("registrazione_web"));
                            acct.ConsensoPctvOttv = reader.IsDBNull(reader.GetOrdinal("consenso_pctvottv")) ? null : reader.GetChar(reader.GetOrdinal("consenso_pctvottv"));
                            acct.SmartCardPaymentType = reader.IsDBNull(reader.GetOrdinal("smartcard_payment_type")) ? null : reader.GetString(reader.GetOrdinal("smartcard_payment_type"));
                            acct.SmartCardStatus = reader.IsDBNull(reader.GetOrdinal("smartcard_status")) ? null : reader.GetString(reader.GetOrdinal("smartcard_status"));
                            acct.StatoMorosita = reader.IsDBNull(reader.GetOrdinal("stato_morosita")) ? null : reader.GetString(reader.GetOrdinal("stato_morosita"));
                            acct.ArticleId = reader.IsDBNull(reader.GetOrdinal("article_id")) ? null : reader.GetString(reader.GetOrdinal("article_id"));
                            acct.VIP = reader.IsDBNull(reader.GetOrdinal("vip")) ? null : reader.GetChar(reader.GetOrdinal("vip"));
                            acct.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                            acct.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                            acct.CustomerCode = reader.IsDBNull(reader.GetOrdinal("customer_code")) ? null : reader.GetString(reader.GetOrdinal("customer_code"));
                            acct.ActivationDate = reader.IsDBNull(reader.GetOrdinal("activation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("activation_date"));
                            acct.DeactivationDate = reader.IsDBNull(reader.GetOrdinal("deactivation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("deactivation_date"));
                            acct.AccountType = reader.IsDBNull(reader.GetOrdinal("account_type")) ? null : reader.GetString(reader.GetOrdinal("account_type"));

                            result.Records.Add(acct);
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

        public static DbResultList<CrmAccount> GetCrmAccount(string crmAccountId)
        {
            string tableName = "crm_account";
            string query = "select * from " + tableName + " where crm_account_id=?";
            var result = new DbResultList<CrmAccount>();

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
                            reader.Read();

                            var acct = new CrmAccount();
                            acct.CrmAccountId = reader.GetString(reader.GetOrdinal("crm_account_id"));
                            acct.UserId = reader.GetInt64(reader.GetOrdinal("user_id"));
                            acct.SmartCardNumber = reader.IsDBNull(reader.GetOrdinal("smartcard_number")) ? null : reader.GetString(reader.GetOrdinal("smartcard_number"));
                            acct.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
                            acct.Surname = reader.IsDBNull(reader.GetOrdinal("surname")) ? null : reader.GetString(reader.GetOrdinal("surname"));
                            acct.BirthDate = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? null : reader.GetDateTime(reader.GetOrdinal("birth_date"));
                            acct.MobilePhone = reader.IsDBNull(reader.GetOrdinal("mobile_phone")) ? null : reader.GetString(reader.GetOrdinal("mobile_phone"));
                            acct.Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"));
                            acct.Cap = reader.IsDBNull(reader.GetOrdinal("cap")) ? null : reader.GetString(reader.GetOrdinal("cap"));
                            acct.SeSso = reader.IsDBNull(reader.GetOrdinal("sesso")) ? null : reader.GetChar(reader.GetOrdinal("sesso"));
                            acct.RegstrazioneWeb = reader.IsDBNull(reader.GetOrdinal("registrazione_web")) ? null : reader.GetString(reader.GetOrdinal("registrazione_web"));
                            acct.ConsensoPctvOttv = reader.IsDBNull(reader.GetOrdinal("consenso_pctvottv")) ? null : reader.GetChar(reader.GetOrdinal("consenso_pctvottv"));
                            acct.SmartCardPaymentType = reader.IsDBNull(reader.GetOrdinal("smartcard_payment_type")) ? null : reader.GetString(reader.GetOrdinal("smartcard_payment_type"));
                            acct.SmartCardStatus = reader.IsDBNull(reader.GetOrdinal("smartcard_status")) ? null : reader.GetString(reader.GetOrdinal("smartcard_status"));
                            acct.StatoMorosita = reader.IsDBNull(reader.GetOrdinal("stato_morosita")) ? null : reader.GetString(reader.GetOrdinal("stato_morosita"));
                            acct.ArticleId = reader.IsDBNull(reader.GetOrdinal("article_id")) ? null : reader.GetString(reader.GetOrdinal("article_id"));
                            acct.VIP = reader.IsDBNull(reader.GetOrdinal("vip")) ? null : reader.GetChar(reader.GetOrdinal("vip"));
                            acct.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                            acct.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                            acct.CustomerCode = reader.IsDBNull(reader.GetOrdinal("customer_code")) ? null : reader.GetString(reader.GetOrdinal("customer_code"));
                            acct.ActivationDate = reader.IsDBNull(reader.GetOrdinal("activation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("activation_date"));
                            acct.DeactivationDate = reader.IsDBNull(reader.GetOrdinal("deactivation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("deactivation_date"));
                            acct.AccountType = reader.IsDBNull(reader.GetOrdinal("account_type")) ? null : reader.GetString(reader.GetOrdinal("account_type"));

                            result.Records.Add(acct);
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
        public static DbResultList<AccountRegion> GetAccountRegion(long userId)
        {
            string tableName = "account_region";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<AccountRegion>();

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
                            reader.Read();
                            var ar = new AccountRegion();
                            ar.AccountRegionId = reader.GetInt32(reader.GetOrdinal("account_region_id"));
                            ar.UserId = reader.GetInt64(reader.GetOrdinal("user_id"));
                            ar.RegionId = reader.GetInt32(reader.GetOrdinal("region_id"));
                            ar.RegionName = reader.IsDBNull(reader.GetOrdinal("region_name")) ? null : reader.GetString(reader.GetOrdinal("region_name"));
                            ar.GeoCountry = reader.IsDBNull(reader.GetOrdinal("geo_country")) ? null : reader.GetString(reader.GetOrdinal("geo_country"));
                            ar.GeoState = reader.IsDBNull(reader.GetOrdinal("geo_state")) ? null : reader.GetString(reader.GetOrdinal("geo_state"));
                            ar.GeoProvince = reader.IsDBNull(reader.GetOrdinal("geo_province")) ? null : reader.GetString(reader.GetOrdinal("geo_province"));
                            ar.GeoCity = reader.IsDBNull(reader.GetOrdinal("geo_city")) ? null : reader.GetString(reader.GetOrdinal("geo_city"));
                            ar.GeoZipCode = reader.IsDBNull(reader.GetOrdinal("geo_zip_code")) ? null : reader.GetString(reader.GetOrdinal("geo_zip_code"));
                            ar.GeoCountryCode = reader.IsDBNull(reader.GetOrdinal("geo_country_code")) ? null : reader.GetString(reader.GetOrdinal("geo_country_code"));
                            ar.GeoStateCode = reader.IsDBNull(reader.GetOrdinal("geo_state_code")) ? null : reader.GetString(reader.GetOrdinal("geo_state_code"));
                            ar.GeoProvinceCode = reader.IsDBNull(reader.GetOrdinal("geo_province_code")) ? null : reader.GetString(reader.GetOrdinal("geo_province_code"));
                            ar.BillCountry = reader.IsDBNull(reader.GetOrdinal("bill_country")) ? null : reader.GetString(reader.GetOrdinal("bill_country"));
                            ar.BillState = reader.IsDBNull(reader.GetOrdinal("bill_state")) ? null : reader.GetString(reader.GetOrdinal("bill_state"));
                            ar.BillProvince = reader.IsDBNull(reader.GetOrdinal("bill_province")) ? null : reader.GetString(reader.GetOrdinal("bill_province"));
                            ar.BillCity = reader.IsDBNull(reader.GetOrdinal("bill_city")) ? null : reader.GetString(reader.GetOrdinal("bill_city"));
                            ar.BillZipCode = reader.IsDBNull(reader.GetOrdinal("bill_zip_code")) ? null : reader.GetString(reader.GetOrdinal("bill_zip_code"));
                            ar.BillCountryCode = reader.IsDBNull(reader.GetOrdinal("bill_country_code")) ? null : reader.GetString(reader.GetOrdinal("bill_country_code"));
                            ar.BillStateCode = reader.IsDBNull(reader.GetOrdinal("bill_state_code")) ? null : reader.GetString(reader.GetOrdinal("bill_state_code"));
                            ar.BillProvinceCode = reader.IsDBNull(reader.GetOrdinal("bill_province_code")) ? null : reader.GetString(reader.GetOrdinal("bill_province_code"));
                            ar.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                            ar.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));

                            result.Records.Add(ar);

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
        public static DbResultList<AccountUserProfile> GetAccountUserProfiles(long userId)
        {
            string tableName = "account_users_profile";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<AccountUserProfile>();

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
                                var aup = new AccountUserProfile();
                                aup.UserId = reader.GetInt64(reader.GetOrdinal("user_id"));
                                aup.ProfileName = reader.GetString(reader.GetOrdinal("profile_name"));
                                aup.IsExitProofEnabled = reader.IsDBNull(reader.GetOrdinal("is_exit_proof_enabled")) ? null : reader.GetInt16(reader.GetOrdinal("is_exit_proof_enabled"));
                                aup.IsKidsProfile = reader.IsDBNull(reader.GetOrdinal("is_kids_profile")) ? null : reader.GetInt16(reader.GetOrdinal("is_kids_profile"));
                                aup.AvatarId = reader.IsDBNull(reader.GetOrdinal("avatar_id")) ? null : reader.GetString(reader.GetOrdinal("avatar_id"));
                                if (aup.AvatarId != null)
                                {
                                    aup.Avatar = UserProfileAvatarKeyspace.GetAvatar(aup.AvatarId);
                                }

                                aup.IsClosedCaptioningEnabled = reader.IsDBNull(reader.GetOrdinal("is_closed_captioning_enabled")) ? null : reader.GetInt16(reader.GetOrdinal("is_closed_captioning_enabled"));
                                aup.IsDescribedVideoEnabled = reader.IsDBNull(reader.GetOrdinal("is_described_video_enabled")) ? null : reader.GetInt16(reader.GetOrdinal("is_described_video_enabled"));
                                aup.IsEntryPinEnabled = reader.IsDBNull(reader.GetOrdinal("is_entry_pin_enabled")) ? null : reader.GetInt16(reader.GetOrdinal("is_entry_pin_enabled"));
                                aup.EntryPin = reader.IsDBNull(reader.GetOrdinal("entry_pin")) ? null : reader.GetString(reader.GetOrdinal("entry_pin"));
                                aup.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                aup.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));

                                result.Records.Add(aup);
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
        public static DbResultList<AccountAttribute> GetAccountAttributes(long userId)
        {
            var dict = GetAttributeDetails();

            string tableName = "account_attribute";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<AccountAttribute>();

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
                                var attribute = new AccountAttribute();
                                attribute.UserAttributeId = reader.GetInt32(reader.GetOrdinal("user_attribute_id"));
                                attribute.AttributeDetailId = reader.GetInt32(reader.GetOrdinal("attribute_detail_id"));
                                attribute.UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                                attribute.AttributeValue = reader.IsDBNull(reader.GetOrdinal("attribute_value")) ? null : reader.GetString(reader.GetOrdinal("attribute_value"));
                                attribute.CreationDate = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                attribute.UpdateDate = reader.GetDateTime(reader.GetOrdinal("update_date"));
                                attribute.EncryptionAlgorithm = reader.IsDBNull(reader.GetOrdinal("encryption_algorithm")) ? null : reader.GetString(reader.GetOrdinal("encryption_algorithm"));

                                if (dict != null)
                                {
                                    AttributeDetail? ad = null;
                                    var doesExist = dict.TryGetValue(attribute.AttributeDetailId, out ad);
                                    if (doesExist && ad != null)
                                    {
                                        attribute.AttributeDetail = ad;
                                    }
                                }

                                result.Records.Add(attribute);
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
        /// <returns></returns>
        public static Dictionary<int, AttributeDetail> GetAttributeDetails()
        {
            string tableName = "attribute_detail";
            string query = "select * from " + tableName;
            var dict = new Dictionary<int, AttributeDetail>();

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
                                var ad = new AttributeDetail();
                                ad.AttributeDetailId = reader.GetInt32(reader.GetOrdinal("attribute_detail_id"));
                                ad.AttributeDetailName = reader.GetString(reader.GetOrdinal("attribute_detail_name"));
                                ad.AttributeDetailDescription = reader.IsDBNull(reader.GetOrdinal("attribute_detail_description")) ? null : reader.GetString(reader.GetOrdinal("attribute_detail_description"));
                                ad.DefaultAttributeValue = reader.IsDBNull(reader.GetOrdinal("default_attribute_value")) ? null : reader.GetString(reader.GetOrdinal("default_attribute_value"));
                                ad.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                ad.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                ad.IsCustom = reader.IsDBNull(reader.GetOrdinal("is_custom")) ? null : reader.GetChar(reader.GetOrdinal("is_custom"));
                                ad.AttributeLabel = reader.IsDBNull(reader.GetOrdinal("attribute_label")) ? null : reader.GetString(reader.GetOrdinal("attribute_label"));
                                ad.DataType = reader.IsDBNull(reader.GetOrdinal("data_type")) ? null : reader.GetString(reader.GetOrdinal("data_type"));
                                ad.DataMaxLength = reader.IsDBNull(reader.GetOrdinal("data_max_length")) ? null : reader.GetInt32(reader.GetOrdinal("data_max_length"));
                                ad.IsEditableByEndUser = reader.IsDBNull(reader.GetOrdinal("is_editable_by_end_user")) ? null : reader.GetChar(reader.GetOrdinal("is_editable_by_end_user"));
                                ad.IsEditableByOperator = reader.IsDBNull(reader.GetOrdinal("is_editable_by_operator")) ? null : reader.GetChar(reader.GetOrdinal("is_editable_by_operator"));
                                ad.StatusId = reader.IsDBNull(reader.GetOrdinal("status_id")) ? null : reader.GetChar(reader.GetOrdinal("status_id"));
                                ad.AllowedValues = reader.IsDBNull(reader.GetOrdinal("allowed_values")) ? null : reader.GetString(reader.GetOrdinal("allowed_values"));
                                ad.IsMandatory = reader.IsDBNull(reader.GetOrdinal("is_mandatory")) ? null : reader.GetChar(reader.GetOrdinal("is_mandatory"));
                                ad.IsVisibileByEndUser = reader.IsDBNull(reader.GetOrdinal("is_visible_by_end_user")) ? null : reader.GetChar(reader.GetOrdinal("is_visible_by_end_user"));
                                ad.IsOnlyForMasterAccount = reader.IsDBNull(reader.GetOrdinal("is_only_for_master_account")) ? null : reader.GetChar(reader.GetOrdinal("is_only_for_master_account"));
                                ad.IsMasked = reader.IsDBNull(reader.GetOrdinal("is_masked")) ? null : reader.GetChar(reader.GetOrdinal("is_masked"));
                                ad.IsForRecommendations = reader.IsDBNull(reader.GetOrdinal("is_for_recommendation")) ? null : reader.GetChar(reader.GetOrdinal("is_for_recommendation"));
                                ad.IsForAnalytics = reader.IsDBNull(reader.GetOrdinal("is_for_analytics")) ? null : reader.GetChar(reader.GetOrdinal("is_for_analytics"));

                                dict.Add(ad.AttributeDetailId, ad);
                            }

                            reader.Close();
                        }
                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        // MySqlHelper.ProcessException(conn, result, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Caught Exception with schema {_schemaName} and table: {tableName}. Message: {message}", _schemaName, tableName, ex.Message);
                        // MySqlHelper.ProcessException(conn, result, ex);
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DbResultList<UserTicket> GetUserTickets(long userId)
        {
            string tableName = "user_ticket";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<UserTicket>();

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
                                var ut = new UserTicket();
                                ut.TicketNumber = reader.GetInt32(reader.GetOrdinal("ticket_number"));
                                ut.UserId = reader.GetInt32(reader.GetOrdinal("user_id"));
                                ut.Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name"));
                                ut.Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email"));
                                ut.ClaimType = reader.IsDBNull(reader.GetOrdinal("claim_type")) ? null : reader.GetString(reader.GetOrdinal("claim_type"));
                                ut.Message = reader.IsDBNull(reader.GetOrdinal("message")) ? null : reader.GetString(reader.GetOrdinal("message"));
                                ut.CreationDate = reader.IsDBNull(reader.GetOrdinal("creation_date")) ? null : reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                ut.UpdateDate = reader.IsDBNull(reader.GetOrdinal("update_date")) ? null : reader.GetDateTime(reader.GetOrdinal("update_date"));
                                ut.StatusId = reader.IsDBNull(reader.GetOrdinal("status_id")) ? null : reader.GetInt32(reader.GetOrdinal("status_id"));
                                ut.UserStatusName = reader.IsDBNull(reader.GetOrdinal("user_status_name")) ? null : reader.GetString(reader.GetOrdinal("user_status_name"));
                                ut.SubscriptionDetails = reader.IsDBNull(reader.GetOrdinal("subscription_details")) ? null : reader.GetString(reader.GetOrdinal("subscription_details"));
                                ut.AdditionalInfo = reader.IsDBNull(reader.GetOrdinal("additional_info")) ? null : reader.GetString(reader.GetOrdinal("additional_info"));

                                result.Records.Add(ut);
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
        public static DbResultList<ReserveAccountCancel> GetReserveAccountCancel(long userId)
        {
            string tableName = "reserve_account_cancel";
            string query = "select * from " + tableName + " where user_id=?";
            var result = new DbResultList<ReserveAccountCancel>();

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
                            reader.Read();

                            var rac = new ReserveAccountCancel();
                            rac.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            rac.UserId = reader.GetInt64(reader.GetOrdinal("user_id"));
                            rac.CreationDate = reader.GetDateTime(reader.GetOrdinal("creation_date"));

                            result.Records.Add(rac);

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