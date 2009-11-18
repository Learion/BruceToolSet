
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Text;
using System.Configuration;
using SEOToolSet.Common;

namespace SEOToolSet.WebApp.Handler
{
    public class RetrieveResource : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            XmlReader reader;
            var request = context.Request;
            var response = context.Response;

            var directoryPath = context.Server.MapPath("~"
                                                          + Path.DirectorySeparatorChar
                                                          + "App_GlobalResources");
            var resourceFileName = ConfigurationManager.AppSettings["JavascriptResourceFile"];
            var culture = request[Constants.CultureParameter] ?? string.Empty;
            string filePath = null;
            var jsonResult = new StringBuilder("{ ");
            //Validating if the directory file exists
            if ( Directory.Exists(directoryPath) )
            {
                //Finding the resource that fits the requested culture
                while ( filePath == null && culture.Length >= 0 )
                {
                    filePath = directoryPath
                                + Path.DirectorySeparatorChar
                                + resourceFileName
                                + ( culture.Length > 0 ? "." + culture : "" ) + ".resx";
                    if (File.Exists(filePath)) continue;

                    filePath = null;
                    culture = culture.Substring(0, culture.Length - 1); //Reducing the culture
                }
                if (filePath != null)
                    using ( reader = XmlReader.Create(File.OpenText(filePath)) )
                    {
                        reader.ReadStartElement("root");
                        while ( reader.ReadToFollowing("data") )
                        {
                            if (reader.NodeType != XmlNodeType.Element || !reader.IsStartElement() ||
                                reader.IsEmptyElement) continue;
                            jsonResult.Append(reader.GetAttribute("name") + " : \"");
                            reader.Read(); // Read the start tag.
                            if ( reader.IsStartElement() )  // Get the values
                                jsonResult.Append(reader.ReadString() + "\", ");
                        }
                    }
                jsonResult.Remove(jsonResult.Length - 2, 2).Append("};");
                response.ContentType = "text/javascript";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Output.Write("window.__resources = " + jsonResult);
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
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
