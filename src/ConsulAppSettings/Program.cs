using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConsulAppSettings.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsulAppSettings
{
    internal class Program
    {
        public static async Task Main(string[] args)
        { 
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.ClearProviders())
                .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .ConfigureAppConfiguration((context, builder) => builder
                    .UseConfiguredConsul(context.HostingEnvironment, "ApplicationSettings")
                    .AddEnvironmentVariables()
                )
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigureAutofac)
                .ConfigureServices(ConfigureService);
        }

        private static void ConfigureAutofac(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
        }

        private static void ConfigureService(IServiceCollection services)
        {
            services.AddHostedService<ConsoleHostedService>();
        }
    }
}