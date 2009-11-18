using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Membership
{
    [Serializable]
    public class UserNotFoundException : EucalyptoException
    {
        public UserNotFoundException(string user)
            : base("User " + user + " not found or invalid password")
        {

        }
    }

    [Serializable]
    public class EMailRequiredException : EucalyptoException
    {
        public EMailRequiredException(string user)
            : base("User " + user + " don't have a valid email specified.")
        {

        }
    }


    [Serializable]
    public class EMailNotValidException : EucalyptoException
    {
        public EMailNotValidException(string email)
            : base("EMail " + email + " not valid")
        {

        }
    }

    [Serializable]
    public class EMailDuplucatedException : EucalyptoException
    {
        public EMailDuplucatedException(string email)
            : base("EMail " + email + " duplicated")
        {

        }
    }
}
