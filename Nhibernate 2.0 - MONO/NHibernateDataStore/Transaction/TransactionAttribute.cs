using System;
using Ninject.Core;
using Ninject.Core.Infrastructure;

namespace NHibernateDataStore.Transaction
{
    public class TransactionAttribute : InterceptAttribute
    {

        public String ConnectionName { get; set; }

        public TransactionAttribute(String ConnectionName)
        {
            this.ConnectionName = ConnectionName;
        }

        public override IInterceptor CreateInterceptor(Ninject.Core.Interception.IRequest request)
        {
            var ti = request.Kernel.Get<TransactionInterceptor>();
            ti.ConnectionName = ConnectionName;

            return ti;
        }
    }
}