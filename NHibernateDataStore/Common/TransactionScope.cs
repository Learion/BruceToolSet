#region Using Directives

using System;
using System.Runtime.Remoting.Messaging;
using System.Web;

#endregion

namespace NHibernateDataStore.Common
{
    /// <summary>
    /// The TransactionScope class identify the scope of the transaction used by the Eucalypto classes.
    /// Contains a nhibernate transaction and session. (NHibernateTransaction, NHibernateSession).
    /// </summary>
    public class TransactionScope : ITransactionScope
    {
        private const string PREFIX_FOR_COUNT = "ActiveTransaction_";
        private readonly String _connectionStringName;

        private bool _validTransactionScope;

        public TransactionScope(string connectionStringName)
        {
            //Thanks to my good friend Paolo, I will never trust in IsNullOrEmpty again!
            Check.Require(!String.IsNullOrEmpty(connectionStringName), "Please provide a valid connectionStringName");

            _connectionStringName = connectionStringName;

            if (ActiveTransactions == 0)
            {
                NHibernateConfigurationManager.ConfigurationHelper.BeginTransaction(_connectionStringName);
            }
            ActiveTransactions++;
        }

        private string key
        {
            get { return string.Format("{0}_{1}", PREFIX_FOR_COUNT, _connectionStringName); }
        }

        private Int32 ActiveTransactions
        {
            get
            {
                var active = (IsInWebContext()) ? HttpContext.Current.Items[key] : CallContext.GetData(key);
                if (active != null) return (int)active;
                return 0;
            }
            set
            {
                if (IsInWebContext()) HttpContext.Current.Items[key] = value;
                else
                {
                    CallContext.SetData(key, value);
                }
            }
        }

        #region ITransactionScope Members

        public void Rollback()
        {
            _validTransactionScope = false;
        }

        public void Commit()
        {
            _validTransactionScope = true;
        }

        public void Dispose()
        {
            ActiveTransactions--;

            if (_validTransactionScope)
            {
                if (ActiveTransactions == 0)
                {
                    if (IsInWebContext())
                        HttpContext.Current.Items[key] = null;
                    else
                        CallContext.SetData(key, null);
                    NHibernateConfigurationManager.ConfigurationHelper.CommitTransaction(_connectionStringName);
                }
            }
            else
            {
                if (IsInWebContext())
                    HttpContext.Current.Items[key] = null;
                else
                    CallContext.SetData(key, null);
                NHibernateConfigurationManager.ConfigurationHelper.RollbackTransaction(_connectionStringName);
            }
        }

        public bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        #endregion
    }
}