using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.ViewEntities;

namespace SEOToolSet.DAL
{
    public class DSEnginesPerProxyResultView : EntityDataStoreBase<EnginesPerProxyResultView, EnginesPerProxyResultViewKey>
    {
        public DSEnginesPerProxyResultView(ISession session)
            : base(session)
        {
        }


        public static DSEnginesPerProxyResultView Create(String connName)
        {
            return new DSEnginesPerProxyResultView(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns

        public static class Columns
        {
            public static String Keyword = "Keyword";
            public static String Pages = "Pages";
            public static String ProxyCity = "ProxyCity";
            public static String ProxyCountry = "ProxyCountry";
            public static String SearchEngineUrl = "SearchEngineUrl";
            public static String SearchEngineName = "SearchEngineName";
            public static String IdProxy = "IdProxy";
            public static String IdSearchEngineCountry = "IdSearchEngineCountry";
            public static String IdRankingMonitorDeepRun = "IdRankingMonitorDeepRun";
            public static String IdRankingMonitorRun = "IdRankingMonitorRun";

        }

        #endregion

        public IList<EnginesPerProxyResultView> GetEnginesPerProxyResultView(int idRankingMonitorRun, string[] enginesFilter, string[] proxiesFilter)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.IdRankingMonitorRun, idRankingMonitorRun));
            if (enginesFilter != null && enginesFilter.Length > 0)
                crit.Add(Restrictions.In(Columns.IdSearchEngineCountry, enginesFilter));
            if (proxiesFilter != null && proxiesFilter.Length > 0)
                crit.Add(Restrictions.In(Columns.IdProxy, proxiesFilter));

            return Find(crit);
        }
    }
}
