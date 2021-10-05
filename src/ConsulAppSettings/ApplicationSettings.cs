using Microsoft.Extensions.Configuration;

namespace ConsulAppSettings
{
    public class ApplicationSettings
    {
        public string SettingsValue { get; }

        private ApplicationSettings(string settingsValue)
        {
            SettingsValue = settingsValue;
        }

        public static ApplicationSettings Create(IConfiguration config)
        {
            return new ApplicationSettings(config["AppSettings:Api"]);
        }
    }
}