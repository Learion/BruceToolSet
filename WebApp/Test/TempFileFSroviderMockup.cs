using System.IO;
using SEOToolSet.TempFileManagerProvider;

namespace SEOToolSet.WebApp.Test
{
    public class TempFileFSProviderMockupSuccessful : TempFileManagerProviderBase
    {

        public override string SaveFile(byte[] binaryData)
        {
            return "SomeID";
        }

        public override byte[] GetFileById(string fileId)
        {
            return File.ReadAllBytes("c:\\logon_app_trace.txt");
        }

        public override void DeleteFile(string fileId)
        {
            
        }

        public override void PurgeOldFiles(System.TimeSpan maxAge)
        {
                        
        }

        public override bool CanWrite()
        {
            return true;
        }
    }
}
