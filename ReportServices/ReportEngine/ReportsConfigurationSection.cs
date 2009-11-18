#region Using Directives

using System.Configuration;

#endregion

namespace SEOToolSetReportServices.ReportEngine
{
    public class ReportsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Reporters")]
        public ReportSettingsCollection Reporters
        {
            get { return (ReportSettingsCollection) base["Reporters"]; }
        }
    }
}