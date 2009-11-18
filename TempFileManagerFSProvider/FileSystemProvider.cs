#region Using Directives

using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Web;
using SEOToolSet.TempFileManagerProvider;

#endregion

namespace SEOToolSet.TempFileManagerFSProvider
{
    public class FileSystemProvider : TempFileManagerProviderBase
    {
        public String ProviderName { get; set; }

        public string TempFileExtension { get; set; }

        private string tempFolderPath { get; set; }

        public String TempFolderLocation
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    var path = HttpContext.Current.Server.MapPath(tempFolderPath);
                    if (!path.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                    {
                        path += Path.DirectorySeparatorChar.ToString();
                    }
                    return path;
                }
                return null;
            }
        }

        public override void Initialize(String name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Trim().Length == 0)
                name = "FileSytemProvider";

            base.Initialize(name, config);

            ProviderName = name;

            var relativeUploadFolder = ProviderHelper.ExtractConfigValue(config, "TempFolderLocation", String.Empty);
            if (relativeUploadFolder.Trim().Length == 0)
            {
                throw new UploadFolderEmptyException();
            }

            TempFileExtension = ProviderHelper.ExtractConfigValue(config, "TempFileExtension", ".html");

            AutoStartPurge = ProviderHelper.ExtractConfigValue(config, "AutoStartPurge", false);

            MaxAgeOfFilesInMinutes = ProviderHelper.ExtractConfigValue(config, "MaxAgeOfFilesInMinutes", 0);

            /*if (HttpContext.Current != null)
                TempFolderLocation = HttpContext.Current.Server.MapPath(relativeUploadFolder);
            else */

            tempFolderPath = relativeUploadFolder;


            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                var attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " +
                                                attr);
            }
        }

        public override string SaveFile(byte[] binaryData)
        {
            if (binaryData == null || binaryData.Length == 0)
            {
                throw new FileSaveException("binaryData Data could not be null nor empty");
            }
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            try
            {
                if (TempFolderLocation != null)
                    File.WriteAllBytes(TempFolderLocation + fileName + TempFileExtension, binaryData);
            }
            catch (Exception ex)
            {
                throw new FileSaveException(fileName, ex);
            }

            return fileName;
        }

        public override byte[] GetFileById(string fileId)
        {
            try
            {
                return TempFolderLocation != null
                           ? File.ReadAllBytes(TempFolderLocation + fileId + TempFileExtension)
                           : null;
            }
            catch (Exception ex)
            {
                throw new FileGetException(fileId, ex);
            }
        }

        public override void DeleteFile(string fileId)
        {
            try
            {
                if (TempFolderLocation != null) File.Delete(TempFolderLocation + fileId + TempFileExtension);
            }
            catch (Exception ex)
            {
                throw new FileDeleteException(fileId, ex);
            }
        }

        public override void PurgeOldFiles(TimeSpan maxAge)
        {
            if (TempFolderLocation == null) return;
            var temporaryDir = new DirectoryInfo(TempFolderLocation);
            var fInfos = temporaryDir.GetFiles(String.Format(CultureInfo.CurrentCulture, "*{0}", TempFileExtension));

            if (fInfos.Length <= 0) return;
            foreach (var itemFile in fInfos)
            {
                if (itemFile.LastWriteTime < DateTime.Now.AddMinutes(-1 * maxAge.Minutes))
                {
                    itemFile.Delete();
                }
            }
        }

        public override bool CanWrite()
        {
            if (TempFolderLocation != null)
            {
                var dir = new DirectoryInfo(TempFolderLocation);
                return dir.Exists && !((dir.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
            }
            return false;
        }
    }
}