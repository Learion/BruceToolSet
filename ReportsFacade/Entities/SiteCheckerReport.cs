#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class SiteCheckerReport
    {
        public IList<SiteCheckerCriterion> Criteria { get; set; }

        #region Nested type: SiteCheckerCriterion

        public class SiteCheckerCriterion
        {
            public string CheckedCriterion { get; set; }
            public string Description { get; set; }
        }

        #endregion
    }
}