namespace SEOToolSet.Entities.Wrappers
{
    public class MonitorProxyServerWrapper
    {
        public virtual int Id { get; set; }

        public virtual ProjectWrapper Project { get; set; }

        public virtual ProxyServerWrapper ProxyServer { get; set; }

        public static implicit operator MonitorProxyServerWrapper(MonitorProxyServer monitorProxyServer)
        {
            if (monitorProxyServer == null) return null;
            return new MonitorProxyServerWrapper
                       {
                           Id = monitorProxyServer.Id,
                           Project = monitorProxyServer.Project,
                           ProxyServer = monitorProxyServer.ProxyServer
                       };
        }
    }
}