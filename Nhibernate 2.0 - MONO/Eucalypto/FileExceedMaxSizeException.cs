using System;

namespace Eucalypto
{
    [Serializable]
    public class FileExceedMaxSizeException : EucalyptoException
    {
        public FileExceedMaxSizeException(string file)
            : base("File " + file + " exceed the maximum file size")
        {

        }
    }
}