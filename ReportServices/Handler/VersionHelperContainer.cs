#region Using Directives

using System;

#endregion

namespace SEOToolSetReportServices.Handler
{
    public class VersionHelperContainer
    {
        public String ReportServicesWebAppVersion { get; set; }

        public String ReportFacadeVersion { get; set; }

        internal string ToJSON()
        {
            var resultTemplate = "{  \"ReportServicesWebAppVersion\" : \"{0}\", \"ReportFacadeVersion\" : \"{1}\" }";

            return resultTemplate.Replace("{0}", ReportServicesWebAppVersion).Replace("{1}", ReportFacadeVersion);
        }
    }
}