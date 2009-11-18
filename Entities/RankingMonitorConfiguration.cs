#region

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    public class RankingMonitorConfiguration
    {
        public virtual string MonitorUpdatedBy { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual DateTime? MonitorUpdatedDate { get; set; }

        public virtual Frequency Frequency { get; set; }

        public virtual IList<MonitorKeywordList> MonitorKeywordList { get; set; }

        public virtual IList<MonitorProxyServer> MonitorProxyServer { get; set; }

        public virtual IList<MonitorSearchEngineCountry> MonitorSearchEngineCountry { get; set; }
    }
}