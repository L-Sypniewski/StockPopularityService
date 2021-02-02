using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Core.Utils.Logging
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        private static readonly string _correlationIdItemName = $"{nameof(CorrelationIdEnricher)}+CorrelationId";
        private readonly IHttpContextAccessor? _contextAccessor;
        private readonly ILogger<CorrelationIdProvider> _logger;

        private static string? _correlationId;


        public string GetCorrelationId()
        {
            _logger.LogInformation("Getting correlation ID");


            if (_contextAccessor?.HttpContext == null)
            {
                if (_correlationId == null)
                {
                    _logger.LogInformation("Creating new Correlation ID");
                    _correlationId = Guid.NewGuid().ToString();
                    return _correlationId;
                }

                _logger.LogInformation("Returning existing correlation ID");
                return _correlationId;
            }

            _logger.LogInformation("HTTP context exists");
            _correlationId =
                ( string ) ( _contextAccessor.HttpContext.Items[_correlationIdItemName] ?? Guid.NewGuid().ToString() );
            return _correlationId;
        }


        public void ResetId()
        {
            _logger.LogInformation("Resetting Correlation ID");
            _correlationId = null;
        }


        public CorrelationIdProvider(IHttpContextAccessor? contextAccessor = null, ILogger<CorrelationIdProvider>? logger = null)
        {
            _contextAccessor = contextAccessor;
           _logger = logger ?? NullLogger<CorrelationIdProvider>.Instance;
           ResetId();
        }
    }
}