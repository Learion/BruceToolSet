using System;
using System.Configuration.Provider;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Collection of RecurringBilling providers 
    ///</summary>
    public class RecurringBillingProviderCollection:ProviderCollection
    {
        public new RecurringBillingProviderBase this[string name]
        {
            get { return (RecurringBillingProviderBase)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is RecurringBillingProviderBase))
                throw new ArgumentException("The provider parameter must be of type RecurringBillingProviderBase.",
                                            "provider");

            base.Add(provider);
        }
    }
}
