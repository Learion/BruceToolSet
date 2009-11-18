using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Eucalypto.Notification
{
    public class NotificationProviderCollection : ProviderCollection
    {
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("The provider parameter cannot be null.");

            if (!(provider is NotificationProvider))
                throw new ArgumentException("The provider parameter must be of type NotificationProvider.");

            base.Add(provider);
        }

        new public NotificationProvider this[string name]
        {
            get { return (NotificationProvider)base[name]; }
        }

        public void CopyTo(NotificationProvider[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }
}
