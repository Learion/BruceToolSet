#region Using Directives

using System.Configuration;

#endregion

namespace NHibernateDataStore.Configuration
{
    public class FactoryMappingElement : ConfigurationElement
    {
        [ConfigurationProperty("ConnectionStringName", IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string) this["ConnectionStringName"]; }
            set { this["ConnectionStringName"] = value; }
        }
    }
}