#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class WordMetricsReport
    {
        public IList<WordMetric> WordMetrics { get; set; }
        public SummaryUsedWords UsedWords { get; set; }

        #region Nested type: SummaryUsedWords

        public class SummaryUsedWords
        {
            public string Total { get; set; }
            public string Words { get; set; }
        }

        #endregion

        #region Nested type: WordMetric

        public class WordMetric
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
        }

        #endregion
    }
}