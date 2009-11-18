#region Using Directives

using System;

#endregion

namespace NHibernateDataStore.Common
{
    public static class EntityHelper
    {
        private const string NOTVALID_CHARS = ",;:\"'\\/<>&?=\t\r\n "; //  ,;:"'\/<>&?=

        /// <summary>
        /// Check if the specified code can be used safely (doesn't contain special characters)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="code"></param>
        public static void ValidateCode(string fieldName, string code)
        {
            if (code == null) return;
            var index = code.LastIndexOfAny(NOTVALID_CHARS.ToCharArray());

            Check.Ensure(index < 0, "Not Valid Chars in field", new CodeInvalidCharsException(fieldName, NOTVALID_CHARS));
        }
    }

    public class CodeInvalidCharsException : Exception
    {
        public CodeInvalidCharsException(string fieldName, string invalidChars)
            : base("Field " + fieldName + " is not valid, cannot contains any of these characters: " + invalidChars)
        {
        }
    }
}