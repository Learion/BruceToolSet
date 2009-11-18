using System;

namespace SEOToolSet.Entities
{
    [Serializable]
    public class AbstractEnginesPerProxyResultView
    {
        public virtual EnginesPerProxyResultViewKey Id { get; set; }
        public virtual string Keyword { get; set; }
        public virtual int? Pages { get; set; }
        public virtual string ProxyCity { get; set; }
        public virtual string ProxyCountry { get; set; }
        public virtual string SearchEngineUrl { get; set; }
        public virtual string SearchEngineName { get; set; }
        public virtual int? IdProxy { get; set; }
        public virtual int? IdSearchEngineCountry { get; set; }
        public virtual int? IdRankingMonitorDeepRun { get; set; }
        public virtual int? IdRankingMonitorRun { get; set; }
    }
    [Serializable]
    public class EnginesPerProxyResultViewKey
    {
        public virtual string Keyword { get; set; }
        public virtual int? IdProxy { get; set; }
        public virtual int? IdSearchEngineCountry { get; set; }
        public virtual int? IdRankingMonitorDeepRun { get; set; }
        public virtual int? IdRankingMonitorRun { get; set; }

        public override bool Equals(object other)
        {
            if (this == other) return true;

            var key = other as EnginesPerProxyResultViewKey;
            if (key == null) return false; // null or not a EnginesPerProxyResultViewKey

            return Keyword == key.Keyword &&
                   (IdProxy == key.IdProxy && (IdSearchEngineCountry == key.IdSearchEngineCountry &&
                                               (IdRankingMonitorRun == key.IdRankingMonitorRun &&
                                                IdRankingMonitorDeepRun == key.IdRankingMonitorDeepRun)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = Keyword.GetHashCode();
                result = result + IdProxy.GetHashCode();
                result = result + IdSearchEngineCountry.GetHashCode();
                result = result + IdRankingMonitorDeepRun.GetHashCode();
                result = 29 * result + IdRankingMonitorRun.GetHashCode();

                return result;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}_{1}_{2}_{3}_{4}", Keyword, IdProxy, IdSearchEngineCountry, IdRankingMonitorDeepRun, IdRankingMonitorRun);
        }

    }

    [Serializable]
    public class EnginesPerProxyResultView : AbstractEnginesPerProxyResultView
    {

    }
}
