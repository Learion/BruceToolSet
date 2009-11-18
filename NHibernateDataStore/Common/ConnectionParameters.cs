#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using NHibernate;
using NHibernateDataStore.Configuration;
using NHibernateDataStore.Exceptions;
using NHibernateDataStore.Interceptor;

#endregion

namespace NHibernateDataStore.Common
{
    /// <summary>
    /// ConnectionParameters class is used to mantain the configuration required for NHibernate.
    /// Con read the configuration from a connection string class using the static method ConnectionParameters.Create().
    /// The ConnectionParameters is automatically added to the cache for future invocations.
    /// Use the static ConnectionParameters.AddCachedConfiguration to manually add a configuration to the cache.
    /// Use the OpenSession method to directly open an NHibernate session from the specified configuration.
    /// The class automatically load the the assemblies to use for the mappings from the eucalypto configuration section.
    /// The static methods are safe for multithread operations and also the OpenSession instance method.
    /// </summary>
    [Serializable]
    public class ConnectionParameters : IConnectionParameters
    {
        public const string DEFAULT_APP = "DefaultApp";

        #region constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionParameters(string name)
        {
            Name = name;
            LoadAssembliesFromConfiguration();
        }

        #endregion

        #region Properties

        private readonly IList<Assembly> _mappingAssemblies = new List<Assembly>();
        private string _connectionProvider = "NHibernate.Connection.DriverConnectionProvider";


