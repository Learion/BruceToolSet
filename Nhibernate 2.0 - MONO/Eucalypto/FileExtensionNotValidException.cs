using System;

namespace Eucalypto
{
    [Serializable]
    public class FileExtensionNotValidException : EucalyptoException
    {
        public FileExtensionNotValidException(string file)
            : base("File extension for " + file + " cannot be used for upload")
        {

        }
    }
}