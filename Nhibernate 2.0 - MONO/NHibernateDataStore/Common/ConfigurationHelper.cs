using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NHibernateDataStore.Exceptions;
using NHibernate;

namespace NHibernateDataStore.Common
{
    public class ConfigurationHelper
    {

        #region Thread-safe, lazy Singleton

        /// <summary>
        /// This is a thread-safe, no so lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static ConfigurationHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor to enforce singleton
        /// </summary>
        private ConfigurationHelper() { }


        private static readonly ConfigurationHelper _instance = new ConfigurationHelper();



        #endregion


        private readonly IDictionary<String, ISessionFactory> sessionFactories = new Dictionary<String, ISessionFactory>();
        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTIONS";
        private const string SESSION_KEY = "CONTEXT_SESSIONS";

        public static ConnectionParameters Create(String name)
        {
            Check.Require(!string.IsNullOrEmpty(name),
               "name may not be null nor empty");

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name];
            if (connectionString != null)
            {
                return ConnectionParameters.Create(connectionString);

            }
            throw new ConfigurationNotFoundException(name);
        }

        public static ISessionFactory GetSectionFactory(String name)
        {
            Check.Require(!string.IsNullOrEmpty(name), "name may not be null nor empty");
            //  Attempt to retrieve a stored SessionFactory from the hashtable.
            var sessionFactory = Instance.sessionFactories.ContainsKey(name) ? Instance.sessionFactories[name] : null;
            if (sessionFactory != null) return sessionFactory;

            var cnp = Create(name);
            sessionFactory = cnp != null ? cnp.GetSessionFactory() : null;

            Instance.sessionFactories.Add(name, sessionFactory);

            return sessionFactory;
        }

        public static ISession GetCurrentSession(String name)
        {
            var session = Instance.ContextSessions.ContainsKey(name) ? Instance.ContextSessions[name] : null;
            if (session != null) return session;

            session = GetSectionFactory(name).OpenSession();

            Check.Ensure(session != null, "session was null");

            Instance.ContextSessions.Add(name, session);
            return session;
        }

        public static void CloseSession(String name)
        {

            var session = Instance.ContextSessions.ContainsKey(name) ? Instance.ContextSessions[name] : null;

            if (session != null && session.IsOpen)
            {
                session.Close();
            }

            Instance.ContextSessions.Remove(name);
        }

        public static ITransaction BeginTransaction(string name)
        {

            var transaction = Instance.ContextTransactions.ContainsKey(name) ? Instance.ContextTransactions[name] : null;

            if (transaction != null) return transaction;

            transaction = GetCurrentSession(name).BeginTransaction();
            Instance.ContextTransactions.Add(name, transaction);

            return transaction;
        }

        public static void RefreshSession(string name)
        {
            var session = GetCurrentSession(name);
            if (session != null) session.Flush();
        }
        public static void CommitTransaction(string name)
        {
            var transaction = Instance.ContextTransactions.ContainsKey(name) ? Instance.ContextTransactions[name] : null;

            try
            {
                
                if (HasOpenTransaction(name))
                {
                    if (transaction != null)
                    {
                        RefreshSession(name);
                        transaction.Commit();
                    }
                    Instance.ContextTransactions.Remove(name);
                }
            }
            catch (HibernateException)
            {
                RollbackTransaction(name);
                throw;
            }
        }

        public static bool HasOpenTransaction(string name)
        {
            var transaction = Instance.ContextTransactions.ContainsKey(name) ? Instance.ContextTransactions[name] : null;

            return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        }

        public static void RollbackTransaction(string name)
        {
            var transaction = Instance.ContextTransactions.ContainsKey(name) ? Instance.ContextTransactions[name] : null;

            try
            {
                if (HasOpenTransaction(name))
                {
                    if (transaction != null) transaction.Rollback();
                }

                Instance.ContextTransactions.Remove(name);
            }
            finally
            {
                CloseSession(name);
            }
        }


        public static void CommitTransactions()
        {
            foreach (var tran in new Dictionary<String, ITransaction>(Instance.ContextTransactions))
            {
                CommitTransaction(tran.Key);
            }
        }

        public static void RollBackTransactions()
        {
            foreach (var tran in new Dictionary<String, ITransaction>(Instance.ContextTransactions))
            {
                RollbackTransaction(tran.Key);
            }
        }

        public static void CloseAllSessions()
        {
            foreach (var tran in new Dictionary<String, ISession>(Instance.ContextSessions))
            {
                CloseSession(tran.Key);
            }
        }


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

        private static bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }
    }
}
