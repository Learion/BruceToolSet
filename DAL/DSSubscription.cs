#region Using Directives

using System;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSSubscription : EntityDataStoreBase<Subscription, Int32>
    {
        public DSSubscription(ISession session)
            : base(session)
        {
        }

        public static DSSubscription Create(String connName)
        {
            return new DSSubscription(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Account = "Account";
            public static String Id = "Id";
            public static String Service = "Service";
        }

        #endregion
    }
}