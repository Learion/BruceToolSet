#region Using Directives

using System;
using System.Collections.Specialized;
using SEOToolSet.Common;
using SEOToolSet.ReportsFacade;
using SEOToolSetReportServices.ReportEngine;

#endregion

namespace SEOToolSetReportServices.Reporters
{
    public class ToolSetKeywordsReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetToolSetKeywords(uri) : null;
        }
    }

    public class OptimizedKeywordsReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri)
                       ? ServiceCaller.GetOptimizedKeywordsReport(uri)
                       : null;
        }
    }

    public class TagInformationReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetTagInformation(uri) : null;
        }
    }

    public class WordPhrasesReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetWordPhrases(uri) : null;
        }
    }

    public class LinearKeywordDistributionReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri)
                       ? ServiceCaller.GetLinearKeywordDistributionReport(uri)
                       : null;
        }
    }

    public class LinkTextReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetLinkTextReport(uri) : null;
        }
    }

    public class WordMetricsReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetWordMetrics(uri) : null;
        }
    }

    public class SiteInfoReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetSiteInfo(uri) : null;
        }
    }

    public class HeaderInfoReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetHeaderInfo(uri) : null;
        }
    }

    public class CloakCheckInfoReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetCloakCheckInfo(uri) : null;
        }
    }

    public class MonitorReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetMonitorReport(uri) : null;
        }
    }

    public class RankingReporter : ReportService
    {
        public override string getReport(NameValueCollection parameters)
        {
            var uriRequested = parameters[Constants.UriPageParameter];
            Uri uri;
            return Uri.TryCreate(uriRequested, UriKind.Absolute, out uri) ? ServiceCaller.GetRankingReport(uri) : null;
        }
    }
}