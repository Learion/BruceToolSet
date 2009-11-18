#region Using Directives

using System.Configuration;

#endregion

namespace SEOToolSet.Providers
{
    public class SEOMembershipProviderConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection) base["providers"]; }
        }

        [ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get { return (string) base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }
    }
}