#region Using Directives

using System.Collections.Specialized;

#endregion

namespace SEOToolSet.Providers
{
    public class ConfigHelper
    {
        /// <summary>
        /// A helper function to retrieve config values from the configuration file and remove the entry.
        /// </summary>
        /// <returns></returns>
        public static string ExtractConfigValue(NameValueCollection config, string key, string defaultValue)
        {
            var val = config[key];
            if (val == null)
                return defaultValue;

            config.Remove(key);
            return val;
        }
    }
}