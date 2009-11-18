#region Using Directives

using System;
using System.Data;
using NHibernate;

#endregion

namespace NHibernateDataStore.Common
{
    public interface IConfigurationHelper
    {
        IConnectionParameters Create(String name);
        ISession GetCurrentSession(String name);
        void CloseSession(String name);
        ITransaction BeginTransaction(string name);

        void CommitTransaction(string name);
        bool HasOpenTransaction(string name);
        void RollbackTransaction(string name);
        void CommitTransactions();
        void CloseAllSessions();

        void RollbackTransactions();

        string GetDataBaseFromConnection(string name);
    }
}