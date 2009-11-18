using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SEOToolSet.Model
{
    public  class Project
    {
        public  Int32 Id { get; set; }

        public  string Domain { get; set; }

        public  string ClientName { get; set; }

        public  string ContactEmail { get; set; }

        public  string ContactName { get; set; }

        public  string ContactPhone { get; set; }

        [SoapElement(IsNullable = true)]
        public  DateTime? CreatedDate { get; set; }

        [SoapElement(IsNullable = true)]
        public  DateTime? UpdatedDate { get; set; }

        public  string CreatedBy { get; set; }

        public  string UpdatedBy { get; set; }

        [SoapElement(IsNullable = true)]
        public  Boolean? Enabled { get; set; }

        public  Account Account { get; set; }

        //public  IList<Competitor> Competitor { get; set; }

        //public  IList<KeywordList> KeywordList { get; set; }

        public  IList<ProjectUser> ProjectUser { get; set; }

        public  String Name { get; set; }
    }
}
