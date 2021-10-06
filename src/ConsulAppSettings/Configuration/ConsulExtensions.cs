using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Winton.Extensions.Configuration.Consul;

namespace ConsulAppSettings.Configuration
{
    public static class ConsulExtensions
    {
        public static IConfigurationBuilder UseConfiguredConsul(
            this IConfigurationBuilder builder,
            IHostEnvironment hostEnvironment,
            string settingsKey)
        {
            return builder.AddConsul(
                $"Applications/{hostEnvironment.ApplicationName}/{hostEnvironment.EnvironmentName}/{settingsKey}",
                source =>
                {
                    var options = ConsulSettings.Build(hostEnvironment);
                    source.ReloadOnChange = true;
                    source.Optional = hostEnvironment.IsDevelopment();
                    source.ConsulConfigurationOptions = config =>
                    {
                        config.Address = options.Address;
                        config.Token = options.Token;
                    };
                    source.OnLoadException = context =>
                    {
                        Log.Error(context.Exception, "Something went wrong when loading Consul configuration");
                    };
                });
        }
    }
}