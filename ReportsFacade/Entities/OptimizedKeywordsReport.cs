#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class OptimizedKeywordsReport
    {
        public IList<Keyword> Keywords { get; set; }

        #region Nested type: Keyword

        public class Keyword
        {
            public string Name { get; set; }
            public string Used { get; set; }
        }

        #endregion
    }
}