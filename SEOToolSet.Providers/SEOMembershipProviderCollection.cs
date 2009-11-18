#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public class SEOMembershipProviderCollection : ProviderCollection
    {
        public new SEOMembershipProviderBase this[string name]
        {
            get { return (SEOMembershipProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is SEOMembershipProviderBase))
                throw new ArgumentException("The provider parameter must be of type SEOMembershipProviderBase.",
                                            "provider");

            base.Add(provider);
        }
    }
}