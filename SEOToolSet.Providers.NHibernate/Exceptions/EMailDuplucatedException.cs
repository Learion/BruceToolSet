#region Using Directives

using System;

#endregion

namespace SEOToolSet.Providers.NHibernate.Exceptions
{
    [Serializable]
    public class EMailDuplucatedException : ApplicationException
    {
        public EMailDuplucatedException(string email)
            : base("EMail " + email + " duplicated")
        {
        }
    }
}