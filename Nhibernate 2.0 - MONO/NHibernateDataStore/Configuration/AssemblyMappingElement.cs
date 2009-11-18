using System.Configuration;

namespace NHibernateDataStore.Configuration
{
    ///<summary>
    /// Assembly Mapping Element
    ///</summary>
    public class AssemblyMappingElement : ConfigurationElement
    {
        ///<summary>
        /// the Assembly Property 
        ///</summary>
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return (string)this["assembly"]; }
            set { this["assembly"] = value; }
        }
    }
}