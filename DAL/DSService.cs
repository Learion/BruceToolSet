#region Using Directives

using System;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSService : EntityDataStoreBase<Service, Int32>
    {
        public DSService(ISession session)
            : base(session)
        {
        }

        public static DSService Create(String connName)
        {
            return new DSService(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Subscription = "Subscription";
        }

        #endregion
    }
}