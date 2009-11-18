#region Using Directives

using System;

#endregion

namespace SEOToolSet.Providers.NHibernate.Exceptions
{
    [Serializable]
    public class EMailNotValidException : ApplicationException
    {
        public EMailNotValidException(string email)
            : base("EMail " + email + " not valid")
        {
        }
    }
}