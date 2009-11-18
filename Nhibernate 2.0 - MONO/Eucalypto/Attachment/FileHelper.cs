using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Attachment
{
    public static class FileHelper
    {
        public const string EXTENSIONS_NONE = "";
        public const string EXTENSIONS_ALL = "*";

        public const string EXTENSIONS_IMAGE = ".gif,.jpg,.png,.bmp,.ico";
        public const string EXTENSIONS_DOC = ".txt,.rtf,.doc,.pdf,.htm,.html,.xls,.ppt,.pps";
        public const string EXTENSIONS_CODE = ".cs,.vb,.xml,.cpp,.h,.c";
        public const string EXTENSIONS_ARCHIVE = ".zip,.rar";

        public static string ReplaceExtensionsSets(string extensions)
        {
            //Replace common sets
            extensions = extensions.Replace("ALL_IMAGES", EXTENSIONS_IMAGE);
            extensions = extensions.Replace("ALL_DOCS", EXTENSIONS_DOC);
            extensions = extensions.Replace("ALL_ARCHIVE", EXTENSIONS_ARCHIVE);
            extensions = extensions.Replace("ALL_CODE", EXTENSIONS_CODE);

            return extensions;
        }

        public static string[] ParseExtensions(string extensions)
        {
            extensions = extensions.Replace(" ", "");

            extensions = ReplaceExtensionsSets(extensions);

            return extensions.Split(',');
        }

        public static bool IsValidFileExtension(string acceptedExtensions, string fileName)
        {
            if (acceptedExtensions == null || acceptedExtensions.Length == 0)
                return false;

            if (acceptedExtensions == EXTENSIONS_ALL)
                return true;

            string[] extensions = ParseExtensions(acceptedExtensions);

            string extension = System.IO.Path.GetExtension(fileName);

            for (int i = 0; i < extensions.Length; i++)
            {
                if (string.Equals(extension, extensions[i], StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="acceptedExtensions">The list of accepted extensions.</param>
        /// <param name="maximumSize">Maximum KB for the uploaded file</param>
        public static void CheckFile(FileInfo file, string acceptedExtensions, int maximumSize)
        {
            int sizeKb = file.ContentData.Length / 1024;
            if (sizeKb > maximumSize)
                throw new FileExceedMaxSizeException(file.Name);

            if (IsValidFileExtension(acceptedExtensions, file.Name) == false)
                throw new FileExtensionNotValidException(file.Name);
        }

    }
}
