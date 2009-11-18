using System.Configuration;

namespace NHibernateDataStore.Configuration
{
    /// <summary>
    /// The Eucalypto conficuration section handler.
    /// Contains the list of assemblies to use for the mappings.
    /// </summary>
    public class EucalyptoSection : ConfigurationSection
    {
        ///<summary>
        /// Get the Section from the config file
        ///</summary>
        ///<returns></returns>
        public static EucalyptoSection GetSection()
        {
            var section = (EucalyptoSection)ConfigurationManager.GetSection("eucalypto");

            return section;
        }

        /// <summary>
        /// the AssemblyMappingCollection that contains the Assemblies to be loaded
        /// </summary>
        [ConfigurationProperty("mappings")]
        public AssemblyMappingCollection Mappings
        {
            get { return this["mappings"] as AssemblyMappingCollection; }
        }
        /// <summary>
        /// the ParameterMappingCollection that contain the parameters for the DAL
        /// </summary>
        [ConfigurationProperty("factories")]
        public FactoriesMappingCollection Factories
        {
            get { return this["factories"] as FactoriesMappingCollection; }
        }
    }
}