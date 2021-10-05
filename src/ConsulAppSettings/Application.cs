using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace ConsulAppSettings
{
    public class Application
    {
        private readonly Func<ApplicationSettings> _appSettingsFactory;

        public Application(Func<ApplicationSettings> appSettingsFactory)
        {
            _appSettingsFactory = appSettingsFactory;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                Log.Information("{DateTime}: {@Settings}", DateTime.Now, _appSettingsFactory());
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                }
                catch (TaskCanceledException ex) when (ex.CancellationToken.IsCancellationRequested)
                {
                    // hide the cancellations by a purpose
                }
            }
        }
    }
}