#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public class ProjectUserProfileProviderCollection : ProviderCollection
    {
        public new ProjectUserProfileProviderBase this[string name]
        {
            get { return (ProjectUserProfileProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is ProjectUserProfileProviderBase))
                throw new ArgumentException("The provider parameter must be of type ProjectUserProfileProviderBase.",
                                            "provider");

            base.Add(provider);
        }
    }
}