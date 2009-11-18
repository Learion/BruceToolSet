using System;
using System.Configuration.Provider;

namespace SEOToolSet.Providers
{
    public class PromoCodeProviderCollection : ProviderCollection
    {
        public new PromoCodeProviderBase this[string name]
        {
            get { return (PromoCodeProviderBase)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is PromoCodeProviderBase))
                throw new ArgumentException("The provider parameter must be of type PromoCodeProviderBase.",
                                            "provider");

            base.Add(provider);
        }
    }
}
