using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SEOToolSet.TempFileManagerProvider
{
    [Serializable]
    public class ProviderHasNotReadWritePermissionException : Exception
    {
        public ProviderHasNotReadWritePermissionException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
        public ProviderHasNotReadWritePermissionException(String message)
            : base(message)
        {
        }
        public ProviderHasNotReadWritePermissionException()
            : base()
        {
        }
    }
    [Serializable]
    public class ProviderCanNotStartThePeriodicPurgeTaskException : Exception
    {
        public ProviderCanNotStartThePeriodicPurgeTaskException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ProviderCanNotStartThePeriodicPurgeTaskException(String message)
            : base(message)
        {
        }
        public ProviderCanNotStartThePeriodicPurgeTaskException()
            : base()
        {
        }
    }

    [Serializable]
    public abstract class TempFileManagerException : Exception
    {
        public TempFileManagerException()
            : base()
        {
        }
        public TempFileManagerException(string message)
            : base(message)
        {
        }
        public TempFileManagerException(string message, Exception innerException) :base(message,innerException)
        {
        }
        protected TempFileManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

}
