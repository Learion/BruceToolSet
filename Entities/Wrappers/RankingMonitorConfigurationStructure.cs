#region

using System.Collections.Generic;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class RankingMonitorConfigurationStructure
    {
        public IList<FrequencyWrapper> Frequency { get; set; }

        public IList<KeywordListWrapper> KeywordLists { get; set; }

        public IList<ProxyServerWrapper> ProxyServers { get; set; }

        public IList<SearchEngineCountryWrapper> SearchEngineCountry { get; set; }

        public LastRun LastRun { get; set; }
    }

    public class LastRun
    {
        public string LastDate { get; set; }
        public string LastTime { get; set; }
    }
}