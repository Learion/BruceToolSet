using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;

namespace SEOToolSet.WebApp.Handler
{
    public static class VersionHelper
    {
        public static VersionHelperContainer ApplicationVersionInformation
        {
            get
            {
                var vh = new VersionHelperContainer();

                try
                {
                    //SEOToolSetVersion
                    vh.SEOToolSetWebAppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    vh.ReportServicesVersions = ReportServicesVersion;
                    vh.TempFileManagerServiceVersion = Assembly.Load("SEOToolSet.TempFileManagerProvider").GetName().Version.ToString();
                    var TempFileManagerFSProvider = Assembly.Load("SEOToolSet.TempFileManagerFSProvider");
                    vh.TempFileManagerProviderName = TempFileManagerFSProvider.GetName().Name;
                    vh.TempFileManagerProviderVersion = TempFileManagerFSProvider.GetName().Version.ToString();
                }
                catch ( Exception ex )
                {
                    LoggerFacade.Log.LogException(typeof(VersionHelperContainer), ex);
                }

                return vh;
            }
        }

        private static String ReportServicesVersion
        {
            get
            {
                try
                {
                    var stringUri = ConfigurationManager.AppSettings["VersionHandlerURL"];
                    var webrequest = (HttpWebRequest)WebRequest.Create(stringUri);
                    var response = webrequest.GetResponse() as HttpWebResponse;
                    if (response != null)
                    {
                        var sr = new StreamReader(response.GetResponseStream());
                        return sr.ReadToEnd();
                    }
                    LoggerFacade.Log.Error(typeof(VersionHelperContainer), "Could not retrieve the Version of the ReportServices, Response was Null");
                    return "null";
                }
                catch ( Exception ex )
                {
                    LoggerFacade.Log.LogException(typeof(VersionHelperContainer), ex);
                    return "null";
                }
            }
        }
    }
}
