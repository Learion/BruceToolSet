namespace SEOToolSet.Entities.Wrappers
{
    public class SearchEngineCountryWrapper
    {
        public int Id { get; set; }
        public SearchEngineWrapper SearchEngine { get; set; }
        public CountryWrapper Country { get; set; }
        public string Url { get; set; }

        public static implicit operator SearchEngineCountryWrapper(SearchEngineCountry searchEngineCountry)
        {
            return searchEngineCountry == null
                       ? null
                       : new SearchEngineCountryWrapper
                             {
                                 Id = searchEngineCountry.Id,
                                 Country = searchEngineCountry.Country,
                                 SearchEngine = searchEngineCountry.SearchEngine,
                                 Url = searchEngineCountry.Url
                             };
        }
    }
}