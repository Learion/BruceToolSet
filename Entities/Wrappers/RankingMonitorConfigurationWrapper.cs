#region

using System;
using System.Collections.Generic;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class RankingMonitorConfigurationWrapper
    {
        public virtual string MonitorUpdatedBy { get; set; }

        public virtual DateTime? MonitorUpdatedDate { get; set; }

        public virtual FrequencyWrapper Frequency { get; set; }

        public virtual IList<MonitorKeywordListWrapper> MonitorKeywordList { get; set; }

        public virtual IList<MonitorProxyServerWrapper> MonitorProxyServer { get; set; }

        public virtual IList<MonitorSearchEngineCountryWrapper> MonitorSearchEngineCountry { get; set; }

        public static implicit operator RankingMonitorConfigurationWrapper(
            RankingMonitorConfiguration rankingMonitorConfiguration)
        {
            if (rankingMonitorConfiguration == null) return null;
            var monitorKeywordLists = new List<MonitorKeywordListWrapper>();
            var monitorProxyServers = new List<MonitorProxyServerWrapper>();
            var monitorSearchEngineCountrys = new List<MonitorSearchEngineCountryWrapper>();
            foreach (MonitorKeywordList monitorKeywordList in rankingMonitorConfiguration.MonitorKeywordList)
                monitorKeywordLists.Add(monitorKeywordList);
            foreach (MonitorProxyServer monitorProxyServer in rankingMonitorConfiguration.MonitorProxyServer)
                monitorProxyServers.Add(monitorProxyServer);
            foreach (
                MonitorSearchEngineCountry monitorSearchEngineCountry in
                    rankingMonitorConfiguration.MonitorSearchEngineCountry)
                monitorSearchEngineCountrys.Add(monitorSearchEngineCountry);
            return new RankingMonitorConfigurationWrapper
                       {
                           Frequency = rankingMonitorConfiguration.Frequency,
                           MonitorKeywordList = monitorKeywordLists,
                           MonitorProxyServer = monitorProxyServers,
                           MonitorSearchEngineCountry = monitorSearchEngineCountrys,
                           MonitorUpdatedBy = rankingMonitorConfiguration.MonitorUpdatedBy,
                           MonitorUpdatedDate = rankingMonitorConfiguration.MonitorUpdatedDate
                       };
        }
    }
}