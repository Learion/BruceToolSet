using System;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;
using NHibernate.Criterion;

namespace SEOToolSet.DAL
{
    public class DSSearchEngineCountry : EntityDataStoreBase<SearchEngineCountry, System.Int32>
    {

        public DSSearchEngineCountry(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSSearchEngineCountry Create(String connName)
        {
            return new DSSearchEngineCountry(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }


        public static class Columns
        {
            public static String Id = "Id";
            public static String Url = "Url";
            public static String RankingMonitorDeepRun = "RankingMonitorDeepRun";
            public static String SearchEngine = "SearchEngine";
            public static String Country = "Country";
            public static String MonitorSearchEngineCountry = "MonitorSearchEngineCountry";
        }

        public System.Collections.Generic.IList<SearchEngineCountry> FindByRankingMonitor(int idRankingMonitorRun)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.RankingMonitorDeepRun)
                .Add(Restrictions.Eq(
                    DSRankingMonitorDeepRun.Columns.RankingMonitorRun,
                    new RankingMonitorRun { Id = idRankingMonitorRun }))
                .SetResultTransformer(CriteriaSpecification.DistinctRootEntity);
            return Find(crit);
        }
    }
}
