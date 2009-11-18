#region Using Directives

using System;
using System.Configuration;

#endregion

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

        [ConfigurationProperty("excludeFromExportSchema")]
        public bool ExcludeFromExportSchema
        {
            get { return Convert.ToBoolean(this["excludeFromExportSchema"]); }
            set { this["excludeFromExportSchema"] = value; }
        }

    }
}