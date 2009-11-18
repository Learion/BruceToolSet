#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using SEOToolSet.Common;
using SEOToolSet.Entities;
using SEOToolSet.ViewEntities;

#endregion

namespace SEOToolSet.Providers
{
    public class RankingMonitorManager
    {

        private static readonly RankingMonitorProviderBase _defaultProvider;

        private static readonly RankingMonitorProviderCollection _providerCollection =
            new RankingMonitorProviderCollection();

        static RankingMonitorManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("RankingMonitorProvider") as RankingMonitorProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for RankingMonitorProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof(RankingMonitorProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null)
                return;

            var defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the RankingMonitorProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the RankingMonitorProvider");
        }

        public static RankingMonitorProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null)
                    return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for RankingMonitorProvider.");
            }
        }

        public static RankingMonitorProviderCollection Providers
        {
            get { return _providerCollection; }
        }

        public static int CreateRankingMonitor(int idProject, string loginName, int[] idKeywordLists, out int keywordsTotal)
        {
            return Provider.CreateRankingMonitor(idProject, loginName, idKeywordLists, out keywordsTotal);
        }

        public static RankingMonitorRun GetRankingMonitorRun(int idRankingMonitorRun)
        {
            return Provider.GetRankingMonitorRun(idRankingMonitorRun);
        }

        public static RankingMonitorConfiguration GetRankingMonitorConfiguration(int idProject)
        {
            return Provider.GetRankingMonitorConfiguration(idProject);
        }

        public static void UpdateRankingMonitorConfiguration(int idProject,
                                                             string login,
                                                             int idFrequency,
                                                             int[] idKeywordLists,
                                                             int[] idProxies,
                                                             int[] idSearchEngineCountries)
        {
            Provider.UpdateRankingMonitorConfiguration(idProject, login, idFrequency, idKeywordLists, idProxies,
                                                       idSearchEngineCountries);
        }

        public static int GetRankingMonitorRunning(int idProject)
        {
            return Provider.GetRankingMonitorRunning(idProject);
        }

        public static IList<ProxyServer> GetProxies()
        {
            return Provider.GetProxies();
        }

        public static IList<ProxyServer> GetAllProxiesButPrimary()
        {
            return Provider.GetAllProxiesButPrimary();
        }

        public static IList<SearchEngineCountry> GetSearchEnginesCountries()
        {
            return Provider.GetSearchEnginesCountries();
        }

        public static DateTime? GetLastRankingMonitorRunDate(int idProject)
        {
            return Provider.GetLastRankingMonitorRunDate(idProject);
        }

        public static IList<Frequency> GetFrequencies()
        {
            return Provider.GetFrequencies();
        }

        public static IList<string> GetTopKeywords(int idProject,
                                                            int quantityOfTopKeywords)
        {
            return Provider.GetTopKeywords(idProject, quantityOfTopKeywords);
        }

        public static IList<RankingMonitorRun> GetRankingMonitorRuns(int idProject, bool showAll)
        {
            return Provider.GetRankingMonitorRuns(idProject, showAll);
        }

        public static IList<SearchEngineCountry> GetSearchEnginesCountriesByRankingMonitorRun(int idRankingMonitor)
        {
            return Provider.GetSearchEnginesCountriesByRankingMonitorRun(idRankingMonitor);
        }

        public static IList<SearchEngine> GetSearchEngines()
        {
            return Provider.GetSearchEngines();
        }

        public static DateTime GetNextScheduledRunDate(DateTime startDate, int idFrequency)
        {
            return FrequencyUtil.GetNextScheduledRunDate(startDate, (FrequencyUtil.Frequencies)idFrequency);
        }

        public static KeywordAnalysis GetSingleKeywordAnalysis(int idKeywordAnalysis)
        {
            return Provider.GetSingleKeywordAnalysis(idKeywordAnalysis);
        }

        public static IList<KeywordAnalysis> GetKeywordAnalysis(int idRankingMonitorRun)
        {
            return Provider.GetKeywordAnalysis(idRankingMonitorRun);
        }

        public static IList<RankingMonitorDeepRun> GetRankingMonitorDeepRuns(int idRankingMonitorRun, bool showOnlyPrimaryServer, string[] engines )
        {
            return Provider.GetRankingMonitorDeepRuns(idRankingMonitorRun, showOnlyPrimaryServer, engines);
        }

        public static IList<EnginesPerProxyResultView> GetKeywordsEnginesPerProxy(int idRankingMonitorRun)
        {
            return Provider.GetEnginesPerProxyResultView(idRankingMonitorRun, null, null);
        }

        public static IList<ProxyServer> GetProxiesInRankingMonitorReportRun(int idRankingMonitorRun)
        {
            return Provider.GetProxiesInRankingMonitorReportRun(idRankingMonitorRun);
        }

        public static void UpdateKeywordAnalysis(KeywordAnalysis keywordAnalysisList)
        {
            Provider.UpdateKeywordAnalysis(keywordAnalysisList);
        }

        public static IList<EnginesPerProxyResultView> GetKeywordsEnginesPerProxy(int idRankingMonitorRun, string[] searchEnginesFilter, string[] proxiesFilter)
        {
            return Provider.GetEnginesPerProxyResultView(idRankingMonitorRun, searchEnginesFilter, proxiesFilter);
        }

        public static void UpdateRankingMonitor(RankingMonitorRun rankingMonitorRun)
        {
            Provider.UpdateRankingMonitor(rankingMonitorRun);
        }

        public static void InsertRankingMonitorDeepRuns(IList<RankingMonitorDeepRun> listRankingMonitorDeepRuns)
        {
            Provider.InsertRankingMonitorDeepRuns(listRankingMonitorDeepRuns);
        }
    }
}
