#region Using Directives

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

#endregion

namespace UnitTests.Test
{
    [TestFixture]
    public class UploadHandlerTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            portApplication = Settings.Default.WebAppPortApplication;
            uriApplication = Settings.Default.WebAppUriApplication;
            uploadHandlerUri = Settings.Default.UploadHandlerUri;
            fileToUpload = Path.Combine(Environment.CurrentDirectory, Settings.Default.FileToUpload);

            if (!File.Exists(fileToUpload))
            {
                throw new Exception("File to upload not found");
            }
        }

        #endregion

        private string portApplication;
        private string uriApplication;
        private string uploadHandlerUri;
        private string fileToUpload;

        private static bool OnlyNumbers(string numbers)
        {
            var regx = new Regex(@"^[\d]{17}$");
            return regx.IsMatch(numbers);
        }

        private static HttpWebResponse UploadFileEx(string uploadfile,
                                                    string url,
                                                    string fileFormName,
                                                    string contenttype,
                                                    NameValueCollection querystring,
                                                    CookieContainer cookies)
        {
            if (string.IsNullOrEmpty(fileFormName))
                fileFormName = "file";
            if (string.IsNullOrEmpty(contenttype))
                contenttype = "application/octet-stream";

            var postdata = "?";
            if (querystring != null)
            {
                foreach (string key in querystring.Keys)
                    postdata += key + "=" + querystring.Get(key) + "&";
            }
            var uri = new Uri(url + postdata);


            var boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            var webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.CookieContainer = cookies;
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";

            // Build up the post message header
            var sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(Path.GetFileName(uploadfile));
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array
            // ensuring the boundary appears on a line by itself
            byte[] boundaryBytes =
                Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var fileStream = new FileStream(uploadfile,
                                            FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length +
                          boundaryBytes.Length;
            webrequest.ContentLength = length;

            Stream requestStream = webrequest.GetRequestStream();

            // Write out our post header
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // Write out the file contents
            var buffer = new Byte[checked((uint)Math.Min(4096,
                                                          (int)fileStream.Length))];
            int bytesRead;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // Write out the trailing boundary
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            return webrequest.GetResponse() as HttpWebResponse;
        }

        [Test]
        public void SuccessfulHandlerCommunication()
        {
            StreamReader responseStream = null;
            String filePath;


            var response = UploadFileEx(fileToUpload,
                                        uriApplication + ":" + portApplication + uploadHandlerUri,
                                        "Filedata",
                                        null,
                                        null,
                                        null);
            try
            {
                responseStream = new StreamReader(response.GetResponseStream());
                filePath = responseStream.ReadToEnd();
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }



            var result = filePath.Split('|');

            Assert.IsTrue((Uri.IsWellFormedUriString(result[1], UriKind.Absolute) && (OnlyNumbers(result[0]))));
        }
    }
}