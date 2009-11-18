using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSRankingMonitorDeepRun : EntityDataStoreBase<RankingMonitorDeepRun, int>
    {

        public DSRankingMonitorDeepRun(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSRankingMonitorDeepRun Create(String connName)
        {
            return new DSRankingMonitorDeepRun(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<RankingMonitorDeepRun> FindByRankingMonitorRun(int idRankingMonitorRun, bool showOnlyPrimaryServer, string[] engines)
        {
            var crit = CreateCriteria();
            crit
                .Add(Restrictions.Eq(Columns.Status, new Status { Name = "C" }));

            if (showOnlyPrimaryServer)
            {
                crit.Add(Restrictions.Eq(Columns.ProxyServer, new ProxyServer { Id = 0 }));
            }

            if (engines != null && engines.Length > 0)
            {
                crit.CreateCriteria(Columns.SearchEngineCountry)
                    .Add(Restrictions.In(DSSearchEngineCountry.Columns.Id, engines));
            }

            crit
                .CreateCriteria(Columns.RankingMonitorRun)
                    .Add(Restrictions.Eq(DSRankingMonitorRun.Columns.Id, idRankingMonitorRun));


            return Find(crit);
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String StatusReason = "StatusReason";
            public static String PageRank = "PageRank";
            public static String InboundLinks = "InboundLinks";
            public static String PagesIndexed = "PagesIndexed";
            public static String KeywordDeepAnalysis = "KeywordDeepAnalysis";
            public static String ProxyServer = "ProxyServer";
            public static String RankingMonitorRun = "RankingMonitorRun";
            public static String SearchEngineCountry = "SearchEngineCountry";
            public static String Status = "Status";

        }
    }
}
