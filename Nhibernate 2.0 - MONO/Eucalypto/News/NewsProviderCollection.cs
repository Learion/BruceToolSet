using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Eucalypto.News
{
    public class NewsProviderCollection : ProviderCollection
    {
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("The provider parameter cannot be null.");

            if (!(provider is NewsProvider))
                throw new ArgumentException("The provider parameter must be of type NewsProvider.");

            base.Add(provider);
        }

        new public NewsProvider this[string name]
        {
            get { return (NewsProvider)base[name]; }
        }

        public void CopyTo(NewsProvider[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }
}
