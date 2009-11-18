#region Using Directives

using System;

#endregion

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
    }
}