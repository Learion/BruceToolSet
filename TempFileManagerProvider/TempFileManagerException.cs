#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    [Serializable]
    public class TempFileManagerException : Exception
    {
        public TempFileManagerException()
        {
        }

        public TempFileManagerException(string message)
            : base(message)
        {
        }

        public TempFileManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TempFileManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}