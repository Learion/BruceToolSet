using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SEOToolSet.Model
{
    public class ProjectUser
    {
        public  Int32 Id { get; set; }

        [SoapElement(IsNullable = true)]
        public  Boolean? Enabled { get; set; }

        public  Project Project { get; set; }

        public  SEOToolsetUser SEOToolsetUser { get; set; }

        public  Boolean? MonitorEmails { get; set; }
    }
}
