using System;
using NHibernateDataStore.Common;
using NHibernate.Criterion;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSMonitorSearchEngineCountry : NHibernateDataStore.Common.EntityDataStoreBase<SEOToolSet.Entities.MonitorSearchEngineCountry, System.Int32>
    {

        public DSMonitorSearchEngineCountry(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSMonitorSearchEngineCountry Create(String connName)
        {
            return new DSMonitorSearchEngineCountry(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public void DeleteByProject(int idProject)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Project, new Project() { Id = idProject }));
            var elements = Find(crit);
            foreach (var element in elements)
                Delete(element);
            NHibernateSession.Flush();
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String SearchEngineCountry = "SearchEngineCountry";
            public static String Project = "Project";

        }

    }
}
