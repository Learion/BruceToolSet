#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.Providers
{
    public class RankingMonitorProviderCollection : ProviderCollection
    {
        public new RankingMonitorProviderBase this[string name]
        {
            get { return (RankingMonitorProviderBase) base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider", "The provider parameter cannot be null.");

            if (!(provider is RankingMonitorProviderBase))
                throw new ArgumentException("The provider parameter must be of type RankingMonitorProviderBase.",
                                            "provider");

            base.Add(provider);
        }
    }
}