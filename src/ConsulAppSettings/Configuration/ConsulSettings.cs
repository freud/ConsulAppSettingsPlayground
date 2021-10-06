using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ConsulAppSettings.Configuration
{
    internal class ConsulSettings
    {
        public Uri Address { get; set; }
        public string Token { get; set; }

        public static ConsulSettings Build(IHostEnvironment hostEnvironment)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", false, false)
                .Build()
                .GetSection("Consul")
                .Get<ConsulSettings>();
        }
    }
}