#region Using Directives

using System;
using System.Runtime.Serialization;

#endregion

namespace NHibernateDataStore.Exceptions
{
    [Serializable]
    public class BaseException : ApplicationException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BaseException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}