#region

using System.Collections.Generic;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class RankingMonitorDetailedAnalysis
    {
        public List<EnginesPerProxyResult> EnginesPerProxyResults = new List<EnginesPerProxyResult>();
        public string Id { get; set; }
        public string Keyword { get; set; }
        public int? Activity { get; set; }
        public int? GoogleResults { get; set; }
        public double? CPC { get; set; }
        public int? AllInTitle { get; set; }
        public string Status { get; set; }

        public static implicit operator RankingMonitorDetailedAnalysis(KeywordAnalysis keywordAnalysis)
        {
            if (keywordAnalysis == null) return null;
            var row = new RankingMonitorDetailedAnalysis
                          {
                              Keyword = keywordAnalysis.Keyword,
                              Activity = keywordAnalysis.DailySearches,
                              GoogleResults = keywordAnalysis.GoogleResults,
                              CPC = keywordAnalysis.CPC,
                              AllInTitle = keywordAnalysis.AllInTitle,
                              Status = keywordAnalysis.Status
                          };
            return row;
        }
    }
}