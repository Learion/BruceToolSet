using System;
using System.Globalization;
using System.Web;
using System.Web.SessionState;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["Filedata"];

            try
            {
                LoggerFacade.Log.Debug(this.GetType(), "The File Has Been Posted");

                Byte[] uploadedBytes = new Byte[file.ContentLength];

                file.InputStream.Read(uploadedBytes, 0, (int)file.ContentLength);

                String fileId = TempFileManagerProvider.TempFileManager.SaveFile(uploadedBytes); 

                context.Response.StatusCode = 200;

                int rnd = new Random(DateTime.Now.Millisecond).Next(1, 3);
                System.Threading.Thread.Sleep(rnd * 1000);

                context.Response.Write((String.Format(CultureInfo.InvariantCulture, "{0}|{1}Handler/RetrieveFile.ashx?fileId={2}", fileId, SEOToolSet.Common.WebHelper.WebAppRootPath, fileId)));
            }
            catch (Exception ex)
            {
                LoggerFacade.Log.LogException(this.GetType(), ex);
                context.Response.Write("ERROR");
            }
            finally
            {
                if (file.InputStream != null)
                    file.InputStream.Close();
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
