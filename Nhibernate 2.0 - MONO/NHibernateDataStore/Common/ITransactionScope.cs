using System;
using System.Data;

namespace NHibernateDataStore.Common
{
    /// <summary>
    /// Represent a class that implements a wrapper to the Isession Transaction
    /// </summary>
    public interface ITransactionScope : IDisposable
    {
        /// <summary>
        /// Cancel the current transaction and undo any change that was not commited.
        /// </summary>
        void Rollback();
        
        /// <summary>
        /// Makes the current changes permanently in the persistence store.
        /// </summary>
        void Commit();
        
        /// <summary>
        ///  return the NHibernate ITransaction in the ITransactionScope
        /// </summary>
        NHibernate.ITransaction NHibernateTransaction { get; }
        
        /// <summary>
        /// return the NHibernate ISession
        /// </summary>
        NHibernate.ISession NHibernateSession { get; }
        /// <summary>
        /// Creates an IDbCommand
        /// </summary>
        /// <returns>an Instance of an IDbCommand</returns>
        IDbCommand CreateDbCommand();
        /// <summary>
        /// Creates a IDbDataParameter
        /// </summary>
        /// <param name="command">The IDbCommand</param>
        /// <param name="parameterName">the parameter name</param>
        /// <param name="type"> the DbType</param>
        /// <param name="value">the value</param>
        /// <returns>an instance of an IDbDataParameter</returns>
        IDbDataParameter CreateDbCommandParameter(IDbCommand command, string parameterName, DbType type, object value);
    }
}