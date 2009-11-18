#region Using Directives

using System;

#endregion

namespace SEOToolSet.Providers
{
    public class DuplicatedEntityException : Exception
    {
        public DuplicatedEntityException()
        {
        }

        public DuplicatedEntityException(string duplicatedEntityName)
            : base(duplicatedEntityName)
        {
        }

        public DuplicatedEntityException(string duplicatedEntityName, Exception innerException)
            : base(duplicatedEntityName, innerException)
        {
        }
    }
}