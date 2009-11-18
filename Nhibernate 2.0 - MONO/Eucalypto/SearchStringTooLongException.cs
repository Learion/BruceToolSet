using System;

namespace Eucalypto
{
    [Serializable]
    public class SearchStringTooLongException : EucalyptoException
    {
        public SearchStringTooLongException()
            : base("Search string too long")
        {

        }
    }
}