using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using SEOToolSet.Common;
using SEOToolSet.Entities;
using SEOToolSet.Entities.Wrappers;
using SEOToolSet.Providers;
using SEOToolSet.ViewEntities;

namespace SEOToolSet.WebApp.Handler
{
    public class RankingMonitorHelper : IHttpHandler
    {
        private const int primaryProxyServerId = 0;

        private static readonly object _syncHelper = new object();

        public void ProcessRequest(HttpContext context)
        {
            var rand = new Random((int)(DateTime.Now.Ticks - int.MaxValue));
            var response = context.Response;
            string idProject, idRankingMonitorRun, idFrequency, loginName;
            List<int> idKeywordLists;
            IList<KeywordAnalysis> keywordAnalysisToUpdate;
            var result = string.Empty;
            response.ContentType = "text/javascript";
            var requestForm = context.Request.Form;
            var serializerType = ObjectSerializerType.Object;
            var action = requestForm["action"];
            Entities.RankingMonitorRun rankingMonitorRunAnalyzed;
            if (string.IsNullOrEmpty(action))
                throw new ArgumentNullException("action", "The action was not provided");

            switch (action)
            {
                case "GetSearchEngines":
                    var searchEngines = RankingMonitorManager.GetSearchEngines();
                    var searchEnginesWrapper = new List<SearchEngineWrapper>();
                    foreach (var searchEngine in searchEngines)
                        searchEnginesWrapper.Add(searchEngine);
                    result = SerializeHelper.GetJsonResult(searchEnginesWrapper, serializerType);
                    break;
                case "GetRankingMonitorConfiguration":
                    idProject = requestForm["idProject"];
                    if (idProject == null) break;
                    RankingMonitorConfigurationWrapper rankingMonitorConfiguration
                        = RankingMonitorManager.GetRankingMonitorConfiguration(int.Parse(idProject));

                    result = SerializeHelper.GetJsonResult(rankingMonitorConfiguration,
                                                           serializerType);
                    break;
                case "GetRankingMonitorConfigurationStructure":
                    idProject = requestForm["idProject"];
                    if (idProject == null) break;
                    var lastRunDateAux = RankingMonitorManager.GetLastRankingMonitorRunDate(int.Parse(idProject));
                    var keywordListsAux = ProjectManager.GetKeywordLists(int.Parse(idProject));
                    var frequenciesAux = RankingMonitorManager.GetFrequencies();
                    var proxiesAux = RankingMonitorManager.GetAllProxiesButPrimary();
                    var searchEnginesAux = RankingMonitorManager.GetSearchEnginesCountries();
                    var keywordListsStruct = new List<KeywordListWrapper>();
                    var frequenciesStruct = new List<FrequencyWrapper>();
                    var proxiesStruct = new List<ProxyServerWrapper>();
                    var searchEncinesStruct = new List<SearchEngineCountryWrapper>();
                    foreach (var keywordList in keywordListsAux)
                        keywordListsStruct.Add(keywordList);
                    foreach (var frequency in frequenciesAux)
                        frequenciesStruct.Add(frequency);
                    foreach (var proxyServer in proxiesAux)
                        proxiesStruct.Add(proxyServer);
                    foreach (var searchEngineCountry in searchEnginesAux)
                        searchEncinesStruct.Add(searchEngineCountry);
                    var rankingMonitorConfigurationStructure = new RankingMonitorConfigurationStructure
                                                                 {
                                                                     KeywordLists = keywordListsStruct,
                                                                     Frequency = frequenciesStruct,
                                                                     ProxyServers = proxiesStruct,
                                                                     SearchEngineCountry = searchEncinesStruct,
                                                                     LastRun = lastRunDateAux.HasValue ? new LastRun
                                                                                   {
                                                                                       LastDate = lastRunDateAux.Value.ToString("MM/dd/yyyy"),
                                                                                       LastTime = lastRunDateAux.Value.ToShortTimeString()
                                                                                   } : null
                                                                 };
                    result = SerializeHelper.GetJsonResult(rankingMonitorConfigurationStructure,
                                                           serializerType);
                    break;
                case "UpdateRankingMonitorConfiguration":
                    idProject = requestForm["idProject"];
                    loginName = context.User.Identity.Name;
                    idFrequency = requestForm["idFrequency"];
                    var idKeywordListsText = requestForm.GetValues("idKeywordLists");
                    var idProxiesText = requestForm.GetValues("idProxies");
                    var idSearchEngineCountriesText = requestForm.GetValues("idSearchEngineCountries");
                    idKeywordLists = new List<int>();
                    var idProxies = new List<int>();
                    var idSearchEngineCountries = new List<int>();
                    if (idSearchEngineCountriesText == null) throw new ArgumentNullException("idSearchEngineCountries");
                    if (idKeywordListsText == null) throw new ArgumentNullException("idKeywordLists");
                    foreach (var idSearchEngineCountry in idSearchEngineCountriesText)
                        idSearchEngineCountries.Add(int.Parse(idSearchEngineCountry));
                    foreach (var idKeywordList in idKeywordListsText)
                        idKeywordLists.Add(int.Parse(idKeywordList));
                    //Adding the Primary Server if there is not established
                    if (!idProxies.Exists(id => id == primaryProxyServerId))
                        idProxies.Add(primaryProxyServerId);
                    if (idProxiesText != null)
                        foreach (var idProxy in idProxiesText)
                            idProxies.Add(int.Parse(idProxy));
                    RankingMonitorManager.UpdateRankingMonitorConfiguration(int.Parse(idProject), loginName,
                                                                            int.Parse(idFrequency),
                                                                            idKeywordLists.ToArray(),
                                                                            idProxies.ToArray(),
                                                                            idSearchEngineCountries.ToArray());
                    result = @"{Result:true}";
                    break;
                case "GetSearchEnginesCountries":
                    var searchEngineCountries = RankingMonitorManager.GetSearchEnginesCountries();
                    var searchEngineCountriesWrapper = new List<SearchEngineCountryWrapper>();
                    foreach (var searchEngineCountry in searchEngineCountries)
                        searchEngineCountriesWrapper.Add(searchEngineCountry);
                    result =
                        SerializeHelper.GetJsonResult(searchEngineCountriesWrapper, serializerType);
                    break;
                case "GetLastRankingMonitorRunDate":
                    idProject = requestForm["idProject"];
                    if (idProject == null) break;
                    var lastRunDate = RankingMonitorManager.GetLastRankingMonitorRunDate(int.Parse(idProject));
                    if (lastRunDate.HasValue)
                    {
                        var dateResult = new StringBuilder();
                        dateResult.AppendLine(@"{ LastRun:");
                        dateResult.AppendFormat(@"{{ LastDate: '{0}',", lastRunDate.Value.ToString("MM/dd/yyyy"));
                        dateResult.AppendFormat(@"LastTime: '{0}' }}", lastRunDate.Value.ToShortTimeString());
                        dateResult.Append(@"}");
                        result = dateResult.ToString();
                    }
                    else
                        result = @"{ LastRun:'N/A' }";
                    break;
                case "GetSearchEnginesCountriesByRankingMonitorRun":
                    idRankingMonitorRun = requestForm["idRankingMonitorRun"];
                    var idRankingMonitorRunToCompare = requestForm["idRankingMonitorRunToCompare"];
                    result = string.Format("{{ enginesInReportToView : {0}, enginesInReportToCompare : {1} }}", GetJSONSearchEnginesInRankingMonitorRun(idRankingMonitorRun), GetJSONSearchEnginesInRankingMonitorRun(idRankingMonitorRunToCompare));

                    break;
                case "GetRankingMonitorRuns":
                    idProject = requestForm["idProject"];
                    if (idProject == null) break;
                    var rankingMonitorRuns = RankingMonitorManager.GetRankingMonitorRuns(int.Parse(idProject), false);
                    var rankingMonitorRunsWrapper = new List<RankingMonitorRunWrapper>();
                    foreach (var rankingMonitorRun in rankingMonitorRuns)
                        rankingMonitorRunsWrapper.Add(rankingMonitorRun);
                    result =
                        SerializeHelper.GetJsonResult(rankingMonitorRunsWrapper, serializerType);
                    break;
                case "GetKeywordsAnalysisFromRankingMonitorRun":
                    idRankingMonitorRun = requestForm["idRankingMonitorRun"];
                    if (idRankingMonitorRun == null) break;
                    var keywordAnalysisList = RankingMonitorManager.GetKeywordAnalysis(int.Parse(idRankingMonitorRun));
                    var keywordAnalysisListWrapper = new List<KeywordAnalysisWrapper>();
                    foreach (var keywordAnalysis in keywordAnalysisList)
                        keywordAnalysisListWrapper.Add(keywordAnalysis);
                    result = SerializeHelper.GetJsonResult(keywordAnalysisListWrapper, serializerType);
                    break;
                case "CheckProgressRankingMonitorRun":
                    idRankingMonitorRun = requestForm["idRankingMonitorRun"];
                    keywordAnalysisToUpdate = RankingMonitorManager.GetKeywordAnalysis(int.Parse(idRankingMonitorRun));

                    var listOfWrappers = new List<KeywordAnalysisWrapper>();
                    foreach (var keywordAnalysis in keywordAnalysisToUpdate)
                    {
                        if (keywordAnalysis.Status == "C" || keywordAnalysis.Status == "F")
                            listOfWrappers.Add(keywordAnalysis);
                    }

                    var keywordAnalysisJson = SerializeHelper.GetJsonResult(listOfWrappers, serializerType);
                    result = string.Format(@"{{ ""KeywordAnalyzed"": {0} }}", keywordAnalysisJson);
                    break;
                case "CheckLastProgressRankingMonitorRun":
                    var rankingMonitorDeepRunResult = string.Empty;
                    var inboundLinksTotal = 0;
                    var pagesIndexedTotal = 0;
                    idRankingMonitorRun = requestForm["idRankingMonitorRun"];
                    if (idRankingMonitorRun == null) break;
                    keywordAnalysisToUpdate = RankingMonitorManager.GetKeywordAnalysis(int.Parse(idRankingMonitorRun));
                    try
                    {
                        RankingMonitorDeepRun summary;
                        int competitorsTotal;
                        lock (_syncHelper)
                        {
                            rankingMonitorRunAnalyzed = RankingMonitorManager.GetRankingMonitorRun(int.Parse(idRankingMonitorRun));
                            competitorsTotal = rankingMonitorRunAnalyzed.Project.Competitor == null ? 0
                                                   : rankingMonitorRunAnalyzed.Project.Competitor.Count;
                            var configuration = rankingMonitorRunAnalyzed.Project.RankingMonitorConfiguration;
                            var pageRankTotal = 0;
                            if (rankingMonitorRunAnalyzed.Status.Name == "C")
                            {
                                foreach (var deepRun in rankingMonitorRunAnalyzed.RankingMonitorDeepRun)
                                {
                                    if (deepRun.InboundLinks.HasValue)
                                        inboundLinksTotal += deepRun.InboundLinks.Value;
                                    if (deepRun.PagesIndexed.HasValue)
                                        pagesIndexedTotal += deepRun.PagesIndexed.Value;
                                    if (deepRun.PageRank.HasValue)
                                        pageRankTotal += deepRun.PageRank.Value;
                                }
                                summary = new RankingMonitorDeepRun
                                {
                                    InboundLinks = inboundLinksTotal,
                                    PageRank = pageRankTotal / rankingMonitorRunAnalyzed.RankingMonitorDeepRun.Count,
                                    PagesIndexed = pagesIndexedTotal
                                };
                            }
                            else
                            {
                                rankingMonitorRunAnalyzed.EndDate = DateTime.Now;
                                var monitorProxyServers = configuration.MonitorProxyServer;
                                var monitorSearchEngines = configuration.MonitorSearchEngineCountry;
                                var rankingMonitorDeepRunList = new List<RankingMonitorDeepRun>();
                                var m = monitorSearchEngines.Count * monitorProxyServers.Count * keywordAnalysisToUpdate.Count;
                                foreach (var monitorProxyServer in monitorProxyServers)
                                {
                                    foreach (var monitorSearchEngine in monitorSearchEngines)
                                    {
                                        var rankingMonitorDeepRun = new RankingMonitorDeepRun
                                        {
                                            InboundLinks = rand.Next(15, 300),
                                            PageRank = rand.Next(1, 10),
                                            PagesIndexed = rand.Next(20, 200),
                                            ProxyServer = monitorProxyServer.ProxyServer,
                                            SearchEngineCountry =
                                                monitorSearchEngine.SearchEngineCountry,
                                            RankingMonitorRun = rankingMonitorRunAnalyzed,
                                            Status = new Status { Name = "C" },
                                            StatusReason = null
                                        };
                                        var keywordDeepAnalysisList = new List<KeywordDeepAnalysis>();
                                        foreach (var keywordAnalysis in keywordAnalysisToUpdate)
                                        {
                                            var keywordAnalysisDeep = new KeywordDeepAnalysis
                                            {
                                                Keyword = keywordAnalysis.Keyword,
                                                Status = (keywordAnalysis.Status == "F"
                                                              ? (rand.Next(m) > 1 ? "F" : "C")
                                                              : "C")
                                            };

                                            if (keywordAnalysisDeep.Status == "C")
                                                keywordAnalysisDeep.Pages = rand.Next(3, 20);

                                            keywordDeepAnalysisList.Add(keywordAnalysisDeep);
                                        }
                                        rankingMonitorDeepRun.KeywordDeepAnalysis = keywordDeepAnalysisList;
                                        rankingMonitorDeepRunList.Add(rankingMonitorDeepRun);
                                    }
                                }
                                RankingMonitorManager.InsertRankingMonitorDeepRuns(rankingMonitorDeepRunList);

                                rankingMonitorRunAnalyzed.Status = new Status { Name = "C" };
                                RankingMonitorManager.UpdateRankingMonitor(rankingMonitorRunAnalyzed);
                                rankingMonitorDeepRunList.ForEach(deepRun =>
                                                                      {
                                                                          if (deepRun.InboundLinks.HasValue)
                                                                              inboundLinksTotal += deepRun.InboundLinks.Value;
                                                                          if (deepRun.PagesIndexed.HasValue)
                                                                              pagesIndexedTotal += deepRun.PagesIndexed.Value;
                                                                          if (deepRun.PageRank.HasValue)
                                                                              pageRankTotal += deepRun.PageRank.Value;
                                                                      });
                                summary = new RankingMonitorDeepRun
                                {
                                    InboundLinks = inboundLinksTotal,
                                    PageRank = pageRankTotal / rankingMonitorDeepRunList.Count,
                                    PagesIndexed = pagesIndexedTotal
                                };
                            }
                        }
                        rankingMonitorDeepRunResult = string.Format(@"{{""RankingMonitorDeepRunSummary"": {0}, ""Competitors"": {1} }}",
                                                                        SerializeHelper.GetJsonResult(summary, serializerType), competitorsTotal);
                    }
                    catch (Exception ex)
                    {
                        LoggerFacade.Log.LogException(GetType(), ex);
                        rankingMonitorRunAnalyzed = RankingMonitorManager.GetRankingMonitorRun(int.Parse(idRankingMonitorRun));
                        if (rankingMonitorRunAnalyzed != null)
                        {
                            rankingMonitorRunAnalyzed.Status = new Status { Name = "F" };
                            rankingMonitorRunAnalyzed.StatusReason = ex.ToString();
                            rankingMonitorRunAnalyzed.EndDate = null;
                            RankingMonitorManager.UpdateRankingMonitor(rankingMonitorRunAnalyzed);
                        }
                    }
                    result = rankingMonitorDeepRunResult;
                    break;
                case "GetTopKeywords":
                    idProject = requestForm["idProject"];
                    var quantity = requestForm["quantity"];
                    if (idProject == null || quantity == null) break;
                    var keywordsAnalysis = RankingMonitorManager.GetTopKeywords(int.Parse(idProject), int.Parse(quantity));
                    var keywordsAnalysisWrapper = new List<string>();
                    foreach (var keyword in keywordsAnalysis)
                        keywordsAnalysisWrapper.Add(keyword);
                    result = SerializeHelper.GetJsonResult(keywordsAnalysisWrapper, serializerType);
                    break;
                case "GetFrequencies":
                    var frequencies = RankingMonitorManager.GetFrequencies();
                    var frequenciesWrapper = new List<FrequencyWrapper>();
                    foreach (var frequency in frequencies)
                        frequenciesWrapper.Add(frequency);
                    result = SerializeHelper.GetJsonResult(frequenciesWrapper, serializerType);
                    break;
                case "GetProxies":
                    var proxies = RankingMonitorManager.GetAllProxiesButPrimary();
                    var proxiesWrapper = new List<ProxyServerWrapper>();
                    foreach (var proxy in proxies)
                        proxiesWrapper.Add(proxy);
                    result = SerializeHelper.GetJsonResult(proxiesWrapper, serializerType);
                    break;
                case "GetRankingMonitorRun":
                    idRankingMonitorRun = requestForm["idRankingMonitorRun"];
                    RankingMonitorRunWrapper rankingMonitorRunWrapper =
                        RankingMonitorManager.GetRankingMonitorRun(int.Parse(idRankingMonitorRun));
                    if (rankingMonitorRunWrapper == null) break;
                    result = SerializeHelper.GetJsonResult(rankingMonitorRunWrapper, serializerType);
                    break;
                case "RunRankingMonitor":
                    idProject = requestForm["idProject"];
                    int keywordsTotal;
                    idRankingMonitorRun = StartRankingMonitor(idProject, requestForm.GetValues("idKeywordLists"), out keywordsTotal)
                        .ToString(NumberFormatInfo.InvariantInfo);
                    result = string.Format(@"{{ IdRankingMonitorRun: {0}, KeywordsTotal: {1} }}", idRankingMonitorRun, keywordsTotal);

                    var tempUri = WebHelper.ResolveUrlToUri(string.Format("/Handler/Async/RunMonitor.ashx?idRankingMonitorRun={0}", idRankingMonitorRun));
                    LoggerFacade.Log.Debug(GetType(),string.Format("tempUri.AbsolutePath {0}", tempUri.AbsolutePath));
                    var request = (HttpWebRequest)WebRequest.Create(tempUri);
                    //var request = (HttpWebRequest)WebRequest.Create(WebHelper.ResolveUrlToUri(string.Format("~/Handler/NotAsync.ashx?idRankingMonitorRun={0}", idRankingMonitorRun)));
                    request.BeginGetResponse(doCallBack, new object());

                    break;
                case "GetNextScheduledRunDate":
                    var startDateText = requestForm["lastRankingMonitorRunDate"];
                    idFrequency = requestForm["idFrequency"];
                    DateTime startDate;
                    if (DateTime.TryParse(startDateText, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out startDate))
                    {
                        var nextScheduledDate = RankingMonitorManager.GetNextScheduledRunDate(startDate, int.Parse(idFrequency));
                        result = string.Format(@"{{ NextRun: '{0}'}}", nextScheduledDate.ToString("MM/dd/yyyy"));
                    }
                    else
                        result = @"{ NextRun: 'N/A' }";
                    break;
                case "GetRankingChartUri":
                    idProject = requestForm["idProject"];
                    lastRunDate = RankingMonitorManager.GetLastRankingMonitorRunDate(int.Parse(idProject));
                    if (!lastRunDate.HasValue)
                    {
                        result = @"{{ ChartUrl:'N/A' }}";
                        break;
                    }
                    var random = new Random(int.Parse(idProject));
                    var arrayInt = new List<int> { 30, 25, 14, 23, 34, 30, 48 };
                    var top10 = new string[7];
                    var i = 0;
                    arrayInt.ForEach(elem => top10[i++] = random.Next(0, elem).ToString());
                    const string chartUrl = "images/ReportChart3.png";
                    //string.Format("http://chart.apis.google.com/chart?&chs=278x200&cht=bvs&chco=4D89F9,C6D9FD&chd=t:{0},{1},{2},{3},{4},{5},{6}|16,18,23,22,25,13,21|3,5,8,8,12,18,23&chdl=Top%2010|Top%2020|Top%2030&chco=E00000,0000E0,00E000&chxt=x,y&chxl=0:|10|18|27|2|10|18|24|1:|0|5|10|15|20|25", top10);
                    result = string.Format(@"{{ ChartUrl:'{0}' }}", chartUrl);
                    break;
                case "GetReportSummaryForRankingMonitorRun":
                    var idReportToView = requestForm["idRankingMonitorRunToView"];
                    var idReportToCompare = requestForm["idRankingMonitorRunToCompare"];
                    var engines = requestForm["engines"];

                    var enginesList = engines.Split(',');

                    var deepRunsWrapperToView = new List<RankingMonitorDeepRunWrapperForResumeReport>();

                    var deepRunsToView = RankingMonitorManager.GetRankingMonitorDeepRuns(int.Parse(idReportToView), true, enginesList);

                    var resume = new DeepRunResumeWrapper { PageRank = 0, InboundLinks = 0, PagesIndexed = 0 };

                    var counter = 0;
                    foreach (var deepRun in deepRunsToView)
                    {
                        var dr = (RankingMonitorDeepRunWrapperForResumeReport)deepRun;
                        dr.ResumeImage = String.Format("images/ViewRankingMonitorReport/resumen0{0}-{1}.jpg",
                            counter++ % 2 + 1, int.Parse(idReportToCompare) != -1 ? "double" : "single");
                        deepRunsWrapperToView.Add(dr);
                        resume.PageRank += deepRun.PageRank;
                        resume.InboundLinks += deepRun.InboundLinks;
                        resume.PagesIndexed += deepRun.PagesIndexed;
                    }
                    if (deepRunsToView.Count > 0)
                        resume.PageRank = resume.PageRank / deepRunsToView.Count;

                    resume.ResumeImage = String.Format("images/ViewRankingMonitorReport/resumen0{0}-{1}.jpg",
                                                       counter % 2 + 1, int.Parse(idReportToCompare) != -1 ? "double" : "single");

                    var rows = SerializeHelper.GetJsonResult(deepRunsWrapperToView, serializerType);

                    result = String.Format("{{ \"data\" : {0}, \"total\" : {1}  }}", rows, SerializeHelper.GetJsonResult(resume, serializerType));
                    break;
                case "GetDetailedRankingMonitorReportRun":
                    result = ReturnDetailedRankingMonitorReport();
                    break;
                case "GetProxiesInRankingMonitorReportRun":
                    result = GetProxiesInRankingMonitorReportRun();
                    break;
                case "GetMonitorReportPDF":
                    result = GetMonitorReportPDF();
                    break;
                case "IsMonitorRunning":
                    idProject = requestForm["idProject"];
                    var idMonitorRunning = RankingMonitorManager.GetRankingMonitorRunning(int.Parse(idProject));
                    //if (idMonitorRunning < 1 && rand.Next(100) < 5)
                    //{
                    //    var keywordLists = ProjectManager.GetKeywordLists(int.Parse(idProject));
                    //    var keywordListsIdText = new List<string>();
                    //    foreach (var keywordList in keywordLists)
                    //    {
                    //        keywordListsIdText.Add(keywordList.Id.ToString(NumberFormatInfo.InvariantInfo));
                    //    }
                    //    idMonitorRunning = StartRankingMonitor(idProject, keywordListsIdText.ToArray(), out keywordsTotal);
                    //}
                    result = string.Format(@"{{ ""IsAnyRunning"": {0} }}", idMonitorRunning);
                    break;
                case "EndMonitorProcess":
                    idRankingMonitorRun = requestForm["idRankingMonitorRun"];
                    var monitorRunning = RankingMonitorManager.GetRankingMonitorRun(int.Parse(idRankingMonitorRun));
                    monitorRunning.Status = new Status { Name = "F" };
                    monitorRunning.StatusReason =
                        string.Format("The Monitor Process was cancelled manually by the user '{0}' on {1}",
                                      context.User.Identity.Name, DateTime.Now);
                    RankingMonitorManager.UpdateRankingMonitor(monitorRunning);
                    result = @"{ ""Result"": true}";
                    break;
                default:
                    break;
            }
            response.Write(result);
        }

