using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;
using SEOToolSet.ViewEntities;

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateRankingMonitorProvider : RankingMonitorProviderBase
    {
        private string _connName;
        private string _providerName;

        public override string Name
        {
            get { return _providerName; }
        }

        public override string ConnectionString
        {
            get { return _connName; }
        }

        public override IList<SearchEngine> GetSearchEngines()
        {
            return DSSearchEngine.Create(_connName).FindAll();
        }

        public override RankingMonitorConfiguration GetRankingMonitorConfiguration(int idProject)
        {
            return DSProject.Create(_connName).FindByKey(idProject).RankingMonitorConfiguration;
        }

        public override void UpdateRankingMonitorConfiguration(int idProject, string loginName, int idFrequency, int[] idKeywordLists, int[] idProxies, int[] idSearchEngineCountries)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var dsProject = DSProject.Create(_connName);
                var dsMonitorKeywordList = DSMonitorKeywordList.Create(_connName);
                var dsMonitorProxy = DSMonitorProxyServer.Create(_connName);
                var dsMonitorSearchEngineCountry = DSMonitorSearchEngineCountry.Create(_connName);
                var monitorKeywordLists = new List<MonitorKeywordList>();
                var monitorProxies = new List<MonitorProxyServer>();
                var monitorSearchEngineCountries = new List<MonitorSearchEngineCountry>();
                var project = dsProject.FindByKey(idProject);

                //deleting old associations
                dsMonitorKeywordList.DeleteByProject(idProject);
                dsMonitorProxy.DeleteByProject(idProject);
                dsMonitorSearchEngineCountry.DeleteByProject(idProject);

                //adding new associations 
                foreach (var idKeywordList in idKeywordLists)
                {
                    var newMonitorKeywordList = new MonitorKeywordList
                                                    {
                                                        KeywordList = new KeywordList { Id = idKeywordList },
                                                        Project = project
                                                    };
                    monitorKeywordLists.Add(newMonitorKeywordList);
                    dsMonitorKeywordList.Insert(newMonitorKeywordList);
                }

                foreach (var idProxy in idProxies)
                {
                    var newMonitorProxy = new MonitorProxyServer
                                              {
                                                  ProxyServer = new ProxyServer
                                                                    {
                                                                        Id = idProxy
                                                                    },
                                                  Project = project
                                              };
                    monitorProxies.Add(newMonitorProxy);
                    dsMonitorProxy.Insert(newMonitorProxy);
                }

                foreach (var idSearchEngineCountry in idSearchEngineCountries)
                {
                    var newMonitorSearchEngineCountry = new MonitorSearchEngineCountry
                                                            {
                                                                SearchEngineCountry = new SearchEngineCountry
                                                                                          {
                                                                                              Id = idSearchEngineCountry
                                                                                          },
                                                                Project = project
                                                            };
                    monitorSearchEngineCountries.Add(newMonitorSearchEngineCountry);
                    dsMonitorSearchEngineCountry.Insert(newMonitorSearchEngineCountry);
                }

                project.RankingMonitorConfiguration = new RankingMonitorConfiguration
                                                          {
                                                              Frequency = new Frequency { Id = idFrequency },
                                                              MonitorKeywordList = monitorKeywordLists,
                                                              MonitorSearchEngineCountry = monitorSearchEngineCountries,
                                                              MonitorProxyServer = monitorProxies,
                                                              MonitorUpdatedBy = loginName,
                                                              MonitorUpdatedDate = DateTime.Now
                                                          };
                dsProject.Update(project);
                tran.Commit();
            }
        }

        public override IList<SearchEngineCountry> GetSearchEnginesCountries()
        {
            return DSSearchEngineCountry.Create(_connName).FindAll();
        }

        public override DateTime? GetLastRankingMonitorRunDate(int idProject)
        {
            var rankingMonitorRun = DSRankingMonitorRun.Create(_connName).FindLastRun(idProject);
            if (rankingMonitorRun == null) return null;
            return rankingMonitorRun.ExecutionDate;
        }

        public override IList<SearchEngineCountry> GetSearchEnginesCountriesByRankingMonitorRun(int idRankingMonitor)
        {
            return DSSearchEngineCountry.Create(_connName).FindByRankingMonitor(idRankingMonitor);
        }

        public override IList<RankingMonitorRun> GetRankingMonitorRuns(int idProject, bool showAll)
        {
            var ds = DSRankingMonitorRun.Create(_connName);
            return ds.GetRankingMonitorRuns(idProject, showAll);
        }

        public override IList<string> GetTopKeywords(int idProject, int quantityOfTopKeywords)
        {
            //NOTE: this is a mock up for the top keywords. this must be replaced in the future
            return DSKeywordAnalysis.Create(_connName)
                .FindTopKeywordsByProject(idProject, quantityOfTopKeywords);
        }

        public override IList<Frequency> GetFrequencies()
        {
            return DSFrequency.Create(_connName).FindAll();
        }

        public override IList<ProxyServer> GetProxies()
        {
            return DSProxyServer.Create(_connName).FindAll();
        }

        public override IList<ProxyServer> GetAllProxiesButPrimary()
        {
            return DSProxyServer.Create(_connName).FindAllButPrimary();
        }

        public override int CreateRankingMonitor(int idProject, string loginName, int[] idKeywordLists, out int keywordsTotal)
        {
            var dsKeyword = DSKeyword.Create(_connName);
            var keywords = dsKeyword.FindUniqueByKeywordLists(idKeywordLists);
            keywordsTotal = keywords.Count;
            var keywordAnalysis = new List<KeywordAnalysis>();
            foreach (var keyword in keywords)
            {
                var keywordAnalysisItem = new KeywordAnalysis
                                              {
                                                  Keyword = keyword,
                                                  Status = "P"
                                              };
                var keyword1 = keyword;
                if (keywordAnalysis.Exists(Item => Item.Keyword == keyword1)) continue;
                keywordAnalysis.Add(keywordAnalysisItem);
            }
            var rankingMonitorRun = new RankingMonitorRun
                                        {
                                            AnalysisType = "M",
                                            ExecutionDate = DateTime.Now,
                                            Project = new Project { Id = idProject },
                                            Status = new Status { Name = "P" },
                                            User = loginName
                                        };
            using (var tran = new TransactionScope(_connName))
            {
                var dsRankingMonitorRun = DSRankingMonitorRun.Create(_connName);
                var dsKeywordAnalysis = DSKeywordAnalysis.Create(_connName);
                dsRankingMonitorRun.Insert(rankingMonitorRun);
                keywordAnalysis.ForEach(KeywordAnalysis =>
                                            {
                                                KeywordAnalysis.RankingMonitorRun = rankingMonitorRun;
                                                dsKeywordAnalysis.Insert(KeywordAnalysis);
                                            });
                tran.Commit();
            }
            return rankingMonitorRun.Id;
        }

        public override RankingMonitorRun GetRankingMonitorRun(int idRankingMonitorRun)
        {
            return DSRankingMonitorRun.Create(_connName).FindByKey(idRankingMonitorRun);
        }

        public override IList<SearchEngine> GetSearchEnginesByCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public override int GetRankingMonitorRunning(int idProject)
        {
            return DSRankingMonitorRun.Create(_connName).IsAnyRankingMonitorRunning(idProject);
        }

        public override RankingMonitorRun GetLastScheduledRun(int idProject)
        {
            var dsRankingMonitorRun = DSRankingMonitorRun.Create(_connName);
            var lastScheduledRankingMonitorRun = dsRankingMonitorRun.FindLastScheduledRun(idProject);
            return lastScheduledRankingMonitorRun;
        }

        public override IList<KeywordAnalysis> GetKeywordAnalysis(int idRankingMonitorRun)
        {
            var dsKeywordAnalysis = DSKeywordAnalysis.Create(_connName);
            var a = dsKeywordAnalysis.FindByRankingMonitorRun(idRankingMonitorRun);
            return a;
        }

        public override IList<RankingMonitorDeepRun> GetRankingMonitorDeepRuns(int idRankingMonitorRun, bool showOnlyPrimaryServer, string[] engines)
        {
            return DSRankingMonitorDeepRun
                    .Create(_connName)
                    .FindByRankingMonitorRun(idRankingMonitorRun, showOnlyPrimaryServer, engines);
        }



        public override IList<ProxyServer> GetProxiesInRankingMonitorReportRun(int idRankingMonitorRun)
        {
            return DSProxyServer.Create(_connName).GetProxiesInRankingMonitorReportRun(idRankingMonitorRun);
        }

        public override IList<EnginesPerProxyResultView> GetEnginesPerProxyResultView(int idRankingMonitorRun, string[] enginesFilter, string[] proxiesFilter)
        {
            return DSEnginesPerProxyResultView.Create(_connName).GetEnginesPerProxyResultView(idRankingMonitorRun, enginesFilter, proxiesFilter);
        }

        public override void UpdateKeywordAnalysis(KeywordAnalysis keywordAnalysis)
        {
            using (var tran = new TransactionScope(_connName))
            {
                DSKeywordAnalysis.Create(_connName).Update(keywordAnalysis);
                tran.Commit();
            }
        }

        public override void UpdateRankingMonitor(RankingMonitorRun rankingMonitorRun)
        {
            using (var tran = new TransactionScope(_connName))
            {
                DSRankingMonitorRun.Create(_connName).Update(rankingMonitorRun);
                tran.Commit();
            }
        }

        public override void InsertRankingMonitorDeepRuns(IList<RankingMonitorDeepRun> rankingMonitorDeepRuns)
        {
            var dsRankingMonitorRun = DSRankingMonitorDeepRun.Create(_connName);
            var dsKeywordDeepAnalysis = DSKeywordDeepAnalysis.Create(_connName);
            using (var tran = new TransactionScope(_connName))
            {
                foreach (var rankingMonitorDeepRun in rankingMonitorDeepRuns)
                {
                    dsRankingMonitorRun.Insert(rankingMonitorDeepRun);
                }
                foreach (var rankingMonitorDeepRun in rankingMonitorDeepRuns)
                {
                    foreach (var keywordDeepAnalysis in rankingMonitorDeepRun.KeywordDeepAnalysis)
                    {
                        keywordDeepAnalysis.RankingMonitorDeepRun = rankingMonitorDeepRun;
                        dsKeywordDeepAnalysis.Insert(keywordDeepAnalysis);
                    }
                }

                tran.Commit();
            }
        }

        public override KeywordAnalysis GetSingleKeywordAnalysis(int idKeyword)
        {
            return DSKeywordAnalysis.Create(_connName).FindByKey(idKeyword);
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "RankingMonitorProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

            if (config.Count == 0)
                return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }

    }
}
