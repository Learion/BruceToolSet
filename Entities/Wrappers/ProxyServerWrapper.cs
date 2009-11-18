namespace SEOToolSet.Entities.Wrappers
{
    public class ProxyServerWrapper
    {
        public int Id { get; set; }
        public CountryWrapper Country { get; set; }
        public string City { get; set; }
        public int? Importance { get; set; }

        public static implicit operator ProxyServerWrapper(ProxyServer proxyServer)
        {
            if (proxyServer == null) return null;
            return new ProxyServerWrapper
                       {
                           Id = proxyServer.Id,
                           Country = proxyServer.Country,
                           City = proxyServer.City,
                           Importance = proxyServer.ImportanceLevel
                       };
        }
    }
}