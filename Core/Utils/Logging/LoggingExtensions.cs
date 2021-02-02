using System;
using Serilog;
using Serilog.Configuration;

namespace Core.Utils.Logging
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration WithCorrelationId(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
                throw new ArgumentNullException(nameof(enrich));

            return enrich.With<CorrelationIdEnricher>();
        }
    }
}