namespace SEOToolSet.Entities.Wrappers
{
    public class RankingMonitorDeepRunWrapperForResumeReport
    {
        public string SearchEngineLogo { get; set; }
        public string SearchEngineName { get; set; }
        public string SearchEngineUrl { get; set; }
        public int? PageRank { get; set; }
        public int? InboundLinks { get; set; }
        public int? PagesIndexed { get; set; }
        public string ResumeImage { get; set; }

        public static implicit operator RankingMonitorDeepRunWrapperForResumeReport(
            RankingMonitorDeepRun rankingMonitorDeepRun)
        {
            return rankingMonitorDeepRun != null
                       ? new RankingMonitorDeepRunWrapperForResumeReport
                             {
                                 SearchEngineLogo =
                                     rankingMonitorDeepRun.SearchEngineCountry.SearchEngine.UrlLogo,
                                 SearchEngineName = rankingMonitorDeepRun.SearchEngineCountry.SearchEngine.Name,
                                 SearchEngineUrl = rankingMonitorDeepRun.SearchEngineCountry.Url,
                                 InboundLinks = rankingMonitorDeepRun.InboundLinks,
                                 PageRank = rankingMonitorDeepRun.PageRank,
                                 PagesIndexed = rankingMonitorDeepRun.PagesIndexed
                             }
                       : null;
        }
    }
}