using System;
using System.Text;
using System.Web;

namespace SEOToolSet.WebApp.Handler
{
    public class RetrieveFile : IHttpHandler
    {


        private String FileId
        {
            get
            {
                return HttpContext.Current.Request.QueryString["fileId"];
            }
        }

        private String action
        {
            get
            {
                return HttpContext.Current.Request.QueryString["action"];
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (String.IsNullOrEmpty(FileId))
                throw new Exception("fileId cout not be null nor Empty");

            var bytes = TempFileManagerProvider.TempFileManager.GetFileById(FileId);
            if (bytes == null) throw new Exception(string.Format("No file was found for the fileID : {0}", FileId));

            if (action == "GetPDFReport")
            {
                var fileName = HttpContext.Current.Request.QueryString["pdfName"] ?? "RankingMonitorReport";

                context.Response.Clear();
                context.Response.Buffer = true;

                context.Response.AddHeader("Content-Disposition", string.Format("inline; Filename={0}.pdf", fileName.Replace(" ", "_")));
                context.Response.ContentType = "application/pdf";
                context.Response.ContentEncoding = Encoding.Default;
                context.Response.BinaryWrite(bytes);
            }
            else
            {
                context.Response.ContentType = "text/html";
                context.Response.BinaryWrite(bytes);
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
