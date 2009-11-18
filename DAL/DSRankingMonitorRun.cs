using System;
using System.Collections.Generic;
using NHibernateDataStore.Common;
using NHibernate.Criterion;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSRankingMonitorRun : EntityDataStoreBase<RankingMonitorRun, int>
    {

        public DSRankingMonitorRun(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSRankingMonitorRun Create(String connName)
        {
            return new DSRankingMonitorRun(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public RankingMonitorRun FindLastRun(int idProject)
        {
            var crit = CreateCriteria();
            crit
                .Add(Restrictions.Eq(Columns.Project, new Project {Id = idProject}))
                .Add(Restrictions.Eq(Columns.Status, new Status {Name = "C"}))
                .AddOrder(Order.Desc(Columns.ExecutionDate));
                
            var rankingMonitorRuns = Find(crit);
            if (rankingMonitorRuns == null || rankingMonitorRuns.Count == 0) return null;
            return rankingMonitorRuns[0];
        }

        public RankingMonitorRun FindLastScheduledRun(int idProject)
        {
            var crit = CreateCriteria();
            crit
                .Add(Restrictions.Eq(Columns.Project, new Project { Id = idProject }))
                .Add(Restrictions.Eq(Columns.AnalysisType, "A"))
                .Add(Restrictions.Eq(Columns.Status, new Status { Name = "C" }))
                .AddOrder(Order.Desc(Columns.ExecutionDate));
            var rankingMonitorRuns = Find(crit);
            if (rankingMonitorRuns == null || rankingMonitorRuns.Count == 0) return null;
            return rankingMonitorRuns[0];
        }

        public int IsAnyRankingMonitorRunning(int idProject)
        {
            var crit = CreateCriteria();
            crit
                .Add(Restrictions.Eq(Columns.Project, new Project { Id = idProject }))
                .Add(Restrictions.Eq(Columns.Status, new Status { Name = "P" }))
                .AddOrder(Order.Desc(Columns.ExecutionDate));
            var rankingMonitorRuns = Find(crit);
            if (rankingMonitorRuns == null || rankingMonitorRuns.Count == 0) return -1;
            return rankingMonitorRuns[0].Id;
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String User = "User";
            public static String ExecutionDate = "ExecutionDate";
            public static String EndDate = "EndDate";
            public static String AnalysisType = "AnalysisType";
            public static String KeywordAnalysis = "KeywordAnalysis";
            public static String Project = "Project";
            public static String RankingMonitorDeepRun = "RankingMonitorDeepRun";
            public static String Status = "Status";

        }

        public IList<RankingMonitorRun> GetRankingMonitorRuns(int idProject, bool showAll)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Project, new Project {Id = idProject}));
            if (!showAll) crit.Add(Restrictions.Eq(Columns.Status, new  Status { Name = "C" } ));
            crit.AddOrder(Order.Desc(Columns.ExecutionDate));
            return Find(crit);
        }
    }
}
