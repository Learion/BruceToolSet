#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    [Serializable]
    public class ProviderHasNotReadWritePermissionException : Exception
    {
        public ProviderHasNotReadWritePermissionException()
        {
        }

        public ProviderHasNotReadWritePermissionException(String message)
            : base(message)
        {
        }

        public ProviderHasNotReadWritePermissionException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProviderHasNotReadWritePermissionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}