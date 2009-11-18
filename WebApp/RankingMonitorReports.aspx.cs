using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace SEOToolSet.WebApp
{
    public partial class RankingMonitorReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Resources.CommonTerms             
        }

        protected void ExportToCSV(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CSVData.Value)) return;
            //TODO Check if the Filename is a valid
            var strFileNameexport = FileNameHidden.Value.Replace(" ", "_");
            Response.Clear();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "text/comma-separated-values";

            //Those lines makes this to work with UTF8.
            Response.ContentEncoding = Encoding.UTF8;
            Response.Charset = Encoding.UTF8.HeaderName;
            var data = CSVData.Value;

            //Those lines makes it possible to work with UCS2
            /*Response.ContentEncoding = Encoding.u;
            Response.Charset = Encoding.Unicode.HeaderName;

            var data = GetUCSStringFromData(CSVData.Value);*/

            Response.AddHeader("Content-Disposition", string.Format("attachment;Filename={0}.csv", strFileNameexport));

            Response.Write(data);
            Response.End();
        }

        private static byte[] GetUCSStringFromData(string value)
        {
            if (String.IsNullOrEmpty(value)) return new byte[] { };

            value = value.Replace("\",\"", "\"\t\"");

            var utf8Encoding = Encoding.UTF8;
            var utfBytes = utf8Encoding.GetBytes(value);

            var ucs2Encoding = Encoding.GetEncoding("utf-16le");
            var ucs2Bytes = Encoding.Convert(utf8Encoding, ucs2Encoding, utfBytes);

            var bytesFinal = new List<byte> {0xff, 0xfe};
            foreach (var b in ucs2Bytes)
            {
                bytesFinal.Add(b);
            }

            return bytesFinal.ToArray(); // ucs2Encoding.GetString(ucs2Bytes);

        }

        protected void ExportToCSVForExcel(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CSVData.Value)) return;

            var strFileNameexport = FileNameHidden.Value.Replace(" ", "_");
            Response.Clear();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "text/comma-separated-values";

            //Those lines makes it possible to work with UCS2
            Response.ContentEncoding = Encoding.GetEncoding("utf-16le");
            Response.Charset = Response.ContentEncoding.HeaderName;

            var data = GetUCSStringFromData(CSVData.Value);

            Response.AddHeader("Content-Disposition", string.Format("attachment;Filename={0}.csv", strFileNameexport));
            
            Response.BinaryWrite(data);
            Response.End();
        }
    }
}
