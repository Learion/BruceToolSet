using System;

namespace SEOToolSet.WebApp.Handler
{
    public class VersionHelperContainer
    {
        public String SEOToolSetWebAppVersion { get; set; }

        public String ReportServicesVersions { get; set; }

        public String TempFileManagerServiceVersion { get; set; }

        public String TempFileManagerProviderName { get; set; }

        public String TempFileManagerProviderVersion { get; set; }

        internal string ToJSON()
        {
            var resultTemplate = "{ \"Version\" : { \"SEOToolSetWebAppVersion\" : \"{0}\", \"ReportServicesWebAppVersion\" : {1}, " +
                "\"TempFileManagerServiceVersion\": \"{3}\", \"TempFileManagerProviderName\": \"{4}\", \"TempFileManagerProviderVersion\" : \"{5}\" }}";

            return resultTemplate.Replace("{0}", SEOToolSetWebAppVersion).Replace("{1}", ReportServicesVersions).Replace("{3}", TempFileManagerServiceVersion).Replace("{4}", TempFileManagerProviderName).Replace("{5}", TempFileManagerProviderVersion);
        }
    }


}
