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
    [XmlInclude(typeof (MonitorProxyServer))]
    [SoapInclude(typeof (MonitorProxyServer))]
    [XmlInclude(typeof (RankingMonitorDeepRun))]
    [SoapInclude(typeof (RankingMonitorDeepRun))]
    public class AbstractProxyServer
    {
        public virtual int Id { get; set; }

        public virtual Country Country { get; set; }

        public virtual string City { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? ImportanceLevel { get; set; }

        public virtual IList<MonitorProxyServer> MonitorProxyServer { get; set; }

        public virtual IList<RankingMonitorDeepRun> RankingMonitorDeepRun { get; set; }
    }

    [Serializable]
    public class ProxyServer : AbstractProxyServer
    {
    }
}