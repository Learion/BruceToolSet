using System;

namespace Eucalypto
{
    [Serializable]
    public class TextNotValidException : EucalyptoException
    {
        public TextNotValidException(Exception innerException)
            : base("Text not valid: " + innerException.Message, innerException)
        {

        }
    }
}