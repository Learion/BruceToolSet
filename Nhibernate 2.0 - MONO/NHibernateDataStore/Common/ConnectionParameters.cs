using System;
using System.Collections.Generic;
using System.Configuration;
using NHibernateDataStore.Exceptions;
using NHibernateDataStore.Interceptor;


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
    public class ConnectionParameters
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

        /// <summary>
        /// Name of the configuration.
        /// </summary>
        public string Name { get; private set; }

        private string _connectionProvider = "NHibernate.Connection.DriverConnectionProvider";
        /// <summary>
        /// Gets or sets the NHibernate provider to use. Default is NHibernate.Connection.DriverConnectionProvider
        /// </summary>
        public virtual string Connection_Provider
        {
            get { return _connectionProvider; }
            set { _connectionProvider = value; }
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
        /// 
        /// </summary>
        public virtual bool ShowSql { get; set; }

        private readonly List<System.Reflection.Assembly> _mappingAssemblies = new List<System.Reflection.Assembly>();
        /// <summary>
        /// List of the assemblies to use in the mapping. 
        /// Already contains the Eucalypto library to load the default mapping files.
        /// You can add your custom assemblies.
        /// </summary>
        public List<System.Reflection.Assembly> MappingAssemblies
        {
            get { return _mappingAssemblies; }
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Load the assemblies from the eucalypto configuration section.
        /// </summary>
        private void LoadAssembliesFromConfiguration()
        {
            //Get the configuration section
            var section = Configuration.EucalyptoSection.GetSection();

            if (section == null)
                return;

            foreach (Configuration.AssemblyMappingElement element in section.Mappings)
            {
                MappingAssemblies.Add(System.Reflection.Assembly.Load(element.Assembly));
            }
        }

        /// <summary>
        /// Initialize the Connection Parameters using the Connection String Settings
        /// </summary>
        /// <param name="settings"></param>
        public void ReadConnectionParameters(ConnectionStringSettings settings)
        {
            var connectionString = new System.Data.Common.DbConnectionStringBuilder { ConnectionString = settings.ConnectionString };


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

            if (connectionString.ContainsKey("TablePrefix"))
            {
                TablePrefix = (string)connectionString["TablePrefix"];
                connectionString.Remove("TablePrefix");
            }

            Connection_ConnectionString = connectionString.ConnectionString;
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
        public NHibernate.Cfg.Configuration CreateNHibernateConfiguration(ConfigurationFlags options, NHibernate.IInterceptor interceptor)
        {
            var configuration = new NHibernate.Cfg.Configuration();

            if ((options & ConfigurationFlags.Settings) == ConfigurationFlags.Settings)
            {
                configuration.SetProperty("connection.connection_string", Connection_ConnectionString);
                configuration.SetProperty("connection.provider", Connection_Provider);
                configuration.SetProperty("connection.driver_class", Connection_DriverClass);
                configuration.SetProperty("show_sql", ShowSql.ToString(System.Globalization.CultureInfo.InvariantCulture).ToLower());
                configuration.SetProperty("dialect", Dialect);

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
                foreach (System.Reflection.Assembly assembly in MappingAssemblies)
                    configuration.AddAssembly(assembly);
            }

            if ((options & ConfigurationFlags.Interceptor) == ConfigurationFlags.Interceptor)
            {
                if (interceptor != null) configuration.SetInterceptor(interceptor);
            }

            return configuration;
        }

        [NonSerialized]
        private NHibernate.ISessionFactory mFactory;

        /// <summary>
        /// Used to syncronize the factory creation
        /// </summary>
        private readonly object mSyncObj = new object();

        /// <summary>
        /// This method use a lock to syncronize the factory creation.
        /// </summary>
        private void CheckFactory()
        {
            if (mFactory != null) return;
            lock (mSyncObj)
            {
                if (mFactory != null) return;
                var configuration = CreateNHibernateConfiguration(ConfigurationFlags.Default);

                mFactory = configuration.BuildSessionFactory();
            }
        }

        /// <summary>
        /// Returns an active NHibernate session.
        /// Usually you don't need to call this method directly, 
        /// I suggest to use the TransactionScope class.
        /// </summary>
        /// <returns></returns>
        public NHibernate.ISession OpenSession()
        {
            return GetSessionFactory().OpenSession();
        }

        /// <summary>
        /// Returns the current NHibernate SessionFactory for the current ConnectioParameters Instance
        /// </summary>
        /// <returns></returns>
        public NHibernate.ISessionFactory GetSessionFactory()
        {
            CheckFactory();

            return mFactory;
        }
        #endregion
        
        #region Static methods
        private static readonly Dictionary<string, ConnectionParameters> mList = new Dictionary<string, ConnectionParameters>();

        /// <summary>
        /// Add the specified configuration to the list of saved configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="throwErrorIfExists">True to throw an exception if there is already a connection with the same name, otherwise if a connection already exist this method simply return the previous connection.</param>
        /// <returns>Return the connection just added or the previous connection is there is already a connection with the same name.</returns>
        public static ConnectionParameters AddCachedConfiguration(ConnectionParameters configuration,
                                                                  bool throwErrorIfExists)
        {
            lock (mList)
            {
                ConnectionParameters savedConfig;
                if (mList.TryGetValue(configuration.Name, out savedConfig))
                {
                    if (throwErrorIfExists)
                        throw new ConfigurationAlreadyExistsException(configuration.Name);
                    return savedConfig;
                }
                mList.Add(configuration.Name, configuration);
                return configuration;
            }
        }

        /// <summary>
        /// Check if a there is already a configuration with the specified name. Return null if not found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConnectionParameters Find(string name)
        {
            lock (mList)
            {
                ConnectionParameters savedConfig;
                return mList.TryGetValue(name, out savedConfig) ? savedConfig : null;
            }
        }

        /// <summary>
        /// This method first look if there is already a saved configuration in the cached list. 
        /// If not exist try to check if there is a connection string in the config file with the same name.
        /// If not exist throw an exception.
        /// </summary>
        /// <param name="connectionString">The name of the connection string to use or a custom connection name. 
        /// To use custom connection use the AddCachedConfiguration method to configure it, otherwise a new configuration is automatically created.</param>
        public static ConnectionParameters Create(ConnectionStringSettings connectionString)
        {
            if (connectionString != null && !string.IsNullOrEmpty(connectionString.Name))
            {
                lock (mList)
                {
                    ConnectionParameters savedConfig;
                    if (mList.TryGetValue(connectionString.Name, out savedConfig))
                        return savedConfig;
                }

                if (!String.IsNullOrEmpty(connectionString.ConnectionString))
                {
                    var configuration = new ConnectionParameters(connectionString.Name);
                    configuration.ReadConnectionParameters(connectionString);

                    return AddCachedConfiguration(configuration, false);
                }
            }
            throw new ConfigurationNotFoundException("Name");

        }
        #endregion
    }
}