using System;

namespace SEOToolSet.Providers.NHibernate.Exceptions
{
    [Serializable]
    public class ProfileValueNotSupportedException : ApplicationException
    {
        public ProfileValueNotSupportedException(string propertyName)
            : base("Profile property " + propertyName + " cannot be deserialized, value type not supported.")
        {

        }
    }
}