#region Using Directives

using System;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSCreditCardType : EntityDataStoreBase<CreditCardType, String>
    {
        public DSCreditCardType(ISession session)
            : base(session)
        {
        }


        public static DSCreditCardType Create(String connName)
        {
            return new DSCreditCardType(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns 

        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
        }

        #endregion
    }
}