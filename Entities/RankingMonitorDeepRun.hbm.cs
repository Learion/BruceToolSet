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
    [XmlInclude(typeof (KeywordDeepAnalysis))]
    [SoapInclude(typeof (KeywordDeepAnalysis))]
    public class AbstractRankingMonitorDeepRun
    {
        public virtual int Id { get; set; }

        public virtual string StatusReason { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? PageRank { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? InboundLinks { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? PagesIndexed { get; set; }

        public virtual IList<KeywordDeepAnalysis> KeywordDeepAnalysis { get; set; }

        public virtual ProxyServer ProxyServer { get; set; }

        public virtual RankingMonitorRun RankingMonitorRun { get; set; }

        public virtual SearchEngineCountry SearchEngineCountry { get; set; }

        public virtual Status Status { get; set; }
    }

    [Serializable]
    public class RankingMonitorDeepRun : AbstractRankingMonitorDeepRun
    {
    }
}