        private void doCallBack(IAsyncResult ar)
        {
            LoggerFacade.Log.Debug(GetType(), "Async request Completed");
        }

        private static int StartRankingMonitor(string idProject, IEnumerable<string> idKeywordListsText, out int keywordsTotal)
        {
            var context = HttpContext.Current;
            var loginName = context.User.Identity.Name;
            var idKeywordLists = new List<int>();
            if (idProject == null || idKeywordListsText == null)
            {
                keywordsTotal = 0;
                return -1;
            }
            foreach (var idKeywordList in idKeywordListsText)
            {
                idKeywordLists.Add(int.Parse(idKeywordList));
            }
            return RankingMonitorManager.CreateRankingMonitor(int.Parse(idProject), loginName, idKeywordLists.ToArray(), out keywordsTotal);
        }



        private string GetMonitorReportPDF()
        {
            var data = HttpContext.Current.Request.Form["ReportData"];
            var fileName = HttpContext.Current.Request.Form["FileName"];
            return ExportToPDF(data, fileName);

        }

        protected string ExportToPDF(String data, String fileName)
        {
            var rows = data.Split('\n');
            if (String.IsNullOrEmpty(data) || rows.Length <= 0) return "{ \"fileId\" : \"-1\" }";
            return createPDF(rows, fileName);
        }
        private string createPDF(string[] rows, string fileName)
        {
            try
            {
                var firstRow = rows[0];
                //if (!useSiberix)
                //{

                //    var document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);




                //    var memStream = new MemoryStream();

                //    PdfWriter.GetInstance(document, memStream);

                //    document.Open();

                //    var p1 = new Paragraph(new Chunk(firstRow,
                //                                     FontFactory.GetFont(FontFactory.HELVETICA, 20))) { SpacingAfter = 20 };


                //    document.Add(p1);

                //    var headerRow = rows[1];
                //    var columns = headerRow.Split('|');

                //    var dataTable = new PdfPTable(columns.Length);

                //    dataTable.DefaultCell.Padding = 3;

                //    dataTable.DefaultCell.BorderWidth = 1;

                //    dataTable.WidthPercentage = 100;
                //    dataTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                //    foreach (var s in columns)
                //    {
                //        dataTable.DefaultCell.GrayFill = 0.7f;
                //        dataTable.DefaultCell.Padding = 5;

                //        dataTable.AddCell(s.Replace("\"", ""));
                //        dataTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    }


                //    dataTable.HeaderRows = 1; // this is the end of the table header

                //    dataTable.DefaultCell.BorderWidth = 1;

                //    for (var i = 2; i < rows.Length; i++)
                //    {
                //        dataTable.DefaultCell.GrayFill = i % 2 == 1 ? 0.9f : 1f;

                //        var values = rows[i].Split('|');
                //        for (var x = 0; x < columns.Length; x++)
                //        {
                //            dataTable.DefaultCell.HorizontalAlignment = x == 0
                //                                                            ? Element.ALIGN_LEFT
                //                                                            : Element.ALIGN_CENTER;
                //            var value = values[x] ?? "";
                //            dataTable.AddCell(value.Replace("\"", ""));
                //        }


                //    }

                //    document.Add(dataTable);

                //    document.Close();


                //    var idFile = TempFileManagerProvider.TempFileManager.SaveFile(memStream.ToArray());

                //    return String.Format("{{ \"fileId\" : \"{0}\", \"fileName\": \"{1}\" }}", idFile, fileName);
                //}
                //else
                //{
                var report = new Siberix.Report.Report();

                report.Info.Title = "Ranking Monitor Report";
                report.Info.Author = "SEOToolSet";
                report.Info.Creator = "SEOToolSet";
                report.Info.Copyright = "© SEOToolSet, USA";

                var section = report.AddSection();
                section.Size = Siberix.Report.PageSize.A4;
                section.Orientation = Siberix.Report.Orientation.Landscape;
                section.Spacings.All = 35;
                var titleStyle = new Siberix.Report.Text.Style(new Siberix.Graphics.Fonts.ArialBold(16), Siberix.Graphics.Brushes.Black);
                var headerStyle = new Siberix.Report.Text.Style(new Siberix.Graphics.Fonts.ArialBold(12), Siberix.Graphics.Brushes.Black);

                var rowStyle = new Siberix.Report.Text.Style(new Siberix.Graphics.Fonts.Arial(10), Siberix.Graphics.Brushes.Black);

                var text = section.AddText();
                text.Style = titleStyle;
                text.AddContent(firstRow);

                var headerRow = rows[1];
                var columns = headerRow.Split('|');

                var grid = section.AddGrid();
                grid.Borders = new Siberix.Report.Borders(Siberix.Graphics.Pens.Black);
                grid.Spacings.All = 5;
                grid.Background = new Siberix.Report.Background(new Siberix.Graphics.Color(240, 240, 220));

                // columns

                foreach (var s in columns)
                {
                    var column = grid.AddColumn();
                    column.Width = new Siberix.Report.RelativeWidth((float)(100.0 / columns.Length));
                }


                // Header
                var header = grid.AddHeader();
                header.Repeat = true;
                var row = header.AddRow();

                foreach (var s in columns)
                {
                    var cell = row.AddCell();
                    cell.Alignment.Horizontal = Siberix.Report.HorizontalAlignment.Center;
                    cell.Borders = new Siberix.Report.Borders(Siberix.Graphics.Pens.Black);
                    cell.Paddings.All = 5;
                    cell.Background = new Siberix.Report.Background
                                          {
                                              Brush =
                                                  new Siberix.Graphics.LinearGradientBrush(Siberix.Graphics.Colors.LightSteelBlue,
                                                                                           Siberix.Graphics.Colors.
                                                                                               WhiteSmoke, (float)90.0)
                                          };

                    text = cell.AddText();
                    text.Style = headerStyle;
                    text.AddContent(s);

                }

                for (var i = 2; i < rows.Length; i++)
                {
                    row = grid.AddRow();
                    var alternateBackground = i % 2 == 0;
                    var values = rows[i].Split('|');
                    for (var x = 0; x < columns.Length; x++)
                    {
                        var value = values[x] ?? "";
                        var cell = row.AddCell();

                        cell.Alignment.Horizontal = x == 0 ? Siberix.Report.HorizontalAlignment.Left : Siberix.Report.HorizontalAlignment.Center;
                        cell.Borders = new Siberix.Report.Borders(Siberix.Graphics.Pens.Black);
                        cell.Paddings.All = 5;
                        text = cell.AddText();
                        text.Style = rowStyle;
                        text.AddContent(value);
                        if (alternateBackground)
                            cell.Background =
                                new Siberix.Report.Background(new Siberix.Graphics.Color(255, 255, 255));
                    }

                }

                var memStream = new MemoryStream();
                report.Publish(memStream, Siberix.Report.FileFormat.PDF);

                var idFile = TempFileManagerProvider.TempFileManager.SaveFile(memStream.ToArray());
                return String.Format("{{ \"fileId\" : \"{0}\", \"fileName\": \"{1}\" }}", idFile, fileName);

                //}
            }
            catch (Exception ex)
            {
                LoggerFacade.Log.LogException(GetType(), ex);
            }
            // return -1 to indicate no file was created
            return "{ \"fileId\" : \"-1\" }";
        }


