using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DemoApplication
{
    public class AppSettingsReader
    {
        public static bool GetBooleanFromConfig(string key)
        {
            return bool.Parse(ReadAppSetting(key));
        }

        public static IEnumerable<string> ReadCommaSepararated(string key)
        {
            var value = ReadAppSetting(key);
            return (value ?? string.Empty).Split(',').Where(s => !string.IsNullOrWhiteSpace(s));
        }

        public static string ReadAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}