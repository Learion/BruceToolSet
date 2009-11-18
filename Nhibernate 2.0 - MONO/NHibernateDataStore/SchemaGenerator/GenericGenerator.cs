using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NHibernateDataStore.Common;

namespace NHibernateDataStore.SchemaGenerator
{
    /// <summary>
    /// Class used to automatically generate the schema of the database.
    /// To generate schema for custom entities use the SetupMappingAttribute to specify the category for each entity.
    /// </summary>
    public class GenericGenerator
    {
        private readonly ConnectionParameters _Configuration;
        /// <summary>
        /// Creates an instance of GenericGenerator Class using a custom ConnectionParameters config
        /// </summary>
        /// <param name="config"></param>
        public GenericGenerator(ConnectionParameters config)
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
            NHibernate.Cfg.Configuration hibConfig = _Configuration.CreateNHibernateConfiguration(ConfigurationFlags.Settings);

            Type[] entities = GetEntities(schemaCategory);

            foreach (Type ent in entities)
                hibConfig.AddClass(ent);

            var ddlExport = new NHibernate.Tool.hbm2ddl.SchemaExport(hibConfig);
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

                //TODO Check if there is a way to see if a table exist without catching exception
                using (TransactionScope transaction = new TransactionScope(_Configuration))
                {
                    foreach (Type ent in entities)
                    {
                        try
                        {
                            NHibernate.ICriteria criteria = transaction.NHibernateSession.CreateCriteria(ent);
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
            }
            catch (Exception)
            {
                return SchemaStatus.ConnectionError;
            }
        }


        //public SchemaStatus GetStatus(SchemaSection section)
        //{
        //    try
        //    {
        //        string query;

        //        if (string.Equals(_Configuration.Connection_DriverClass, ConnectionParameters.DRIVER_SQLITE, StringComparison.InvariantCultureIgnoreCase))
        //            query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{0}'";
        //        else if (string.Equals(_Configuration.Connection_DriverClass, ConnectionParameters.DRIVER_SQLSERVER, StringComparison.InvariantCultureIgnoreCase))
        //            query = "SELECT COUNT(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')";
        //        else
        //            return SchemaStatus.UnknownDriver;

        //        Type[] entities = GetEntities(section);

        //        int founded = 0;
        //        foreach (Type ent in entities)
        //        {
        //            using (TransactionScope transaction = new TransactionScope(_Configuration))
        //            {
        //                IDbCommand command = transaction.CreateDbCommand();
        //                command.CommandText = string.Format(query, ent.Name);

        //                //Note: I don't use command parameters for a better support for special database (where the parameters are not identified by @)
        //                //IDbDataParameter param = transaction.CreateDbCommandParameter(command, "@tableName", DbType.String, ent.Name);
        //                //command.Parameters.Add(param);

        //                int count = Convert.ToInt32( command.ExecuteScalar() );
        //                if (count == 1)
        //                    founded++;
        //            }
        //        }

        //        if (entities.Length == founded)
        //            return SchemaStatus.AlreadyExist;
        //        else if (founded == 0)
        //            return SchemaStatus.NotExist;
        //        else if (entities.Length > founded)
        //            return SchemaStatus.PartialExist;
        //        else
        //            throw new EucalyptoException("Returned value not valid");
        //    }
        //    catch (Exception)
        //    {
        //        return SchemaStatus.ConnectionError;
        //    }
        //}

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
            string[] keys = new string[mMappings.Keys.Count];
            mMappings.Keys.CopyTo(keys, 0);

            return keys;
        }

        /// <summary>
        /// A dictionary with the category as a string and a list of the types for the category
        /// </summary>
        private Dictionary<string, List<Type>> mMappings = new Dictionary<string, List<Type>>();
        private void LoadAssembliesMappings()
        {
            foreach (System.Reflection.Assembly assembly in _Configuration.MappingAssemblies)
            {
                object[] attributes = assembly.GetCustomAttributes(typeof(SetupMappingAttribute), false);

                foreach (SetupMappingAttribute attribute in attributes)
                {
                    if (mMappings.ContainsKey(attribute.Category) == false)
                        mMappings.Add(attribute.Category, new List<Type>());

                    foreach (Type type in attribute.MappingTypes)
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