        private static string GetProxiesInRankingMonitorReportRun()
        {
            var idReportToView = HttpContext.Current.Request.Form["idRankingMonitorRunToView"];
            var idReportToCompare = HttpContext.Current.Request.Form["idRankingMonitorRunToCompare"];

            var proxiesInView = RankingMonitorManager.GetProxiesInRankingMonitorReportRun(int.Parse(idReportToView));
            var proxiesInCompare =
                RankingMonitorManager.GetProxiesInRankingMonitorReportRun(int.Parse(idReportToCompare));

            var proxiesMixed = new List<ProxyServerWrapper>();

            foreach (var proxy in proxiesInView)
            {
                proxiesMixed.Add(proxy);
            }

            foreach (var proxyInCompare in proxiesInCompare)
            {
                var proxyInCompare1 = proxyInCompare;
                if (proxiesMixed.Find(proxy => proxy.Id == proxyInCompare1.Id) != null) continue;
                proxiesMixed.Add(proxyInCompare);
            }

            return SerializeHelper.GetJsonResult(proxiesMixed, ObjectSerializerType.Object);

        }

        private static string ReturnDetailedRankingMonitorReport()
        {
            var idReportToView = HttpContext.Current.Request.Form["idRankingMonitorRunToView"];
            var idReportToCompare = HttpContext.Current.Request.Form["idRankingMonitorRunToCompare"];

            var idSearchEngineFilter = HttpContext.Current.Request.Form["idSearchEngineFilter"];
            var idProxyServerFilter = HttpContext.Current.Request.Form["idProxyServerFilter"];

            var searchEnginefilterArray = idSearchEngineFilter.Split(',');
            var proxiesFilterArray = idProxyServerFilter.Split(',');

            var keywordAnalysisWrapperToViewCollection = new List<RankingMonitorDetailedAnalysis>();

            var keywordAnalyisCollectionToView = RankingMonitorManager.GetKeywordAnalysis(int.Parse(idReportToView));
            var keywordsPerEnginePerProxyToView = RankingMonitorManager.GetKeywordsEnginesPerProxy(int.Parse(idReportToView), searchEnginefilterArray, proxiesFilterArray) as List<EnginesPerProxyResultView>;

            foreach (var ka in keywordAnalyisCollectionToView)
            {
                var kaTemp = (RankingMonitorDetailedAnalysis)ka;
                keywordAnalysisWrapperToViewCollection.Add(kaTemp);
                FindEnginesPerProxyResult(kaTemp, keywordsPerEnginePerProxyToView);
            }

            var keywordAnalysisWrapperToCompareCollection = new List<RankingMonitorDetailedAnalysis>();

            var keywordAnalyisCollectionToCompare = RankingMonitorManager.GetKeywordAnalysis(int.Parse(idReportToCompare));
            var keywordsPerEnginePerProxyToCompare = RankingMonitorManager.GetKeywordsEnginesPerProxy(int.Parse(idReportToCompare), searchEnginefilterArray, proxiesFilterArray) as List<EnginesPerProxyResultView>;

            foreach (var ka in keywordAnalyisCollectionToCompare)
            {
                var kaTemp = (RankingMonitorDetailedAnalysis)ka;
                keywordAnalysisWrapperToCompareCollection.Add(kaTemp);
                FindEnginesPerProxyResult(kaTemp, keywordsPerEnginePerProxyToCompare);
            }

            return string.Format("{{ \"keywordAnalisis\" : {0}, \"keywordAnalisisToCompare\" : {1} }} ",
                SerializeHelper.GetJsonResult(keywordAnalysisWrapperToViewCollection, ObjectSerializerType.Object),
                SerializeHelper.GetJsonResult(keywordAnalysisWrapperToCompareCollection, ObjectSerializerType.Object));
        }

        private static void FindEnginesPerProxyResult(RankingMonitorDetailedAnalysis temp, List<EnginesPerProxyResultView> enginePerProxy)
        {
            var subResult =
                enginePerProxy.FindAll(element => element.Keyword == temp.Keyword);

            foreach (var view in subResult)
            {
                temp.EnginesPerProxyResults.Add(view);
            }
        }

        private static string GetJSONSearchEnginesInRankingMonitorRun(string idRankingMonitorRun)
        {
            var searchEnginesCountries =
                RankingMonitorManager.GetSearchEnginesCountriesByRankingMonitorRun(int.Parse(idRankingMonitorRun));
            var searchEnginesCountriesWrapper = new List<SearchEngineCountryWrapperForReport>();
            foreach (var searchEnginesCountry in searchEnginesCountries)
                searchEnginesCountriesWrapper.Add(searchEnginesCountry);
            var result = SerializeHelper.GetJsonResult(searchEnginesCountriesWrapper, ObjectSerializerType.Object);
            return result;
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


}
