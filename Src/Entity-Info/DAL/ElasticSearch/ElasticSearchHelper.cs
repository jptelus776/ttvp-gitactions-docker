using EntityInfoService.Utils;
using Serilog;

namespace EntityInfoService.DAL.ElasticSearch
{
    public class ElasticSearchHelper
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(ElasticSearchHelper));
        public static string? ESHost { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("ES_HOST", "localhost", isRequired: false);
        public static string? ESPort { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("ES_PORT", "9042", isRequired: false);
        public static string? ESScheme { get; private set; } = EnvironmentUtils.GetEnvironmentVariable("ES_SCHEME", "https", isRequired: false);

        public static void InitalizeEnvVariables()
        {
            try
            {
                _logger.Information(@"Environment Variables : Host:{ESHost}, Port: {ESPort}, Scheme: {ESScheme}", ESHost, ESPort, ESScheme);

            }
            catch (Exception ex)
            {
                _logger.Error("Error reading Elasticsearch environment variables. Message - {message}", ex.Message);
            }
        }

        public static HttpClient ElasticsearchClient
        {
            get
            {
                // Create an HttpClient with certificate validation disabled
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                var httpClient = new HttpClient(handler);

                // Set the base address of the Elasticsearch node(s) you want to connect to
                httpClient.BaseAddress = new Uri($"{ESScheme}://{ESHost}:{ESPort}");

                return httpClient;
            }
        }
    }
}
