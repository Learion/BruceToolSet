#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class MonitorReport
    {
        public MonitorReport()
        {
            Data = new List<MonitorReportRow>
                       {
                           new MonitorReportRow
                               {
                                   Keyword = "search engine marketing",
                                   Pages = 2,
                                   Engines = 1,
                                   Activity = "14,268 for 52,500,000 results",
                                   CostPerClick = 5.45m,
                                   AllInTitle = 741,
                                   AliasDomains = 9
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "search engine optimization",
                                   Pages = 3,
                                   Engines = 1,
                                   Activity = "11,701 for 4,110,000 results",
                                   CostPerClick = 5.19m,
                                   AllInTitle = 123,
                                   AliasDomains = 6
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "web promotion advice",
                                   Pages = 2,
                                   Engines = 1,
                                   Activity = "2 for 52,500,000 results",
                                   CostPerClick = 1.02m,
                                   AllInTitle = 141,
                                   AliasDomains = 7
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "search engine ranking tips",
                                   Pages = 3,
                                   Engines = 2,
                                   Activity = "1 for 52,500,000 results",
                                   CostPerClick = 1.12m,
                                   AllInTitle = 652,
                                   AliasDomains = 8
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "seo code of ethics",
                                   Pages = 13,
                                   Engines = 3,
                                   Activity = "1 for 52,500,000 results",
                                   CostPerClick = 0.69m,
                                   AllInTitle = 741,
                                   AliasDomains = 8
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "search engine ranking",
                                   Pages = 3,
                                   Engines = 2,
                                   Activity = "2,681 for 52,500,000 results",
                                   CostPerClick = 3.56m,
                                   AllInTitle = 523,
                                   AliasDomains = 5
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "search engine placement",
                                   Pages = 2,
                                   Engines = 2,
                                   Activity = "2,368 for 52,500,000 results",
                                   CostPerClick = 3.25m,
                                   AllInTitle = 753,
                                   AliasDomains = 3
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "website optimization",
                                   Pages = 2,
                                   Engines = 1,
                                   Activity = "933 for 52,500,000 results",
                                   CostPerClick = 4.86m,
                                   AllInTitle = 749,
                                   AliasDomains = 2
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "web site ranking",
                                   Pages = 2,
                                   Engines = 2,
                                   Activity = "449 for 52,500,000 results",
                                   CostPerClick = 4.96m,
                                   AllInTitle = 852,
                                   AliasDomains = 4
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "search engine optimization software",
                                   Pages = 2,
                                   Engines = 1,
                                   Activity = "243 for 52,500,000 results",
                                   CostPerClick = 1.23m,
                                   AllInTitle = 456,
                                   AliasDomains = 7
                               },
                           new MonitorReportRow
                               {
                                   Keyword = "search engine relationship chart",
                                   Pages = 10,
                                   Engines = 2,
                                   Activity = "31 for 52,500,000 results",
                                   CostPerClick = 0.75m,
                                   AllInTitle = 623,
                                   AliasDomains = 3
                               }
                       };
        }

        public List<MonitorReportRow> Data { get; set; }
    }

    public class MonitorReportRow
    {
        public string Keyword { get; set; }
        public int Pages { get; set; }
        public int Engines { get; set; }
        public string Activity { get; set; }
        public decimal CostPerClick { get; set; }
        public int AllInTitle { get; set; }
        public int AliasDomains { get; set; }
    }
}