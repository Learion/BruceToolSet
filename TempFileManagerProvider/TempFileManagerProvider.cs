#region Using Directives

using System;
using System.Configuration.Provider;

#endregion

namespace SEOToolSet.TempFileManagerProvider
{
    public abstract class TempFileManagerProviderBase : ProviderBase
    {
        public Int32 MaxAgeOfFilesInMinutes { get; set; }

        public Boolean AutoStartPurge { get; set; }

        public abstract String SaveFile(Byte[] binaryData);

        public abstract Byte[] GetFileById(String fileId);

        public abstract void DeleteFile(string fileId);

        public abstract void PurgeOldFiles(TimeSpan maxAge);

        public abstract Boolean CanWrite();
    }
}