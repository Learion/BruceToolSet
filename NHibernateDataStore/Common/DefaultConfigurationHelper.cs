#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NHibernate;
using NHibernateDataStore.Exceptions;

#endregion

namespace NHibernateDataStore.Common
{
    internal class DefaultConfigurationHelper : IConfigurationHelper
    {
        private const string SESSION_KEY = "CONTEXT_SESSIONS";
        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTIONS";

        private readonly Dictionary<string, ConnectionParameters> mList = new Dictionary<string, ConnectionParameters>();

        /// <summary>
        /// Since multiple databases may be in use, there may be one transaction per database 
        /// persisted at any one time.  The easiest way to store them is via a hashtable
        /// with the key being tied to session factory.  If within a web context, this uses
        /// <see cref="HttpContext" /> instead of the WinForms specific <see cref="CallContext" />.  
        /// Discussion concerning this found at http://forum.springframework.net/showthread.php?t=572
        /// </summary>
        private IDictionary<String, ITransaction> ContextTransactions
        {
            get
            {
                if (IsInWebContext())
                {
                    if (HttpContext.Current.Items[TRANSACTION_KEY] == null)
                        HttpContext.Current.Items[TRANSACTION_KEY] = new Dictionary<String, ITransaction>();

                    return (IDictionary<String, ITransaction>)HttpContext.Current.Items[TRANSACTION_KEY];
                }
                if (CallContext.GetData(TRANSACTION_KEY) == null)
                    CallContext.SetData(TRANSACTION_KEY, new Dictionary<String, ITransaction>());

                return (IDictionary<String, ITransaction>)CallContext.GetData(TRANSACTION_KEY);
            }
        }

        /// <summary>
        /// Since multiple databases may be in use, there may be one session per database 
        /// persisted at any one time.  The easiest way to store them is via a hashtable
        /// with the key being tied to session factory.  If within a web context, this uses
        /// <see cref="HttpContext" /> instead of the WinForms specific <see cref="CallContext" />.  
        /// Discussion concerning this found at http://forum.springframework.net/showthread.php?t=572
        /// </summary>
        private IDictionary<String, ISession> ContextSessions
        {
            get
            {
                if (IsInWebContext())
                {
                    if (HttpContext.Current.Items[SESSION_KEY] == null)
                        HttpContext.Current.Items[SESSION_KEY] = new Dictionary<String, ISession>();

                    return (IDictionary<String, ISession>)HttpContext.Current.Items[SESSION_KEY];
                }
                if (CallContext.GetData(SESSION_KEY) == null)
                    CallContext.SetData(SESSION_KEY, new Dictionary<String, ISession>());

                return (IDictionary<String, ISession>)CallContext.GetData(SESSION_KEY);
            }
        }

        #region IConfigurationHelper Members

        public IConnectionParameters Create(String name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[name];
            if (connectionString != null)
            {
                return CreateFromConnectionString(connectionString);
            }

            throw new ConfigurationNotFoundException(name);
        }

        public ISession GetCurrentSession(String name)
        {
            var session = ContextSessions.ContainsKey(name) ? ContextSessions[name] : null;
            if (session != null) return session;

            var cnp = Create(name);

            session = cnp.GetSessionFactory().OpenSession();

            Check.Ensure(session != null, "session was null");

            ContextSessions.Add(name, session);
            return session;
        }

        public void CloseSession(String name)
        {
            var session = ContextSessions.ContainsKey(name) ? ContextSessions[name] : null;

            if (session != null && session.IsOpen)
            {
                session.Close();
            }

            ContextSessions.Remove(name);
        }

        public ITransaction BeginTransaction(string name)
        {
            var transaction = ContextTransactions.ContainsKey(name) ? ContextTransactions[name] : null;

            if (transaction != null) return transaction;

            transaction = GetCurrentSession(name).BeginTransaction();
            ContextTransactions.Add(name, transaction);

            return transaction;
        }

        public void CommitTransaction(string name)
        {
            var transaction = ContextTransactions.ContainsKey(name) ? ContextTransactions[name] : null;

            try
            {
                if (HasOpenTransaction(name))
                {
                    if (transaction != null)
                    {
                        //RefreshSession(name);
                        transaction.Commit();
                    }
                }
                ContextTransactions.Remove(name);
            }
            catch (HibernateException)
            {
                RollbackTransaction(name);
                throw;
            }
        }

        public bool HasOpenTransaction(string name)
        {
            var transaction = ContextTransactions.ContainsKey(name) ? ContextTransactions[name] : null;

            return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        }

        public void RollbackTransaction(string name)
        {
            var transaction = ContextTransactions.ContainsKey(name) ? ContextTransactions[name] : null;

            try
            {
                if (HasOpenTransaction(name))
                {
                    if (transaction != null) transaction.Rollback();
                }

                ContextTransactions.Remove(name);
            }
            finally
            {
                CloseSession(name);
            }
        }


        public void CommitTransactions()
        {
            foreach (var tran in new Dictionary<String, ITransaction>(ContextTransactions))
            {
                CommitTransaction(tran.Key);
            }
        }

        public void CloseAllSessions()
        {
            foreach (var tran in new Dictionary<String, ISession>(ContextSessions))
            {
                CloseSession(tran.Key);
            }
        }

        public void RollbackTransactions()
        {
            foreach (var tran in new Dictionary<String, ITransaction>(ContextTransactions))
            {
                RollbackTransaction(tran.Key);
            }
        }

        public string GetDataBaseFromConnection(string name)
        {
            return Create(name).DatabaseName;
        }

        #endregion

        /// <summary>
        /// Add the specified configuration to the list of saved configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="throwErrorIfExists">True to throw an exception if there is already a connection with the same name, otherwise if a connection already exist this method simply return the previous connection.</param>
        /// <returns>Return the connection just added or the previous connection is there is already a connection with the same name.</returns>
        private IConnectionParameters AddCachedConfiguration(ConnectionParameters configuration,
                                                             bool throwErrorIfExists)
        {
            lock (mList)
            {
                ConnectionParameters savedConfig;
                if (mList.TryGetValue(configuration.Name, out savedConfig))
                {
                    if (throwErrorIfExists)
                        throw new ConfigurationAlreadyExistsException(configuration.Name);
                    return savedConfig;
                }
                mList.Add(configuration.Name, configuration);
                return configuration;
            }
        }

        /// <summary>
        /// This method first look if there is already a saved configuration in the cached list. 
        /// If not exist try to check if there is a connection string in the config file with the same name.
        /// If not exist throw an exception.
        /// </summary>
        /// <param name="connectionString">The name of the connection string to use or a custom connection name. 
        /// To use custom connection use the AddCachedConfiguration method to configure it, otherwise a new configuration is automatically created.</param>
        private IConnectionParameters CreateFromConnectionString(ConnectionStringSettings connectionString)
        {
            if (connectionString != null && !string.IsNullOrEmpty(connectionString.Name))
            {
                lock (mList)
                {
                    ConnectionParameters savedConfig;
                    if (mList.TryGetValue(connectionString.Name, out savedConfig))
                        return savedConfig;
                }

                if (!String.IsNullOrEmpty(connectionString.ConnectionString))
                {
                    var configuration = new ConnectionParameters(connectionString.Name);
                    configuration.ReadConnectionParameters(connectionString);

                    return AddCachedConfiguration(configuration, false);
                }
            }
            throw new ConfigurationNotFoundException("Name");
        }


        private static bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }
    }
}