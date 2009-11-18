using System;
using NHibernateDataStore.Common;
using NHibernate.Criterion;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSMonitorProxyServer : NHibernateDataStore.Common.EntityDataStoreBase<SEOToolSet.Entities.MonitorProxyServer, System.Int32>
    {

        public DSMonitorProxyServer(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSMonitorProxyServer Create(String connName)
        {
            return new DSMonitorProxyServer(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
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
            public static String Project = "Project";
            public static String ProxyServer = "ProxyServer";

        }
    }
}
