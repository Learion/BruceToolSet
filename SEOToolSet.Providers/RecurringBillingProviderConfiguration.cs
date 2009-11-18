using System.Configuration;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Manages the configuration section for the RecurringBilling providers
    ///</summary>
    public class RecurringBillingProviderConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        [ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }
    }
}
