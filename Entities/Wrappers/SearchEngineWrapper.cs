namespace SEOToolSet.Entities.Wrappers
{
    public class SearchEngineWrapper
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UrlLogo { get; set; }
        public string SearchEngineName { get; set; }
        public string SearchEngineUrl { get; set; }

        public static implicit operator SearchEngineWrapper(SearchEngine searchEngine)
        {
            return searchEngine == null
                       ? null
                       : new SearchEngineWrapper
                             {
                                 Id = searchEngine.Id,
                                 Description = searchEngine.Description,
                                 SearchEngineName = searchEngine.Name,
                                 SearchEngineUrl = searchEngine.UrlLogo
                             };
        }
    }
}