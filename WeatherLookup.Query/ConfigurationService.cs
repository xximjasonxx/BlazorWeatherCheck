
namespace WeatherLookup.Query
{
    public class ConfigurationService
    {
        public string ApiKey { get; private set; }

        public ConfigurationService(string key)
        {
            ApiKey = key;
        }
    }
}
