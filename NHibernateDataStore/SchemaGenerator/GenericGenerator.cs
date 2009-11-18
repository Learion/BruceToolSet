#region Using Directives

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateDataStore.Common;

#endregion

namespace NHibernateDataStore.SchemaGenerator
{
    /// <summary>
    /// Class used to automatically generate the schema of the database.
    /// To generate schema for custom entities use the SetupMappingAttribute to specify the category for each entity.
    /// </summary>
    public class GenericGenerator
    {
        private readonly IConnectionParameters _Configuration;

        /// <summary>
        /// A dictionary with the category as a string and a list of the types for the category
        /// </summary>
        private readonly Dictionary<string, List<Type>> mMappings = new Dictionary<string, List<Type>>();

        /// <summary>
        /// Creates an instance of GenericGenerator Class using a custom ConnectionParameters config
        /// </summary>
        /// <param name="config"></param>
        public GenericGenerator(IConnectionParameters config)
        {
            _Configuration = config;

            LoadAssembliesMappings();
        }

        /// <summary>
        /// Create the specified database schema category.
        /// Remember that these methods delete any existing data on the database and recreate the database structure.
        /// </summary>
        /// <param name="schemaCategory"></param>
        public void CreateSchemaTable(string schemaCategory)
        {
            NHibernate.Cfg.Configuration hibConfig =
                _Configuration.CreateNHibernateConfiguration(ConfigurationFlags.Settings);

            Type[] entities = GetEntities(schemaCategory);

            foreach (var ent in entities)
                hibConfig.AddClass(ent);

            var ddlExport = new SchemaExport(hibConfig);
            ddlExport.Create(false, true);
        }

        /// <summary>
        /// Get the status of the schema.
        /// </summary>
        /// <param name="schemaCategory"></param>
        /// <returns></returns>
        public SchemaStatus GetStatus(string schemaCategory)
        {
            try
            {
                Type[] entities = GetEntities(schemaCategory);
                ISession session = _Configuration.GetSessionFactory().OpenSession();

                //TODO Check if there is a way to see if a table exist without catching exception
                foreach (var ent in entities)
                {
                    try
                    {
                        ICriteria criteria = session.CreateCriteria(ent);
                        criteria.SetMaxResults(1);

                        //If the query works is because the table exist
                        criteria.List();
                    }
                    catch (Exception)
                    {
                        //If the query fails is because the table don't exist
                        return SchemaStatus.NotExist;
                    }
                }

                return SchemaStatus.AlreadyExist;
            }
            catch (Exception)
            {
                return SchemaStatus.ConnectionError;
            }
        }


        /// <summary>
        /// Get the list of entities for the specified category
        /// </summary>
        /// <param name="schemaCategory"></param>
        /// <returns></returns>
        public Type[] GetEntities(string schemaCategory)
        {
            List<Type> list;
            if (mMappings.TryGetValue(schemaCategory, out list) == false)
                throw new ArgumentException(schemaCategory + " value not supported", "schemaCategory");

            return list.ToArray();
        }

        /// <summary>
        /// Get the list of schema categories.
        /// </summary>
        /// <returns></returns>
        public string[] GetSchemaCategories()
        {
            var keys = new string[mMappings.Keys.Count];
            mMappings.Keys.CopyTo(keys, 0);

            return keys;
        }

        private void LoadAssembliesMappings()
        {
            foreach (var assembly in _Configuration.MappingAssemblies)
            {
                object[] attributes = assembly.GetCustomAttributes(typeof (SetupMappingAttribute), false);

                foreach (SetupMappingAttribute attribute in attributes)
                {
                    if (mMappings.ContainsKey(attribute.Category) == false)
                        mMappings.Add(attribute.Category, new List<Type>());

                    foreach (var type in attribute.MappingTypes)
                        mMappings[attribute.Category].Add(type);
                }
            }
        }
    }

    /// <summary>
    /// Especifies the Status of the Schema
    /// </summary>
    public enum SchemaStatus
    {
        ///<summary>
        /// All the tables in the Schema already Exist in the DB.
        ///</summary>
        AlreadyExist,
        ///<summary>
        /// Some tables in the schema doesn't exist in the DB.
        ///</summary>
        PartialExist,

        /// <summary>
        /// The Entire Schema Doesn't exist.
        /// </summary>
        NotExist,
        /// <summary>
        /// There were a connection Error when try to access the DB 
        /// </summary>
        ConnectionError,
        /// <summary>
        /// The Driver Provided was unknow
        /// </summary>
        UnknownDriver
    }
}