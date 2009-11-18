#region Using Directives

using System;

#endregion

namespace SEOToolSet.Providers.NHibernate.Exceptions
{
    [Serializable]
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string user)
            : base("User " + user + " not found or invalid password")
        {
        }
    }
}