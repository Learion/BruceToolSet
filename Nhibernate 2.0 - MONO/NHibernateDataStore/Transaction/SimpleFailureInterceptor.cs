using System;
using Ninject.Core;
using Ninject.Core.Interception;

namespace NHibernateDataStore.Transaction
{
    public abstract class SimpleFailureInterceptor : IInterceptor
    {

        #region IInterceptor Members

        public virtual void Intercept(IInvocation invocation)
        {
            try
            {
                BeforeInvoke(invocation);
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                OnError(invocation, ex);
            }
            finally
            {
                AfterInvoke(invocation);
            }
        }

        #endregion

        /// <summary>
        /// Takes some action before the invocation proceeds.
        /// </summary>
        /// <param name="invocation">The invocation that is being intercepted.</param>
        protected virtual void BeforeInvoke(IInvocation invocation)
        {
        }

        /// <summary>
        /// Takes some action after the invocation proceeds.
        /// </summary>
        /// <param name="invocation">The invocation that is being intercepted.</param>
        protected virtual void AfterInvoke(IInvocation invocation)
        {
        }

        /// <summary>
        /// Takes some action when an error occurs.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="exception">The exception.</param>
        protected virtual void OnError(IInvocation invocation, Exception exception)
        {
            throw exception;
        }
    }
}