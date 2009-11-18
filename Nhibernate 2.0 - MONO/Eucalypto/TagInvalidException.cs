using System;

namespace Eucalypto
{
    [Serializable]
    public class TagInvalidException : EucalyptoException
    {
        public TagInvalidException(string tag)
            : base("Tag " + tag + " not supported")
        {

        }
    }
}