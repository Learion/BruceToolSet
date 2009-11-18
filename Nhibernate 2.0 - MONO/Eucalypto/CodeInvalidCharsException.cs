using System;

namespace Eucalypto
{
    [Serializable]
    public class CodeInvalidCharsException : EucalyptoException
    {
        public CodeInvalidCharsException(string fieldName, string invalidChars)
            : base("Field " + fieldName + " is not valid, cannot contains any of these characters: " + invalidChars)
        {

        }
    }
}