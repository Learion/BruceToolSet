#region

using System;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class KeywordAnalysisWrapper
    {
        public virtual int Id { get; set; }

        public virtual string Keyword { get; set; }

        public virtual Int32? GoogleResults { get; set; }

        public virtual Int32? AllInTitle { get; set; }

        public virtual Int32? AliasDomains { get; set; }

        public virtual Double? CPC { get; set; }

        public virtual Int32? DailySearches { get; set; }

        public virtual Int32? Results { get; set; }

        public virtual Int32? Engines { get; set; }

        public virtual Int32? Pages { get; set; }

        public virtual string Status { get; set; }

        public static implicit operator KeywordAnalysisWrapper(KeywordAnalysis keywordAnalysis)
        {
            return keywordAnalysis == null
                       ? null
                       : new KeywordAnalysisWrapper
                             {
                                 Id = keywordAnalysis.Id,
                                 AliasDomains = keywordAnalysis.AliasDomains,
                                 AllInTitle = keywordAnalysis.AllInTitle,
                                 CPC = keywordAnalysis.CPC,
                                 DailySearches = keywordAnalysis.DailySearches,
                                 GoogleResults = keywordAnalysis.GoogleResults,
                                 Keyword = keywordAnalysis.Keyword,
                                 Results = keywordAnalysis.Results,
                                 Engines = keywordAnalysis.Engines,
                                 Pages = keywordAnalysis.Pages,
                                 Status = keywordAnalysis.Status
                             };
        }
    }
}