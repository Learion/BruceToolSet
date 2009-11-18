#region

using System;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class KeywordDeepAnalysisWrapper
    {
        public virtual int Id { get; set; }

        public virtual string Keyword { get; set; }

        public virtual Int32? Pages { get; set; }

        public virtual string Status { get; set; }

        public static implicit operator KeywordDeepAnalysisWrapper(KeywordDeepAnalysis keywordDeepAnalysis)
        {
            return keywordDeepAnalysis == null
                       ? null
                       : new KeywordDeepAnalysisWrapper
                             {
                                 Id = keywordDeepAnalysis.Id,
                                 Keyword = keywordDeepAnalysis.Keyword,
                                 Pages = keywordDeepAnalysis.Pages,
                                 Status = keywordDeepAnalysis.Status
                             };
        }
    }
}