#region Using Directives

using System;

#endregion

namespace SEOToolSet.Providers.NHibernate.Exceptions
{
    [Serializable]
    public class EMailRequiredException : ApplicationException
    {
        public EMailRequiredException(string user)
            : base("User " + user + " don't have a valid email specified.")
        {
        }
    }
}