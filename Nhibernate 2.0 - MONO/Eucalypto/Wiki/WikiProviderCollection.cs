using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Eucalypto.Wiki
{
    public class WikiProviderCollection : ProviderCollection
    {
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("The provider parameter cannot be null.");

            if (!(provider is WikiProvider))
                throw new ArgumentException("The provider parameter must be of type WikiProvider.");

            base.Add(provider);
        }

        new public WikiProvider this[string name]
        {
            get { return (WikiProvider)base[name]; }
        }

        public void CopyTo(WikiProvider[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }
}
