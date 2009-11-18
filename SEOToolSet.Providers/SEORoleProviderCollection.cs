#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public class SEORoleProviderCollection : ProviderCollection
    {
        public new SEORoleProviderBase this[string name]
        {
            get { return (SEORoleProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is SEORoleProviderBase))
                throw new ArgumentException("The provider parameter must be of type SEORoleProviderBase.", "provider");

            base.Add(provider);
        }
    }
}