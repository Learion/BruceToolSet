#region Using Directives

using System;
using System.Configuration;

#endregion

namespace SEOToolSetReportServices.ReportEngine
{
    public class ReportMappingElement : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public String Name
        {
            get { return (String) this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("ClassName", IsRequired = true)]
        public string ClassName
        {
            get { return (string) this["ClassName"]; }
            set { this["ClassName"] = value; }
        }
    }
}