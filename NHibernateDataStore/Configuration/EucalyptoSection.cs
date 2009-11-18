#region Using Directives

using System.Configuration;

#endregion

namespace NHibernateDataStore.Configuration
{
    /// <summary>
    /// The Eucalypto conficuration section handler.
    /// Contains the list of assemblies to use for the mappings.
    /// </summary>
    public class EucalyptoSection : ConfigurationSection
    {
        /// <summary>
        /// the AssemblyMappingCollection that contains the Assemblies to be loaded
        /// </summary>
        [ConfigurationProperty("mappings")]
        public AssemblyMappingCollection Mappings
        {
            get { return base["mappings"] as AssemblyMappingCollection; }
        }

        [ConfigurationProperty("NHibernateHelperType")]
        public string NHibernateHelperType
        {
            get { return (string) base["NHibernateHelperType"]; }
            set { base["NHibernateHelperType"] = value; }
        }

        ///<summary>
        /// Get the Section from the config file
        ///</summary>
        ///<returns></returns>
        public static EucalyptoSection GetSection()
        {
            var section = (EucalyptoSection) ConfigurationManager.GetSection("eucalypto");

            return section;
        }
    }
}