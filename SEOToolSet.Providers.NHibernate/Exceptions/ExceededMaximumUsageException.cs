using System;

namespace SEOToolSet.Providers.NHibernate.Exceptions
{
    ///<summary>
    ///Exception thrown when there is a limit for usage and it is exceeded
    ///</summary>
    [Serializable]
    public class ExceededMaximumUsageException : ApplicationException
    {
        ///<summary>
        ///Default constructor
        ///</summary>
        public ExceededMaximumUsageException()
            : base("The maximum usage value for this promo code was exceeded.")
        {
        }
    }
}
