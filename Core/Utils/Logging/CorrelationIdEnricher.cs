#if NETFULL
using Serilog.Enrichers.CorrelationId.Accessors;
#else
using Microsoft.AspNetCore.Http;
#endif
using Serilog.Core;
using Serilog.Events;

namespace Core.Utils.Logging
{
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;
        private readonly IHttpContextAccessor? _contextAccessor;
        private const string CorrelationIdPropertyName = "CorrelationId";
        private static readonly string _correlationIdItemName = $"{nameof(CorrelationIdEnricher)}+CorrelationId";


        internal CorrelationIdEnricher(ICorrelationIdProvider correlationIdProvider, IHttpContextAccessor? contextAccessor = null)
        {
            _correlationIdProvider = correlationIdProvider; ;
            _contextAccessor = contextAccessor;
        }


        public CorrelationIdEnricher() : this(new CorrelationIdProvider(new HttpContextAccessor()), new HttpContextAccessor())
        {
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var correlationId = _correlationIdProvider.GetCorrelationId();
            AttachCorrelationIdToHttpContext(correlationId, _contextAccessor);

            var correlationIdProperty = new LogEventProperty(CorrelationIdPropertyName, new ScalarValue(correlationId));

            logEvent.AddOrUpdateProperty(correlationIdProperty);
        }


        private static void AttachCorrelationIdToHttpContext(string correlationId, IHttpContextAccessor? contextAccessor)
        {
            if (contextAccessor?.HttpContext == null)
            {
                return;
            }

            contextAccessor.HttpContext.Items[_correlationIdItemName] = correlationId;
        }
    }
}