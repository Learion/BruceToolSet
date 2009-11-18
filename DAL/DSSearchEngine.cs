using System;
using NHibernateDataStore.Common;

namespace SEOToolSet.DAL
{
    public class DSSearchEngine : NHibernateDataStore.Common.EntityDataStoreBase<SEOToolSet.Entities.SearchEngine, System.Int32>
    {

        public DSSearchEngine(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSSearchEngine Create(String connName)
        {
            return new DSSearchEngine(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }


        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Description = "Description";
            public static String SearchEngineCountry = "SearchEngineCountry";

        }


    }
}
