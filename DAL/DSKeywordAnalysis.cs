using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSKeywordAnalysis : EntityDataStoreBase<KeywordAnalysis, int>
    {

        public DSKeywordAnalysis(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSKeywordAnalysis Create(String connName)
        {
            return new DSKeywordAnalysis(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<KeywordAnalysis> FindByRankingMonitorRun(int idRankingMonitorRun)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.RankingMonitorRun, new RankingMonitorRun { Id = idRankingMonitorRun }));
            crit.AddOrder(Order.Desc(Columns.DailySearches));
            return Find(crit);
        }

        public IList<string> FindTopKeywordsByProject(int idProject, int quantity)
        {
            var crit = CreateCriteria();
            crit
                .SetProjection(Projections.Distinct(Projections.Property(Columns.Keyword)))
                .AddOrder(Order.Desc(Columns.GoogleResults))
                .CreateCriteria(Columns.RankingMonitorRun)
                .CreateCriteria(DSRankingMonitorRun.Columns.Project)
                .Add(Restrictions.Eq(DSProject.Columns.Id, idProject))
                .SetMaxResults(quantity);
            return crit.List<string>();
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String Keyword = "Keyword";
            public static String GoogleResults = "GoogleResults";
            public static String AllInTitle = "AllInTitle";
            public static String AliasDomains = "AliasDomains";
            public static String CPC = "CPC";
            public static String DailySearches = "DailySearches";
            public static String Results = "Results";
            public static String Engines = "Engines";
            public static String Pages = "Pages";
            public static String RankingMonitorRun = "RankingMonitorRun";

        }
    }
}
