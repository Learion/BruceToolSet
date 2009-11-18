#region Using Directives

using System;
using System.Web;
using NHibernateDataStore.Common;

#endregion

namespace NHibernateDataStore.CustomModule
{
    public class NHibernateSessionModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.EndRequest += CommitAndCloseSession;
        }

        public void Dispose()
        {
        }

        #endregion

        private static void CommitAndCloseSession(object sender, EventArgs e)
        {
            try
            {
                //if all goes fine, the transaction es commited at the end if 
                NHibernateConfigurationManager.ConfigurationHelper.RollbackTransactions();
            }
            finally
            {
                NHibernateConfigurationManager.ConfigurationHelper.CloseAllSessions();
            }
        }
    }
}