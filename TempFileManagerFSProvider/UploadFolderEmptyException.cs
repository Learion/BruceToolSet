#region Using Directives

using System;
using System.Runtime.Serialization;
using SEOToolSet.TempFileManagerProvider;

#endregion

namespace SEOToolSet.TempFileManagerFSProvider
{
    [Serializable]
    public class UploadFolderEmptyException : TempFileManagerException
    {
        public UploadFolderEmptyException()
            : base("UploadFolder Attribute not found or empty")
        {
        }

        public UploadFolderEmptyException(string message)
            : base("UploadFolder Attribute not found or empty. " + message)
        {
        }

        public UploadFolderEmptyException(string message, Exception innerException)
            : base("UploadFolder Attribute not found or empty. " + message, innerException)
        {
        }

        protected UploadFolderEmptyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class FileSaveException : Exception
    {
        public FileSaveException()
        {
        }

        public FileSaveException(String fileName)
            : base("Could not Save the file : " + fileName)
        {
        }

        public FileSaveException(String fileName, Exception innerException)
            : base("Could not Save the file : " + fileName, innerException)
        {
        }

        protected FileSaveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class FileGetException : Exception
    {
        public FileGetException()
        {
        }

        public FileGetException(String fileName)
            : base("Could not Retrieve the file with ID : " + fileName)
        {
        }

        public FileGetException(String fileName, Exception innerException)
            : base("Could not Retrieve the file with ID : " + fileName, innerException)
        {
        }

        protected FileGetException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class FileDeleteException : Exception
    {
        public FileDeleteException()
        {
        }

        public FileDeleteException(String fileName)
            : base("Could not Delete the file with ID : " + fileName)
        {
        }

        public FileDeleteException(String fileName, Exception innerException)
            : base("Could not Delete the file with ID : " + fileName, innerException)
        {
        }

        protected FileDeleteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}