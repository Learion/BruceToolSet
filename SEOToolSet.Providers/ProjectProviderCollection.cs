#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public class ProjectProviderCollection : ProviderCollection
    {
        public new ProjectProviderBase this[string name]
        {
            get { return (ProjectProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is ProjectProviderBase))
                throw new ArgumentException("The provider parameter must be of type ProjectProviderBase.", "provider");

            base.Add(provider);
        }
    }
}