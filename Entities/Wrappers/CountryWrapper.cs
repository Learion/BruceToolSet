namespace SEOToolSet.Entities.Wrappers
{
    public class CountryWrapper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FlagUrl { get; set; }
        public int SearchEngineImportance { get; set; }

        public static implicit operator CountryWrapper(Country country)
        {
            if (country == null) return null;
            return new CountryWrapper
                       {
                           Id = country.Id,
                           Name = country.Name,
                           FlagUrl = country.FlagUrl,
                           SearchEngineImportance = country.SearchEngineImportance
                       };
        }
    }
}