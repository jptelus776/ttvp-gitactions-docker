using Cassandra;
using EntityInfoService.Models.OpusBackend;
using EntityInfoService.Utils;
using Serilog;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace EntityInfoService.DAL.CassandraDB
{
    public static class CassandraHelper
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(CassandraHelper));

        public static string? CAHost { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("CA_HOST", isRequired: false);
        public static string? CAPort { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("CA_PORT", isRequired: false);
        public static string? CACertPath { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("CA_CERT_PATH", isRequired: false);
        public static string? CAUser { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("CA_USER", isRequired: false);
        public static string? CAPassword { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("CA_PASSWORD", isRequired: false);
        public static string? CAUseSsl { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("CA_USE_SSL", isRequired: false);
        private static Cluster? _cluster = null;

        /// <summary>
        /// 
        /// </summary>
        public static Cluster? Cluster
        {
            get
            {
                if (_cluster == null)
                {
                    GetCluster();
                }

                return _cluster;
            }
        }

        public static void InitalizeEnvVariables()
        {
            try
            {
                _logger.Information(@"Cassandra Environment Variables : Host:{CAHost}, Port: {CAPort}, Cert Path: {CACertPath}, User: {CAUser}, Password: {CAPassword}, Use SSL: {CAUseSsl}", CAHost, CAPort, CACertPath, CAUser, !string.IsNullOrEmpty(CAPassword) ? CAPassword.Replace(CAPassword, "****") : string.Empty, CAUseSsl);

            }
            catch (Exception ex)
            {
                _logger.Error("Error reading Cassandra environment variables. Message - {message}", ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private static void GetCluster()
        {
            try
            {
                int port = 9042;
                if (!string.IsNullOrEmpty(CAPort))
                {
                    bool result = int.TryParse(CAPort, out port);
                }


                if (!string.IsNullOrEmpty(CAUseSsl) && CAUseSsl.ToLower().Equals("true"))
                {
                    if (!string.IsNullOrEmpty(CACertPath))
                    {
                        // Create an instance of SSLOptions with the PEM certificate
                        var sslOptions = new SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate)
                           .SetCertificateCollection(new X509CertificateCollection
                           {
                        new X509Certificate2 (CACertPath ?? string.Empty)
                           });

                        // Create a CqlSession instance with the SSL options
                        _cluster = Cluster.Builder()
                            .WithPort(port)
                            .AddContactPoint(CAHost ?? "localhost")
                        .WithSSL(new SSLOptions().SetRemoteCertValidationCallback((sender, certificate, chain, errors) => true))
                        .Build();
                    }
                    else
                    {
                        // Create a CqlSession instance with the without SSL options
                        _cluster = Cluster.Builder()
                            .WithPort(port)
                            .AddContactPoint(CAHost ?? "localhost")
                            .Build();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(CAUser) && !string.IsNullOrEmpty(CAPassword))
                    {
                        // Create a CqlSession instance with the Username/password options
                        _cluster = Cluster.Builder()
                            .WithPort(port)
                            .AddContactPoint(CAHost ?? "localhost")
                            .WithCredentials(CAUser, CAPassword)
                            .Build();
                    }
                    else
                    {
                        _cluster = Cluster.Builder()
                           .WithPort(port)
                           .AddContactPoint(CAHost ?? "localhost")                           
                           .Build();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error reading Cassandra environment variables. Message - {message}", ex.Message);
            }
        }

        internal static DbResultList<SchemaVersion> GetSchemaVersions(Cassandra.ISession? session, string schema, params string[] tables)
        {
            DbResultList<SchemaVersion> result = new DbResultList<SchemaVersion>();

            foreach (string table in tables)
            {
                try
                {
                    var cluster = CassandraHelper.Cluster;
                    if (cluster == null)
                    {
                        result.Errors.Add("Cluster was null");
                        return result;
                    }

                    if (session == null)
                    {
                        session = cluster.Connect(schema);
                        if (session == null)
                        {
                            _logger.Error(@"Not able to connect to schema {_schemaName}", schema);
                            result.Errors.Add($"Not able to connect to schema {schema}");
                            return result;
                        }

                        _logger.Information(@"Connected to {_schemaName} keyspace.", schema);
                    }
                    else
                    {
                        _logger.Information(@"Reusing exisitng connection to {_schemaName}", schema);
                    }

                    string query = $"select * from {table}";
                    var prepStmt = session.Prepare(query);
                    var boundStmt = prepStmt.Bind();
                    var rows = session.Execute(boundStmt);
                    foreach (var row in rows)
                    {
                        var sv = new SchemaVersion();
                        sv.SchemaName = schema;
                        sv.TableName = table;
                        sv.DbType = "Cassandra";
                        sv.AvsRelease = row.IsNull("avs_release") ? null : row.GetValue<string>("avs_release");
                        sv.AvsLastIncremental = row.IsNull("avs_last_incremental") ? null : row.GetValue<int>("avs_last_incremental");
                        sv.AvsStartIncremental = row.IsNull("avs_start_incremental") ? null : row.GetValue<int>("avs_start_incremental");
                        sv.CreationDate = row.IsNull("creation_date") ? null : row.GetValue<DateTime>("creation_date");
                        sv.UpdateDate = row.IsNull("update_date") ? null : row.GetValue<DateTime>("update_date");
                        result.Records.Add(sv);
                    }
                }
                catch (QueryExecutionException ex)
                {
                    _logger.Error("Caught Exception with schema {_schemaName} and table: {table}. Message: {ex.Message}, Exception: {ex}", schema, table, ex.Message, ex);
                    ProcessException(result, ex);
                }
                catch (Exception ex)
                {
                    _logger.Error("Caught Exception with schema {_schemaName} and table: {table}. Message: {ex.Message}, Exception: {ex}", schema, table, ex.Message, ex);
                    ProcessException(result, ex);
                }
            }

            return result;
        }

        internal static void ProcessException(DbResultBase result, Exception ex, [CallerMemberName] string? memberName = null)
        {
            if (result != null)
            {
                var msg = string.IsNullOrEmpty(memberName) ? $"Message: {ex.Message}" : $"Method: {memberName}, Message: {ex.Message}";
                result.Errors.Add(msg);
                // result.Exception = ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
