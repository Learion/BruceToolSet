#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using SEOToolSet.Entities;
using SEOToolSet.ViewEntities;

#endregion

namespace SEOToolSet.Providers
{
    public abstract class RankingMonitorProviderBase : ProviderBase
    {
        public abstract String ConnectionString { get; }

        public abstract IList<SearchEngine> GetSearchEngines();

        public abstract IList<SearchEngine> GetSearchEnginesByCountry(int countryId);

        public abstract RankingMonitorConfiguration GetRankingMonitorConfiguration(int idProject);

        public abstract void UpdateRankingMonitorConfiguration(int idProject, string loginName, int idFrequency,
                                                               int[] idKeywordLists,
                                                               int[] idProxies, int[] idSearchEngineCountries);

        public abstract IList<SearchEngineCountry> GetSearchEnginesCountries();

        public abstract DateTime? GetLastRankingMonitorRunDate(int idProject);

        public abstract IList<SearchEngineCountry> GetSearchEnginesCountriesByRankingMonitorRun(
            int idRankingMonitor);

        public abstract IList<RankingMonitorRun> GetRankingMonitorRuns(int idProject, bool ShowAll);

        public abstract IList<string> GetTopKeywords(int idProject, int quantityOfTopKeywords);

        public abstract IList<Frequency> GetFrequencies();

        public abstract IList<ProxyServer> GetProxies();

        public abstract IList<ProxyServer> GetAllProxiesButPrimary();

        public abstract RankingMonitorRun GetRankingMonitorRun(int idRankingMonitorRun);

        public abstract int CreateRankingMonitor(int idProject, string loginName, int[] idKeywordLists, out int keywordsTotal);

        public abstract int GetRankingMonitorRunning(int idProject);

        public abstract RankingMonitorRun GetLastScheduledRun(int idProject);

        public abstract IList<KeywordAnalysis> GetKeywordAnalysis(int idRankingMonitorRun);

        public abstract IList<RankingMonitorDeepRun> GetRankingMonitorDeepRuns(int idRankingMonitorRun, bool showOnlyPrimaryServer, string[] engines);

        public abstract IList<ProxyServer> GetProxiesInRankingMonitorReportRun(int idRankingMonitorRun);

        public abstract IList<EnginesPerProxyResultView> GetEnginesPerProxyResultView(int idRankingMonitorRun,
                                                                            string[] enginesFilter,
                                                                            string[] proxiesFilter);

        public abstract void UpdateKeywordAnalysis(KeywordAnalysis keywordAnalysisList);

        public abstract void UpdateRankingMonitor(RankingMonitorRun rankingMonitorRun);

        public abstract void InsertRankingMonitorDeepRuns(IList<RankingMonitorDeepRun> rankingMonitorDeepRuns);

        public abstract KeywordAnalysis GetSingleKeywordAnalysis(int idKeywordAnalysis);
    }
}
