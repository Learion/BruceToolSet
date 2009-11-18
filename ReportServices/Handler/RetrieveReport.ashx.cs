#region Using Directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Web;
using LoggerFacade;
using SEOToolSet.Common;
using SEOToolSetReportServices.ReportEngine;

#endregion

namespace SEOToolSetReportServices.Handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class RetrieveReport : IHttpHandler
    {
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var tokenReportIdentifier = context.Request.QueryString[Constants.KindOfReportParameter];
                var reportService = CreateInstanceBasedOnToken(tokenReportIdentifier);

                //Retrieve the class name parameters
                var classNames = new StringDictionary();
                foreach (var styleName in typeof (StyleName).GetFields())
                    classNames.Add(styleName.Name, context.Request[styleName.GetValue(null).ToString()]);

                var textResponse = reportService.getReport(context.Request.QueryString);

                //it waits 1 up to 10 seconds
                var rnd = new Random(DateTime.Now.Millisecond).Next(1, 7);
                Thread.Sleep(rnd*1000);
                Log.Debug(GetType(),
                          "Time loading the report: " + rnd.ToString(CultureInfo.InvariantCulture) + " seconds");
                // *** end ***
                textResponse = ReportHelper.FormatResultTokens(textResponse, classNames);

                if (!String.IsNullOrEmpty(context.Request[Constants.JsonCallbackParameter]))
                    textResponse = String.Format(CultureInfo.InvariantCulture, "{0}({1});",
                                                 context.Request[Constants.JsonCallbackParameter], textResponse);

                context.Response.StatusCode = (int) HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                context.Response.Write(textResponse);
            }
            catch (Exception ex)
            {
                Log.LogException(GetType(), ex);
            }
        }


        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        private static ReportService CreateInstanceBasedOnToken(string identifier)
        {
            var instance = ReportHelper.FindInCache(identifier);

            if (instance == null)
            {
                var reportMappingElement = ReportHelper.registeredReporters[identifier];
                var settingsType = Type.GetType(reportMappingElement.ClassName);

                if (settingsType == null)
                    throw new ConfigurationErrorsException(String.Format("Could not find type: {0}",
                                                                         reportMappingElement.ClassName));

                instance = Activator.CreateInstance(settingsType) as ReportService;
                ReportHelper.PutInCache(identifier, instance);
            }
            return instance;
        }
    }
}