using System;
using Microsoft.Extensions.Configuration;

namespace ConsulAppSettings.Configuration
{
    internal class ConsulSettings
    {
        public Uri Address { get; set; }
        public string Token { get; set; }

        public static ConsulSettings Build()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .Build()
                .GetSection("Consul")
                .Get<ConsulSettings>();
        }
    }
}