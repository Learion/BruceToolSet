#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public class AccountProviderCollection : ProviderCollection
    {
        public new AccountProviderBase this[string name]
        {
            get { return (AccountProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is AccountProviderBase))
                throw new ArgumentException("The provider parameter must be of type AccountProviderBase.", "provider");

            base.Add(provider);
        }
    }
}