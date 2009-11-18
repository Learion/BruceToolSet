using System;
namespace NHibernateDataStore.Exceptions
{
    [Serializable]
    public class ConfigurationAlreadyExistsException : BaseException
    {
        public ConfigurationAlreadyExistsException(string name)
            : base("Configuration '" + name + "' already exists")
        {

        }
    }
}