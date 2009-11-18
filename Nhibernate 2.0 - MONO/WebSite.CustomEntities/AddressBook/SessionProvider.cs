using NHibernate;
using NHibernateDataStore.Common;
using Ninject.Core.Activation;
using Ninject.Core.Creation;

namespace WebSite.CustomEntities.AddressBook
{
  
    public class SessionProvider : SimpleProvider<ISession>
    {
        protected override ISession CreateInstance(IContext context)
        {
            return ConfigurationHelper.GetCurrentSession(CustomEntities.Default.ConnectionStringName);
        }
    }
}