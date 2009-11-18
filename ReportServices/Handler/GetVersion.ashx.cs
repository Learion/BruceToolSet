#region Using Directives

using System;
using System.Globalization;
using System.Web;
using SEOToolSet.Common;

#endregion

namespace SEOToolSetReportServices.Handler
{
    public class GetVersion : IHttpHandler
    {
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            String result = VersionHelper.ApplicationVersionInformation.ToJSON();

            if (!String.IsNullOrEmpty(context.Request[Constants.JsonCallbackParameter]))
            {
                result = String.Format(CultureInfo.InvariantCulture, "{0}({1});",
                                       context.Request[Constants.JsonCallbackParameter], result);
            }

            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion
    }
}