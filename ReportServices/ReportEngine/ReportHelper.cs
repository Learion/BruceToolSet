#region Using Directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;

#endregion

namespace SEOToolSetReportServices.ReportEngine
{
    public static class ReportHelper
    {
        public static ReportSettingsCollection registeredReporters;

        static ReportHelper()
        {
            var reportsSettings = ConfigurationManager.GetSection("ReportsSettings") as ReportsConfigurationSection;
            if (reportsSettings == null)
            {
                throw new Exception("ReportsSettings not found");
            }

            registeredReporters = reportsSettings.Reporters;
        }

        public static ReportService FindInCache(string name)
        {
            return HttpContext.Current.Cache.Get(name) as ReportService;
        }

        public static void PutInCache(string name, ReportService reportService)
        {
            //HttpContext.Current.Cache[name] = reportService;
            HttpContext.Current.Cache.Insert(name, reportService, null, Cache.NoAbsoluteExpiration,
                                             TimeSpan.FromMinutes(20));
        }

        public static string FormatResultTokens(
            string resultText,
            StringDictionary styleNames)
        {
            var array = new string[styleNames.Keys.Count];
            styleNames.Keys.CopyTo(array, 0);

            foreach (var styleName in array)
            {
                if (string.IsNullOrEmpty(styleNames[styleName]))
                    styleNames[styleName] = WebConfigurationManager.AppSettings[styleName];
                //Replacing the tokens in the result
                resultText = resultText.Replace("<" + styleNames[styleName] + ">",
                                                string.Format(CultureInfo.InvariantCulture, "<span class='{0}'>",
                                                              styleNames[styleName]));
                resultText = resultText.Replace("</" + styleNames[styleName] + ">", "</span>");
            }
            resultText = resultText.Replace("\\n", "<br />");
            return resultText;
        }
    }
}