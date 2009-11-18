#region

using System;
using System.Collections.Generic;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class RankingMonitorDeepRunWrapper
    {
        public virtual int Id { get; set; }

        public virtual string StatusReason { get; set; }

        public virtual Int32? PageRank { get; set; }

        public virtual Int32? InboundLinks { get; set; }

        public virtual Int32? PagesIndexed { get; set; }

        public virtual IList<KeywordDeepAnalysisWrapper> KeywordDeepAnalysis { get; set; }

        public virtual ProxyServerWrapper ProxyServer { get; set; }

        public virtual SearchEngineCountryWrapper SearchEngineCountry { get; set; }

        public virtual StatusWrapper Status { get; set; }

        public static implicit operator RankingMonitorDeepRunWrapper(RankingMonitorDeepRun rankingMonitorDeepRun)
        {
            if (rankingMonitorDeepRun == null) return null;
            IList<KeywordDeepAnalysis> keywordDeepAnalysis = rankingMonitorDeepRun.KeywordDeepAnalysis;
            var keywordDeepAnalysisWrapper = new List<KeywordDeepAnalysisWrapper>();
            if (keywordDeepAnalysis == null)
                keywordDeepAnalysisWrapper = null;
            else
                foreach (KeywordDeepAnalysis keywordDeepAnalysi in keywordDeepAnalysis)
                    keywordDeepAnalysisWrapper.Add(keywordDeepAnalysi);

            return new RankingMonitorDeepRunWrapper
                       {
                           Id = rankingMonitorDeepRun.Id,
                           StatusReason = rankingMonitorDeepRun.StatusReason,
                           PageRank = rankingMonitorDeepRun.PageRank,
                           InboundLinks = rankingMonitorDeepRun.InboundLinks,
                           PagesIndexed = rankingMonitorDeepRun.PagesIndexed,
                           KeywordDeepAnalysis = keywordDeepAnalysisWrapper,
                           ProxyServer = rankingMonitorDeepRun.ProxyServer,
                           SearchEngineCountry = rankingMonitorDeepRun.SearchEngineCountry,
                           Status = rankingMonitorDeepRun.Status
                       };
        }
    }
}