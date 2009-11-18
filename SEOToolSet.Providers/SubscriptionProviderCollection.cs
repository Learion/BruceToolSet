
using System;
using System.Configuration.Provider;

namespace SEOToolSet.Providers
{
    public class SubscriptionProviderCollection:ProviderCollection
    {
        public new SubscriptionProviderBase this[string name]
        {
            get { return (SubscriptionProviderBase)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is SubscriptionProviderBase))
                throw new ArgumentException("The provider parameter must be of type SubscriptionProviderBase.",
                                            "provider");

            base.Add(provider);
        }
    }
}
