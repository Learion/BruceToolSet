#region Using Directives

using System;
using System.Collections.Specialized;
using System.Globalization;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    public static class ProviderHelper
    {
        public static string ExtractConfigValue(NameValueCollection config, string key, string defaultValue)
        {
            var val = config[key];
            if (val == null)
                return defaultValue.Trim();
            config.Remove(key);

            return val.Trim();
        }

        public static T ExtractConfigValue<T>(NameValueCollection config, string key, T defaultValue)
            where T : IConvertible
        {
            T val;
            try
            {
                val = (T) Convert.ChangeType(config[key], typeof (T), CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return defaultValue;
            }

            if (Equals(val, default(T)))
                return defaultValue;
            config.Remove(key);

            return val;
        }
    }
}