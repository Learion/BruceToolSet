#region Using Directives

using System;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSCountry : EntityDataStoreBase<Country, Int32>
    {
        public DSCountry(ISession session)
            : base(session)
        {
        }


        public static DSCountry Create(String connName)
        {
            return new DSCountry(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns 

        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
        }

        #endregion
    }
}