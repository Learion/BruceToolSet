using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using LoggerFacade;
using SEOToolSet.TempFileManagerProvider;

namespace SEOToolSet.WebApp
{

    public class Global : HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //TODO: Remove this lines when the Demo has been finished
            var currentContext = HttpContext.Current;

            if (currentContext.Request.PhysicalPath.Contains(".aspx"))
            {
                var currentUri = currentContext.Request.Url;

                if (currentUri.OriginalString.Contains("Handler/Report/"))
                {
                    var queryString = currentUri.Query;
                    var newUrl = string.Format("~/Handler/RetrieveFile.ashx{0}", queryString);

                    currentContext.RewritePath(newUrl);
                }
                else
                {

                    var token = string.Empty;
                    if (currentUri.Segments.Length > 0)
                    {
                        token = currentUri.Segments[currentUri.Segments.Length - 1].Replace(".aspx", "");
                    }

                    if (!File.Exists(currentContext.Request.PhysicalPath))
                    {
                        //Log.Debug(GetType(), "Looking for Token : " + currentContext.Request.PhysicalPath);
                        currentContext.RewritePath(String.Format(CultureInfo.InvariantCulture, "~/Dummy.aspx?token={0}",
                                                                 token));
                    }
                }
            }
            SetCulture();
        }

        //protected void Session_End(object sender, EventArgs e)
        //{

        //}

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var ex = Server.GetLastError();
                if (ex != null && !(ex is ThreadAbortException))
                {
                    Log.LogException(GetType(), ex);
                }
            }
            catch (Exception)
            {
                //Last change here we can't do anything!!!
                Console.WriteLine("Exception trying to write to the log file");
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            TempFileManager.StopTimer();
        }

        private void SetCulture()
        {
            var cookie = Request.Cookies["CurrentCulture"];
            if (cookie == null || cookie.Value == null) return;

            var languageSelected = cookie.Value;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageSelected);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageSelected);
        }
    }
}