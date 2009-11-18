
using Ninject.Conditions;
using Ninject.Core;
using NHibernate;
using R3M.Integration.Ninject.Helper;
using System;

namespace WebSite.CustomEntities.AddressBook
{

    public class RegisterModules : INinjectModulesToLoad
    {

        #region INinjectModulesToLoad Members

        public IModule[] RequiredModules
        {
            get
            {
                var modules = new IModule[1];
                modules[0] = new CustomEntitiesModule();
                
                return modules;
            }
        }

        #endregion
    }

    public class CustomEntitiesModule : StandardModule
    {
        public override void Load()
        {
            Bind<ISession>().ToProvider(new SessionProvider()).Only(When.Context.Target.HasAttribute<CustomEntitiesAttribute>());
            Bind<ContactDataStore>().ToSelf();
        }
    }

    public class CustomEntitiesAttribute : Attribute
    {
        
    }
}
