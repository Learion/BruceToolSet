using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Profile
{

    [Serializable]
    public class ProfileValueNotSupportedException : EucalyptoException
    {
        public ProfileValueNotSupportedException(string propertyName)
            : base("Profile property " + propertyName + " cannot be deserialized, value type not supported.")
        {

        }
    }
}
