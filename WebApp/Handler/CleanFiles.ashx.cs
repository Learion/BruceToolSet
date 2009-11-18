using System;
using System.IO;
using System.Web;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Summary description for CleanFiles handler
    /// </summary>
    public class CleanFiles : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context == null || context.Request.Form["action"] != "DeleteTempFiles") return;
            
            LoggerFacade.Log.Debug(this.GetType(), "FileNames Received " + context.Request.Form["FileNames"]);

            String[] FileIds = null;
            try
            {
                FileIds = context.Request.Form["FileNames"].Split('|');
            }
            catch (NullReferenceException ex)
            {
                LoggerFacade.Log.Debug(this.GetType(), ex.Message);
            }

            if (FileIds != null)
            {
                for (int i = 0; i < FileIds.Length; i++)
                {
                    String fileId = String.Empty;
                    try
                    {
                        fileId = FileIds[i];
                        if ((fileId == null) || (fileId.Trim().Length == 0)) continue;

                        //path = context.Server.MapPath("~" + Path.AltDirectorySeparatorChar.ToString() + "Upload" + Path.AltDirectorySeparatorChar.ToString() + FileIDs[i].Trim());
                        //LoggerFacade.Log.Debug(this.GetType(), "About to delete file " + path);
                        //File.Delete(path);
                        //LoggerFacade.Log.Debug(this.GetType(), "file deleted sucessfully " + path);

                        LoggerFacade.Log.Debug(this.GetType(), "About to delete file with ID : " + fileId);
                        TempFileManagerProvider.TempFileManager.DeleteFile(fileId);
                        LoggerFacade.Log.Debug(this.GetType(), "file with ID DELETED : " + fileId);

                        context.Response.Write("Clean Completed");
                    }
                    catch (TempFileManagerProvider.TempFileManagerException ex)
                    {
                        LoggerFacade.Log.Debug(this.GetType(), "[Error] : the file was not deleted -> " + fileId);
                        LoggerFacade.Log.LogException(this.GetType(), ex);
                    }
                }
            }
            else
            {
                LoggerFacade.Log.Debug(this.GetType(), "Impossible to erase files because FileNames is Null");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
