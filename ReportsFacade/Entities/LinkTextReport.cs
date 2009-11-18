#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class LinkTextReport
    {
        public Summary SummaryLinks { get; set; }
        public IList<Link> Links { get; set; }

        #region Nested type: Link

        public class Link
        {
            public string LinkedTo { get; set; }
            public string Category { get; set; }
            public string Rel { get; set; }
            public string AnchorText { get; set; }
            public string Error { get; set; }
        }

        #endregion

        #region Nested type: Summary

        public class Summary
        {
            public int Total { get; set; }
            public int UniqueTargets { get; set; }
        }

        #endregion
    }
}