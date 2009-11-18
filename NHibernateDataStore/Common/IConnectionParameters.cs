#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using NHibernate;

#endregion

namespace NHibernateDataStore.Common
{
    public interface IConnectionParameters
    {
        /// <summary>
        /// Name of the configuration.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the NHibernate provider to use. Default is NHibernate.Connection.DriverConnectionProvider
        /// </summary>
        string Connection_Provider { get; set; }

        string DatabaseName { get; set;}

        IList<Assembly> MappingAssemblies { get; }

        IList<Assembly> MappingAssembliesToExport { get; }
        /// <summary>
        /// Gets or sets the NHibernate driver to use. For example NHibernate.Driver.SQLite20Driver
        /// </summary>
        string Connection_DriverClass { get; set; }

        /// <summary>
        /// Gets or sets the NHibernate ConnectionString to use.
        /// </summary>
        string Connection_ConnectionString { get; set; }

        string ProxyFactoryClass { get; set; }

        /// <summary>
        /// Gets or sets the NHibernate dialect to use NHibernate.Dialect.SQLiteDialect.
        /// </summary>
        string Dialect { get; set; }

        ///<summary>
        /// Sets the Table prefix used by the NamingStrategy
        ///</summary>
        String TablePrefix { get; set; }

        /// <summary>
        ///  The fullName of the Intercetor to Load
        /// </summary>
        String Interceptor { get; set; }

        /// <summary>
        /// The name of the CreatedOn Field 
        /// </summary>
        String FieldCreatedOn { get; set; }

        /// <summary>
        /// The name of the CreatedBy Field
        /// </summary>
        String FieldCreatedBy { get; set; }

        /// <summary>
        /// The name of the UpdatedOn Field
        /// </summary>
        String FieldUpdatedOn { get; set; }

        /// <summary>
        /// The name of the UpdatedBy Field
        /// </summary>
        String FieldUpdatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool ShowSql { get; set; }

        /// <summary>
        /// Initialize the Connection Parameters using the Connection String Settings
        /// </summary>
        /// <param name="settings"></param>
        void ReadConnectionParameters(ConnectionStringSettings settings);

        /// <summary>
        /// Create an NHibernate configuration class
        /// </summary>
        /// <returns></returns>
        NHibernate.Cfg.Configuration CreateNHibernateConfiguration(ConfigurationFlags options);

        /// <summary>
        /// Create an NHibernate configuration class
        /// </summary>
        /// <returns></returns>
        NHibernate.Cfg.Configuration CreateNHibernateConfiguration(ConfigurationFlags options, IInterceptor interceptor);

        /// <summary>
        /// Returns the current NHibernate SessionFactory for the current ConnectioParameters Instance
        /// </summary>
        /// <returns></returns>
        ISessionFactory GetSessionFactory();
    }
}