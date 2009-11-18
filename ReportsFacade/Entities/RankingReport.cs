#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class RankingReport
    {
        public RankingReport()
        {
            Data = new List<RankingReportRow>
                       {
                           new RankingReportRow
                               {
                                   Keyword = "search engine marketing",
                                   Activity = 14268,
                                   GoogleResults = 457000000,
                                   CostPerClick = "$5.45",
                                   AllInTitle = (int) (.4*457000000),
                                   GoogleOlder = 15,
                                   GoogleNewer = 14,
                                   GoogleDiff = 1,
                                   YahooOlder = 0,
                                   YahooNewer = 0,
                                   YahooDiff = 0,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "search engine optimization",
                                   Activity = 11701,
                                   GoogleResults = 26400000,
                                   CostPerClick = "$5.19",
                                   AllInTitle = (int) (.4*26400000),
                                   GoogleOlder = 4,
                                   GoogleNewer = 5,
                                   GoogleDiff = -1,
                                   YahooOlder = 11,
                                   YahooNewer = 9,
                                   YahooDiff = 2,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "web promotion advice",
                                   Activity = 2,
                                   GoogleResults = 2410000,
                                   CostPerClick = "$1.02",
                                   AllInTitle = (int) (.4*2410000),
                                   GoogleOlder = 5,
                                   GoogleNewer = 5,
                                   GoogleDiff = 0,
                                   YahooOlder = 0,
                                   YahooNewer = 0,
                                   YahooDiff = 0,
                                   LiveOlder = 0,
                                   LiveNewer = 34,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "search engine ranking tips",
                                   Activity = 1,
                                   GoogleResults = 6110000,
                                   CostPerClick = "$1.12",
                                   AllInTitle = (int) (.4*6110000),
                                   GoogleOlder = 2,
                                   GoogleNewer = 2,
                                   GoogleDiff = 0,
                                   YahooOlder = 19,
                                   YahooNewer = 6,
                                   YahooDiff = 13,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "seo code of ethics",
                                   Activity = 1,
                                   GoogleResults = 934000,
                                   CostPerClick = "$0.69",
                                   AllInTitle = (int) (.4*934000),
                                   GoogleOlder = 1,
                                   GoogleNewer = 1,
                                   GoogleDiff = 0,
                                   YahooOlder = 1,
                                   YahooNewer = 1,
                                   YahooDiff = 0,
                                   LiveOlder = 8,
                                   LiveNewer = 1,
                                   LiveDiff = 7
                               },
                           new RankingReportRow
                               {
                                   Keyword = "search engine ranking",
                                   Activity = 2681,
                                   GoogleResults = 5560000,
                                   CostPerClick = "$3.56",
                                   AllInTitle = (int) (.4*5560000),
                                   GoogleOlder = 9,
                                   GoogleNewer = 10,
                                   GoogleDiff = -1,
                                   YahooOlder = 25,
                                   YahooNewer = 14,
                                   YahooDiff = 11,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "search engine placement",
                                   Activity = 2368,
                                   GoogleResults = 7410000,
                                   CostPerClick = "$3.25",
                                   AllInTitle = (int) (.4*7410000),
                                   GoogleOlder = 14,
                                   GoogleNewer = 14,
                                   GoogleDiff = 0,
                                   YahooOlder = 0,
                                   YahooNewer = 47,
                                   YahooDiff = 0,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "website optimization",
                                   Activity = 933,
                                   GoogleResults = 12900000,
                                   CostPerClick = "$4.86",
                                   AllInTitle = (int) (.4*12900000),
                                   GoogleOlder = 13,
                                   GoogleNewer = 14,
                                   GoogleDiff = -1,
                                   YahooOlder = 0,
                                   YahooNewer = 0,
                                   YahooDiff = 0,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "web site ranking",
                                   Activity = 321,
                                   GoogleResults = 154000000,
                                   CostPerClick = "$4.96",
                                   AllInTitle = (int) (.4*154000000),
                                   GoogleOlder = 29,
                                   GoogleNewer = 28,
                                   GoogleDiff = 1,
                                   YahooOlder = 15,
                                   YahooNewer = 14,
                                   YahooDiff = 1,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "search engine optimization software",
                                   Activity = 171,
                                   GoogleResults = 6740000,
                                   CostPerClick = "$1.23",
                                   AllInTitle = (int) (.4*6740000),
                                   GoogleOlder = 0,
                                   GoogleNewer = 0,
                                   GoogleDiff = 0,
                                   YahooOlder = 15,
                                   YahooNewer = 14,
                                   YahooDiff = 1,
                                   LiveOlder = 0,
                                   LiveNewer = 0,
                                   LiveDiff = 0
                               },
                           new RankingReportRow
                               {
                                   Keyword = "search engine relationship chart",
                                   Activity = 1,
                                   GoogleResults = 1750000,
                                   CostPerClick = "$0.75",
                                   AllInTitle = (int) (.4*1750000),
                                   GoogleOlder = 0,
                                   GoogleNewer = 0,
                                   GoogleDiff = 0,
                                   YahooOlder = 15,
                                   YahooNewer = 14,
                                   YahooDiff = 1,
                                   LiveOlder = 0,
                                   LiveNewer = 11,
                                   LiveDiff = 0
                               },
                       };
        }

        public List<RankingReportRow> Data { get; set; }
    }

    public class RankingReportRow
    {
        public string Keyword { get; set; }
        public int Activity { get; set; }
        public int GoogleResults { get; set; }
        public string CostPerClick { get; set; }
        public int AllInTitle { get; set; }
        public int GoogleOlder { get; set; }
        public int YahooOlder { get; set; }
        public int LiveOlder { get; set; }
        public int GoogleNewer { get; set; }
        public int YahooNewer { get; set; }
        public int LiveNewer { get; set; }
        public int GoogleDiff { get; set; }
        public int YahooDiff { get; set; }
        public int LiveDiff { get; set; }

        public string GoogleComparison
        {
            get { return String.Format("{0,2:n0} / {1,2:n0} / {2,3:n0}", GoogleOlder, GoogleNewer, GoogleDiff); }
            set { throw new NotImplementedException(); }
        }

        public string YahooComparison
        {
            get { return String.Format("{0,2:n0} / {1,2:n0} / {2,3:n0}", YahooOlder, YahooNewer, YahooDiff); }
            set { throw new NotImplementedException(); }
        }

        public string LiveComparison
        {
            get { return String.Format("{0,2:n0} / {1,2:n0} / {2,3:n0}", LiveOlder, LiveNewer, LiveDiff); }
            set { throw new NotImplementedException(); }
        }
    }
}