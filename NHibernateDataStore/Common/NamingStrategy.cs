#region Using Directives

using System;
using NHibernate.Cfg;
using NHibernate.Util;

#endregion

namespace NHibernateDataStore.Common
{
    /// <summary>
    /// Naming strategy for this project's Hibernate mapping.
    /// This is almost the same code as the DefaultNamingStrategy
    /// </summary>
    public class NamingStrategy : INamingStrategy
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        public static readonly INamingStrategy Instance = new NamingStrategy();

        private String _prefix = String.Empty;

        private NamingStrategy()
        {
        }

        /// <summary>
        /// Prefix for the Table Name
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        #region INamingStrategy Members

        /// <summary>
        /// Return the unqualified class name
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public string ClassToTableName(string className)
        {
            return StringHelper.Unqualify(className);
        }

        /// <summary>
        /// Return the unqualified property name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string PropertyToColumnName(string propertyName)
        {
            return StringHelper.Unqualify(propertyName);
        }

        /// <summary>
        /// Return the argument
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string TableName(string tableName)
        {
            /*******************************
             *  Here is the prefix for all the tables -- Flavio.
             * ***************************** */
            return _prefix + tableName;
        }

        /// <summary>
        /// Return the argument
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string ColumnName(string columnName)
        {
            return columnName;
        }

        /// <summary>
        /// Return the unqualified property name
        /// </summary>
        /// <param name="className"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string PropertyToTableName(string className, string propertyName)
        {
            return StringHelper.Unqualify(propertyName);
        }

        /// <summary>
        /// Returns the Logical columnName when available or the Unqualified propertyName if it's not available.
        /// </summary>
        /// <param name="columnName">The name of the Column</param>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>The name of the columnName or the property Name if the first is not empty</returns>
        public string LogicalColumnName(string columnName, string propertyName)
        {
            return StringHelper.IsNotEmpty(columnName) ? columnName : StringHelper.Unqualify(propertyName);
        }

        #endregion
    }
}