#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    [Serializable]
    public class ProviderCannotStartThePeriodicPurgeTaskException : Exception
    {
        public ProviderCannotStartThePeriodicPurgeTaskException()
        {
        }

        public ProviderCannotStartThePeriodicPurgeTaskException(String message)
            : base(message)
        {
        }

        public ProviderCannotStartThePeriodicPurgeTaskException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProviderCannotStartThePeriodicPurgeTaskException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}