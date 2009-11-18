using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSProxyServer : EntityDataStoreBase<ProxyServer, int>
    {
        private const int primaryProxyServer = 0;
        public DSProxyServer(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSProxyServer Create(String connName)
        {
            return new DSProxyServer(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<ProxyServer> FindAllButPrimary()
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Not(Restrictions.Eq(Columns.Id, primaryProxyServer)));
            return Find(crit);
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String Country = "Country";
            public static String City = "City";
            public static String ImportanceLevel = "ImportanceLevel";
            public static String MonitorProxyServer = "MonitorProxyServer";
            public static String RankingMonitorDeepRun = "RankingMonitorDeepRun";

        }

        public IList<ProxyServer> GetProxiesInRankingMonitorReportRun(int idRankingMonitorReportRun)
        {
            var crit = CreateCriteria();
                crit.SetResultTransformer(CriteriaSpecification.DistinctRootEntity)
                .CreateCriteria(Columns.RankingMonitorDeepRun)
                .Add(Restrictions.Eq(DSRankingMonitorDeepRun.Columns.RankingMonitorRun,
                                     new RankingMonitorRun { Id = idRankingMonitorReportRun }))
                .Add(Restrictions.Eq(DSRankingMonitorDeepRun.Columns.Status, new Status { Name = "C" }));
            return Find(crit);
        }
    }
}
