using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Common
{
    /// <summary>
    /// Static class with some entity related methods
    /// </summary>
    public static class EntityHelper
    {
        private const string NOTVALID_CHARS = ",;:\"'\\/<>&?=\t\r\n ";  //  ,;:"'\/<>&?=

        /// <summary>
        /// Check if the specified code can be used safely (doesn't contain special characters)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="code"></param>
        public static void ValidateCode(string fieldName, string code)
        {
            if (code != null)
            {
                int index = code.LastIndexOfAny(NOTVALID_CHARS.ToCharArray());
                if (index >= 0)
                    throw new CodeInvalidCharsException(fieldName, NOTVALID_CHARS);
            }
        }
    }
}
