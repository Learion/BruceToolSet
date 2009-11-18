#region Using Directives

using System;
using System.Configuration;
using NHibernateDataStore.Configuration;

#endregion

namespace NHibernateDataStore.Common
{
    public static class NHibernateConfigurationManager
    {
        #region Thread-safe, lazy Singleton

        private static readonly IConfigurationHelper _instanceHelper = CreateHelper();

        public static IConfigurationHelper ConfigurationHelper
        {
            get { return _instanceHelper; }
        }

        private static IConfigurationHelper CreateHelper()
        {
            var settingsType = Type.GetType(EucalyptoSection.GetSection().NHibernateHelperType);

            if (settingsType == null)
                throw new ConfigurationErrorsException(String.Format("Could not find type: {0}",
                                                                     EucalyptoSection.GetSection().NHibernateHelperType));

            return Activator.CreateInstance(settingsType) as IConfigurationHelper;
        }

        #endregion
    }
}