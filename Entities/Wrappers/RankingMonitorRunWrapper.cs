#region

using System;
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class RankingMonitorRunWrapper
    {
        public virtual int Id { get; set; }

        public virtual string User { get; set; }

        public virtual string ExecutionDate { get; set; }

        public virtual string ExecutionDateOnly { get; set; }

        public virtual string ExecutionTimeOnly { get; set; }

        public virtual string AnalysisType { get; set; }

        public virtual IList<KeywordAnalysisWrapper> KeywordAnalysis { get; set; }

        public virtual ProjectWrapper Project { get; set; }

        public virtual StatusWrapper Status { get; set; }

        public virtual string StatusReason { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public static implicit operator RankingMonitorRunWrapper(RankingMonitorRun rankingMonitorRun)
        {
            if (rankingMonitorRun == null) return null;
            var keywordAnalysis = new List<KeywordAnalysisWrapper>();
            if (rankingMonitorRun.KeywordAnalysis != null)
            {
                foreach (KeywordAnalysis keywordAnalysisItem in rankingMonitorRun.KeywordAnalysis)
                    keywordAnalysis.Add(keywordAnalysisItem);
            }
            return new RankingMonitorRunWrapper
                       {
                           Id = rankingMonitorRun.Id,
                           AnalysisType = rankingMonitorRun.AnalysisType,
                           ExecutionDate = rankingMonitorRun.ExecutionDate.ToString(DateTimeFormatInfo.InvariantInfo),
                           ExecutionDateOnly =
                               rankingMonitorRun.ExecutionDate.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo),
                           ExecutionTimeOnly =
                               rankingMonitorRun.ExecutionDate.ToString("HH:mm", DateTimeFormatInfo.InvariantInfo),
                           KeywordAnalysis = keywordAnalysis,
                           Project = rankingMonitorRun.Project,
                           Status = rankingMonitorRun.Status,
                           User = rankingMonitorRun.User,
                           StatusReason = rankingMonitorRun.StatusReason,
                           EndDate = rankingMonitorRun.EndDate
                       };
        }
    }
}