using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsulAppSettings
{
    internal sealed class ConsoleHostedService : BackgroundService
    {
        private readonly Application _application;
        private readonly ILogger _logger;

        public ConsoleHostedService(Application application, ILogger logger)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            try
            {
                await _application.RunAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred");
            }
        }
    }
}