﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
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
    [XmlInclude(typeof (RankingMonitorDeepRun))]
    [SoapInclude(typeof (RankingMonitorDeepRun))]
    [XmlInclude(typeof (MonitorSearchEngineCountry))]
    [SoapInclude(typeof (MonitorSearchEngineCountry))]
    public class AbstractSearchEngineCountry
    {
        public virtual int Id { get; set; }

        public virtual string Url { get; set; }

        public virtual IList<RankingMonitorDeepRun> RankingMonitorDeepRun { get; set; }

        public virtual SearchEngine SearchEngine { get; set; }

        public virtual Country Country { get; set; }

        public virtual IList<MonitorSearchEngineCountry> MonitorSearchEngineCountry { get; set; }
    }

    [Serializable]
    public class SearchEngineCountry : AbstractSearchEngineCountry
    {
    }
}