﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    [XmlInclude(typeof (Competitor))]
    [SoapInclude(typeof (Competitor))]
    [XmlInclude(typeof (KeywordList))]
    [SoapInclude(typeof (KeywordList))]
    [XmlInclude(typeof (ProjectUser))]
    [SoapInclude(typeof (ProjectUser))]
    [XmlInclude(typeof (MonitorKeywordList))]
    [SoapInclude(typeof (MonitorKeywordList))]
    [XmlInclude(typeof (MonitorProxyServer))]
    [SoapInclude(typeof (MonitorProxyServer))]
    [XmlInclude(typeof (RankingMonitorRun))]
    [SoapInclude(typeof (RankingMonitorRun))]
    [XmlInclude(typeof (MonitorSearchEngineCountry))]
    [SoapInclude(typeof (MonitorSearchEngineCountry))]
    public class AbstractProject
    {
        public virtual Int32 Id { get; set; }

        public virtual string Domain { get; set; }

        public virtual string ClientName { get; set; }

        public virtual string ContactEmail { get; set; }

        public virtual string ContactName { get; set; }

        public virtual string ContactPhone { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual DateTime? CreatedDate { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual DateTime? UpdatedDate { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual string UpdatedBy { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Boolean? Enabled { get; set; }

        public virtual Account Account { get; set; }

        public virtual IList<Competitor> Competitor { get; set; }

        public virtual IList<KeywordList> KeywordList { get; set; }

        public virtual IList<ProjectUser> ProjectUser { get; set; }

        public virtual String Name { get; set; }

        public virtual IList<RankingMonitorRun> RankingMonitorRun { get; set; }

        public virtual RankingMonitorConfiguration RankingMonitorConfiguration { get; set; }
    }

    [Serializable]
    public class Project : AbstractProject
    {
    }
}