        /// <summary>
        /// Name of the configuration.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the NHibernate provider to use. Default is NHibernate.Connection.DriverConnectionProvider
        /// </summary>
        public virtual string Connection_Provider
        {
            get { return _connectionProvider; }
            set { _connectionProvider = value; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        /// <summary>
        /// Gets or sets the NHibernate driver to use. For example NHibernate.Driver.SQLite20Driver
        /// </summary>
        public virtual string Connection_DriverClass { get; set; }

        /// <summary>
        /// Gets or sets the NHibernate ConnectionString to use.
        /// </summary>
        public virtual string Connection_ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the NHibernate dialect to use NHibernate.Dialect.SQLiteDialect.
        /// </summary>
        public virtual string Dialect { get; set; }

        ///<summary>
        /// Sets the Table prefix used by the NamingStrategy
        ///</summary>
        public virtual String TablePrefix { get; set; }

        /// <summary>
        ///  The fullName of the Intercetor to Load
        /// </summary>
        public virtual string Interceptor { get; set; }

        /// <summary>
        /// The name of the CreatedOn Field 
        /// </summary>
        public virtual string FieldCreatedOn { get; set; }

        /// <summary>
        /// The name of the CreatedBy Field
        /// </summary>
        public virtual string FieldCreatedBy { get; set; }

        /// <summary>
        /// The name of the UpdatedOn Field
        /// </summary>
        public virtual string FieldUpdatedOn { get; set; }

        /// <summary>
        /// The name of the UpdatedBy Field
        /// </summary>
        public virtual string FieldUpdatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool ShowSql { get; set; }

        /// <summary>
        /// List of the assemblies to use in the mapping. 
        /// Already contains the Eucalypto library to load the default mapping files.
        /// You can add your custom assemblies.
        /// </summary>
        public IList<Assembly> MappingAssemblies
        {
            get { return _mappingAssemblies; }
        }

        private readonly IList<Assembly> _mappingAssembliesToExport = new List<Assembly>();

        public IList<Assembly> MappingAssembliesToExport
        {
            get { return _mappingAssembliesToExport; }
        }



        #endregion

        #region Instance methods

        /// <summary>
        /// Used to syncronize the factory creation
        /// </summary>
        private readonly object mSyncObj = new object();

        [NonSerialized]
        private ISessionFactory mFactory;

        private string _databaseName;


        /// <summary>
        /// Initialize the Connection Parameters using the Connection String Settings
        /// </summary>
        /// <param name="settings"></param>
        public void ReadConnectionParameters(ConnectionStringSettings settings)
        {
            var connectionString = new DbConnectionStringBuilder { ConnectionString = settings.ConnectionString };

            if (connectionString.ContainsKey("Database"))
            {
                DatabaseName = (string)connectionString["Database"];
            }

            if (connectionString.ContainsKey("DriverClass"))
            {
                Connection_DriverClass = (string)connectionString["DriverClass"];
                connectionString.Remove("DriverClass");
            }
            else
                throw new ConnectionElementNotFoundException("DriverClass");

            if (connectionString.ContainsKey("Dialect"))
            {
                Dialect = (string)connectionString["Dialect"];
                connectionString.Remove("Dialect");
            }
            else
                throw new ConnectionElementNotFoundException("Dialect");

            if (connectionString.ContainsKey("proxyfactory.factory_class"))
            {
                ProxyFactoryClass = (string)connectionString["proxyfactory.factory_class"];
                connectionString.Remove("proxyfactory.factory_class");
            }

            if (connectionString.ContainsKey("TablePrefix"))
            {
                TablePrefix = (string)connectionString["TablePrefix"];
                connectionString.Remove("TablePrefix");
            }

            if (connectionString.ContainsKey("show_sql"))
            {
                ShowSql = (string)connectionString["NHibernateInterceptor"] == "true";
                connectionString.Remove("show_sql");
            }
            if (connectionString.ContainsKey("NHibernateInterceptor"))
            {
                Interceptor = (string)connectionString["NHibernateInterceptor"];
                connectionString.Remove("NHibernateInterceptor");
            }

            if (connectionString.ContainsKey("CreatedOn"))
            {
                FieldCreatedOn = (string)connectionString["CreatedOn"];
                connectionString.Remove("CreatedOn");
            }

            if (connectionString.ContainsKey("UpdatedOn"))
            {
                FieldUpdatedOn = (string)connectionString["UpdatedOn"];
                connectionString.Remove("UpdatedOn");
            }

            if (connectionString.ContainsKey("CreatedBy"))
            {
                FieldCreatedBy = (string)connectionString["CreatedBy"];
                connectionString.Remove("CreatedBy");
            }

            if (connectionString.ContainsKey("UpdatedBy"))
            {
                FieldUpdatedBy = (string)connectionString["UpdatedBy"];
                connectionString.Remove("UpdatedBy");
            }

            Connection_ConnectionString = connectionString.ConnectionString;
        }

        public string ProxyFactoryClass
        {
            get;
            set;
        }

        /// <summary>
        /// Create an NHibernate configuration class
        /// </summary>
        /// <returns></returns>
        public NHibernate.Cfg.Configuration CreateNHibernateConfiguration(ConfigurationFlags options)
        {
            return CreateNHibernateConfiguration(options, null);
        }

        /// <summary>
        /// Create an NHibernate configuration class
        /// </summary>
        /// <returns></returns>
        public NHibernate.Cfg.Configuration CreateNHibernateConfiguration(ConfigurationFlags options,
                                                                          IInterceptor interceptor)
        {
            var configuration = new NHibernate.Cfg.Configuration();


            if ((options & ConfigurationFlags.Settings) == ConfigurationFlags.Settings)
            {
                configuration.SetProperty("connection.connection_string", Connection_ConnectionString);
                configuration.SetProperty("connection.provider", Connection_Provider);
                configuration.SetProperty("connection.driver_class", Connection_DriverClass);
                configuration.SetProperty("dialect", Dialect);
                configuration.SetProperty("show_sql", ShowSql.ToString(CultureInfo.InvariantCulture).ToLower());
                configuration.SetProperty("proxyfactory.factory_class", ProxyFactoryClass);

                //Include The TablePrefix Property For the Table Mappings ... RRRM : 23/09/2007
                if (!String.IsNullOrEmpty(TablePrefix))
                {
                    var ns = (NamingStrategy)NamingStrategy.Instance;
                    ns.Prefix = TablePrefix;
                    configuration.SetNamingStrategy(NamingStrategy.Instance);
                }
            }

            if ((options & ConfigurationFlags.Mappings) == ConfigurationFlags.Mappings)
            {
                foreach (var assembly in MappingAssemblies)
                    configuration.AddAssembly(assembly);
            }

            if ((options & ConfigurationFlags.Interceptor) == ConfigurationFlags.Interceptor)
            {
                if (interceptor != null) configuration.SetInterceptor(interceptor);
            }

            if ((options & ConfigurationFlags.MappingsToExport) == ConfigurationFlags.MappingsToExport)
            {
                foreach (var assembly in MappingAssembliesToExport)
                    configuration.AddAssembly(assembly);
            }

            return configuration;
        }


        /// <summary>
        /// Returns the current NHibernate SessionFactory for the current ConnectioParameters Instance
        /// </summary>
        /// <returns></returns>
        public ISessionFactory GetSessionFactory()
        {
            CheckFactory();

            return mFactory;
        }

        /// <summary>
        /// Load the assemblies from the eucalypto configuration section.
        /// </summary>
        private void LoadAssembliesFromConfiguration()
        {
            //Get the configuration section
            var section = EucalyptoSection.GetSection();

            if (section == null)
                return;

            foreach (AssemblyMappingElement element in section.Mappings)
            {
                var assembly = Assembly.Load(element.Assembly);
                MappingAssemblies.Add(assembly);
                if (!element.ExcludeFromExportSchema)
                {
                    MappingAssembliesToExport.Add(assembly);
                }

            }
        }

        /// <summary>
        /// This method use a lock to syncronize the factory creation.
        /// </summary>
        private void CheckFactory()
        {
            if (mFactory != null) return;
            lock (mSyncObj)
            {
                if (mFactory != null) return;

                InterceptorBase interceptor = null;
                if (!String.IsNullOrEmpty(Interceptor))
                {
                    var interceptorType = Type.GetType(Interceptor);

                    if (interceptorType == null)
                        throw new ConfigurationErrorsException(String.Format("Could not find type: {0}", Interceptor));

                    interceptor = Activator.CreateInstance(interceptorType) as InterceptorBase;
                    if (interceptor != null)
                    {
                        interceptor.FieldCreatedBy = FieldCreatedBy;
                        interceptor.FieldCreatedOn = FieldCreatedOn;
                        interceptor.FieldUpdatedBy = FieldUpdatedBy;
                        interceptor.FieldUpdatedOn = FieldUpdatedOn;
                    }
                }

                var configuration = CreateNHibernateConfiguration(ConfigurationFlags.Default, interceptor);

                mFactory = configuration.BuildSessionFactory();
            }
        }

        #endregion
    }
}