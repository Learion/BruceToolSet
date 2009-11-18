#region

using SEOToolSet.ViewEntities;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class EnginesPerProxyResult
    {
        public string Id { get; set; }
        public int? IdSearchEngineCountry { get; set; }
        public string SearchEngineName { get; set; }
        public string SearchEngineUrl { get; set; }
        public int? IdProxy { get; set; }
        public string ProxyCountry { get; set; }
        public string ProxyCity { get; set; }
        public int? PagesNumber { get; set; }

        public string Pages
        {
            get
            {
                return (PagesNumber != null && PagesNumber > 0 && PagesNumber <= 50)
                           ? string.Format("{0}", PagesNumber)
                           : "NR";
            }
        }

        public static implicit operator EnginesPerProxyResult(EnginesPerProxyResultView epprv)
        {
            if (epprv == null) return null;
            var row = new EnginesPerProxyResult
                          {
                              Id = epprv.Id.ToString(),
                              IdSearchEngineCountry = epprv.IdSearchEngineCountry,
                              SearchEngineName = epprv.SearchEngineName,
                              SearchEngineUrl = epprv.SearchEngineUrl,
                              IdProxy = epprv.IdProxy,
                              ProxyCountry = epprv.ProxyCountry,
                              ProxyCity = epprv.ProxyCity,
                              PagesNumber = epprv.Pages
                          };

            return row;
        }
    }

    //public class RankingMonitorDetailedAnalysisRow
    //{
    //    public string Keyword { get; set; }
    //    public int? Activity { get; set; }
    //    public int? GoogleResults { get; set; }
    //    public int? CPC { get; set; }
    //    public int? AllInTitle { get; set; }

    //    public EnginesPerProxyResult dynaColumn1 { get; set; }
    //    public EnginesPerProxyResult dynaColumn2 { get; set; }
    //    public EnginesPerProxyResult dynaColumn3 { get; set; }
    //    public EnginesPerProxyResult dynaColumn4 { get; set; }
    //    public EnginesPerProxyResult dynaColumn5 { get; set; }
    //    public EnginesPerProxyResult dynaColumn6 { get; set; }

    //    public static implicit operator RankingMonitorDetailedAnalysisRow(RankingMonitorDetailedAnalysis rmda)
    //    {
    //        if (rmda == null) return null;
    //        var row = new RankingMonitorDetailedAnalysisRow
    //                      {
    //                          Keyword = rmda.Keyword,
    //                          Activity = rmda.Activity,
    //                          GoogleResults = rmda.GoogleResults,
    //                          AllInTitle = rmda.AllInTitle,
    //                          CPC = rmda.CPC
    //                      };

    //        return row;
    //    }
    //}
}