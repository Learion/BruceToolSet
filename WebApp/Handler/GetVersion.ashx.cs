using System;
using System.Web;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class GetVersion : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";


            var result = VersionHelper.ApplicationVersionInformation.ToJSON();

            System.Threading.Thread.Sleep(2000);

            if (!String.IsNullOrEmpty(context.Request["jsoncallback"]))
            {
                result = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}({1});", context.Request["jsoncallback"], result);
            }

            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
