using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Eucalypto.Forum
{
    public class ForumProviderCollection : ProviderCollection
    {
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("The provider parameter cannot be null.");

            if (!(provider is ForumProvider))
                throw new ArgumentException("The provider parameter must be of type ForumProvider.");

            base.Add(provider);
        }

        new public ForumProvider this[string name]
        {
            get { return (ForumProvider)base[name]; }
        }

        public void CopyTo(ForumProvider[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }
}
