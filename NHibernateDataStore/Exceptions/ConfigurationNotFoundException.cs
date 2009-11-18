#region Using Directives

using System;

#endregion

namespace NHibernateDataStore.Exceptions
{
    [Serializable]
    public class ConfigurationNotFoundException : BaseException
    {
        public ConfigurationNotFoundException(string name)
            : base("Configuration or connection string '" + name + "' not found")
        {
        }
    }
}