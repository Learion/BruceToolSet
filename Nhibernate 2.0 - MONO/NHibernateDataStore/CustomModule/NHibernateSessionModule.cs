using System;
using System.Web;
using NHibernateDataStore.Common;

namespace NHibernateDataStore.CustomModule
{
    public class NHibernateSessionModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginTransaction;
            context.EndRequest += RollBackOpenTransactionsAndCloseSessions;
            
        }

        private static void RollBackOpenTransactionsAndCloseSessions(object sender, EventArgs e)
        {
            try
            {
                //if any transaction is still active here is rolledback
                ConfigurationHelper.RollBackTransactions();
            }
            finally
            {
                ConfigurationHelper.CloseAllSessions();
            }
        }

        private static void BeginTransaction(object sender, EventArgs e)
        {
            //Do Nothing the SessionFactories are Lazy Loaded
        }

        public void Dispose()
        {
            
        }
    }
}
