using System;
using NHibernateDataStore.Common;

namespace NHibernateDataStore.Transaction
{
    public class TransactionInterceptor : SimpleFailureInterceptor  
    {
        public String ConnectionName { get; set; }

        protected override void BeforeInvoke(global::Ninject.Core.Interception.IInvocation invocation)
        {
            ConfigurationHelper.BeginTransaction(ConnectionName);
            
        }

        protected override void OnError(Ninject.Core.Interception.IInvocation invocation, Exception exception)
        {
            ConfigurationHelper.RollbackTransaction(ConnectionName);
            
        }

        protected override void AfterInvoke(Ninject.Core.Interception.IInvocation invocation)
        {
            ConfigurationHelper.CommitTransaction(ConnectionName);
            
        }

    }
}