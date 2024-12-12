using Serilog.Core;
using Serilog.Events;

namespace EntityInfoService.Utils
{
    public class LogLevelEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var logLevelProperty = propertyFactory.CreateProperty("Level", logEvent.Level.ToString());
            logEvent.AddPropertyIfAbsent(logLevelProperty);
        }
    }
}
