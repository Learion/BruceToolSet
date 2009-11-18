namespace SEOToolSet.Entities.Wrappers
{
    public class SearchEngineCountryWrapperForReport
    {
        public int Id { get; set; }
        public string UrlLogo { get; set; }
        public string SearchEngineName { get; set; }
        public string SearchEngineUrl { get; set; }

        public static implicit operator SearchEngineCountryWrapperForReport(SearchEngineCountry searchEngineCountry)
        {
            if (searchEngineCountry == null) return null;
            var sec = new SearchEngineCountryWrapperForReport
                          {
                              Id = searchEngineCountry.Id
                          };
            if (searchEngineCountry.SearchEngine == null) return sec;

            sec.SearchEngineName = searchEngineCountry.SearchEngine.Name;
            sec.SearchEngineUrl = searchEngineCountry.Url;
            sec.UrlLogo = searchEngineCountry.SearchEngine.UrlLogo;

            return sec;
        }
    }
}