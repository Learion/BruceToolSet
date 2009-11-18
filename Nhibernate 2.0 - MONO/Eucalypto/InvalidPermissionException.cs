using System;

namespace Eucalypto
{
    [Serializable]
    public class InvalidPermissionException : EucalyptoException
    {
        public InvalidPermissionException(string action)
            : base("You don't have the permission to " + action + ".")
        {

        }
    }
}