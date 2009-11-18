using System.Data;


namespace NHibernateDataStore.Common
{
    /// <summary>
    /// The TransactionScope class identify the scope of the transaction used by the Eucalypto classes.
    /// Contains a nhibernate transaction and session. (NHibernateTransaction, NHibernateSession).
    /// </summary>
    public class TransactionScope : ITransactionScope
    {
        // ReSharper disable FieldCanBeMadeReadOnly
        private bool _dispose;
        // ReSharper restore FieldCanBeMadeReadOnly

        /// <summary>
        /// Create a transaction scope using the specified session and transaction.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="transaction"></param>
        /// <param name="dispose">Set to true to dispose the objects and RollBack the transaction if still active when the TransactionScope is disposed.
        /// This can be useful when you share the ITransaction with other clasess</param>
        public TransactionScope(NHibernate.ISession session,
                                NHibernate.ITransaction transaction,
                                bool dispose)
        {
            NHibernateSession = session;
            NHibernateTransaction = transaction;
            _dispose = dispose;
        }

        /// <summary>
        /// Create a new transaction scope with a new session and a new transaction.
        /// The session and transaction are created based on the ConnectionParameters class.
        /// Both objects are disposed at the end.
        /// </summary>
        /// <param name="configuration"></param>
        public TransactionScope(ConnectionParameters configuration)
        {
            NHibernateSession = configuration.OpenSession();
            NHibernateTransaction = NHibernateSession.BeginTransaction();
            _dispose = true;
        }

        public void Rollback()
        {
            NHibernateTransaction.Rollback();
        }

        public void Commit()
        {
            NHibernateTransaction.Commit();
        }

        public NHibernate.ITransaction NHibernateTransaction { get; private set; }

        public NHibernate.ISession NHibernateSession { get; private set; }

        public IDbCommand CreateDbCommand()
        {
            IDbCommand command = NHibernateSession.Connection.CreateCommand();
            NHibernateTransaction.Enlist(command);
            return command;
        }
        public IDbDataParameter CreateDbCommandParameter(IDbCommand command, string parameterName, DbType type, object value)
        {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = parameterName;
            param.DbType = type;
            param.Value = value;
            return param;
        }


        #region IDisposable Members
        public void Dispose()
        {
            if (_dispose)
            {
                //Usually the Transaction implementation automatically rollback the transaction when Dispose is called
                // but seems that MySql doesn't follow this rule so I manually call a RollBack to be sure.
                if (NHibernateTransaction != null)
                {
                    if (NHibernateTransaction.IsActive)
                        NHibernateTransaction.Rollback();

                    NHibernateTransaction.Dispose();
                }
                NHibernateSession.Dispose();
            }

            NHibernateTransaction = null;
            NHibernateSession = null;
        }
        #endregion
    }
}