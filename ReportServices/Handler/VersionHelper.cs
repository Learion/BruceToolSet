#region Using Directives

using System.Reflection;

#endregion

namespace SEOToolSetReportServices.Handler
{
    public static class VersionHelper
    {
        public static VersionHelperContainer ApplicationVersionInformation
        {
            get
            {
                var vh = new VersionHelperContainer
                             {
                                 ReportServicesWebAppVersion =
                                     Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                                 ReportFacadeVersion =
                                     Assembly.Load("SEOToolSet.ReportsFacade").GetName().Version.ToString()
                             };


                return vh;
            }
        }

        ///// <summary>
        ///// Replace the tokens of the result with the appropriate style classes
        ///// </summary>
        ///// <param name="resultText"></param>
        ///// <param name="styleNames"></param>
        ///// <returns></returns>
        //public static string FormatResultTokens(
        //    string resultText,
        //    StringDictionary styleNames)
        //{

        //    var array = new string[styleNames.Keys.Count];
        //    styleNames.Keys.CopyTo(array, 0);

        //    foreach (var styleName in array)
        //    {
        //        if (string.IsNullOrEmpty(styleNames[styleName]))
        //            styleNames[styleName] = WebConfigurationManager.AppSettings[styleName];
        //        //Replacing the tokens in the result
        //        resultText = resultText.Replace("<" + styleNames[styleName] + ">", string.Format(CultureInfo.InvariantCulture, "<span class='{0}'>", styleNames[styleName]));
        //        resultText = resultText.Replace("</" + styleNames[styleName] + ">", "</span>");
        //    }
        //    resultText = resultText.Replace("\\n", "<br />");
        //    return resultText;
        //}

        ///// <summary>
        ///// Retrieve the result since the report was executed
        ///// </summary>
        ///// <param name="kindOfReport">Kind of report</param>
        ///// <param name="uriRequested">URI to analyze</param>
        ///// <returns>Object(s) as JSon</returns>
        //public static string ExecuteReport(GeneralReport kindOfReport,
        //                                    Uri uriRequested)
        //{
        //    //Depending on the indicated report, web methods used for the report are different each other
        //    switch (kindOfReport)
        //    {
        //        case GeneralReport.LinearKeywordDistribution:
        //            return ServiceCaller.GetLinearKeywordDistributionReport(uriRequested);
        //        case GeneralReport.LinkText:
        //            return ServiceCaller.GetLinkTextReport(uriRequested);
        //        case GeneralReport.OptimizedKeywords:
        //            return ServiceCaller.GetOptimizedKeywordsReport(uriRequested);
        //        case GeneralReport.TagInformation:
        //            return ServiceCaller.GetTagInformation(uriRequested);
        //        case GeneralReport.ToolSetKeywords:
        //            return ServiceCaller.GetToolSetKeywords(uriRequested);
        //        case GeneralReport.WordMetrics:
        //            return ServiceCaller.GetWordMetrics(uriRequested);
        //        case GeneralReport.WordPhrases:
        //            return ServiceCaller.GetWordPhrases(uriRequested);
        //        case GeneralReport.SiteInfo:
        //            return ServiceCaller.GetSiteInfo(uriRequested);
        //        case GeneralReport.HeaderInfo:
        //            return ServiceCaller.GetHeaderInfo(uriRequested);
        //        case GeneralReport.CloakCheckInfo:
        //            return ServiceCaller.GetCloakCheckInfo(uriRequested);
        //        case GeneralReport.Monitor:
        //            return ServiceCaller.GetMonitorReport(uriRequested);
        //        case GeneralReport.Ranking:
        //            return ServiceCaller.GetRankingReport(uriRequested);
        //        default:
        //            throw new ArgumentException("The kind of report is not defined", "kindOfReport");
        //    }
        //}
    }
}