using System;
using System.Runtime.Serialization;

namespace SEOToolSet.Providers.Exceptions
{
    public class ProjectRoleNameMustExistException : Exception
    {
        public ProjectRoleNameMustExistException(string message)
            : base(message)
        {

        }
        public ProjectRoleNameMustExistException()
        {
            
        }
        public ProjectRoleNameMustExistException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected ProjectRoleNameMustExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }
         
    }
}
