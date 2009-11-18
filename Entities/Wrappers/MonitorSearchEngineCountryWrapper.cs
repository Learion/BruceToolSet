namespace SEOToolSet.Entities.Wrappers
{
    public class MonitorSearchEngineCountryWrapper
    {
        public virtual int Id { get; set; }

        public virtual SearchEngineCountryWrapper SearchEngineCountry { get; set; }

        public virtual ProjectWrapper Project { get; set; }

        public static implicit operator MonitorSearchEngineCountryWrapper(
            MonitorSearchEngineCountry monitorSearchEngineCountry)
        {
            if (monitorSearchEngineCountry == null) return null;
            return new MonitorSearchEngineCountryWrapper
                       {
                           Id = monitorSearchEngineCountry.Id,
                           Project = monitorSearchEngineCountry.Project,
                           SearchEngineCountry = monitorSearchEngineCountry.SearchEngineCountry
                       };
        }
    }
}