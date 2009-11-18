using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto
{
    [Serializable]
    public class TagAttributeInvalidException : EucalyptoException
    {
        public TagAttributeInvalidException(string attribute)
            : base("Element attribute " + attribute + " not supported")
        {

        }
    }
}
