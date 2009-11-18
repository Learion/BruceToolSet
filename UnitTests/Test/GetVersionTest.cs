#region Using Directives

using System;
using System.IO;
using System.Net;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

#endregion

namespace UnitTests.Test
{
    [TestFixture]
    public class GetVersionTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            webAppPortApplication = Settings.Default.WebAppPortApplication;
                //ConfigurationManager.AppSettings["WebAppPortApplication"];
            webAppUriApplication = Settings.Default.WebAppUriApplication;
                //ConfigurationManager.AppSettings["WebAppUriApplication"];
            reportServicesPortApplication = Settings.Default.ReportServicesPortApplication;
                // ConfigurationManager.AppSettings["ReportServicesPortApplication"];
            reportServicesUriApplication = Settings.Default.ReportServicesUriApplication;
            webAppGetVersionHandlerUri = Settings.Default.ReportServicesGetVersionHandlerUri;
            webAppGetVersionHandlerUri = Settings.Default.WebAppGetVersionHandlerUri;
            reportServicesGetVersionHandlerUri = Settings.Default.ReportServicesGetVersionHandlerUri;
        }

        #endregion

        private string webAppPortApplication;
        private string webAppUriApplication;
        private string reportServicesPortApplication;
        private string reportServicesUriApplication;
        private string webAppGetVersionHandlerUri;
        private string reportServicesGetVersionHandlerUri;

        [Test]
        public void ReportServicesRetrieval()
        {
            StreamReader responseStream = null;
            HttpWebRequest request;
            HttpWebResponse response;
            string jsonReturned;
            request = TestUtil.CreateRequest(
                new Uri(
                    reportServicesUriApplication + ":" +
                    reportServicesPortApplication +
                    reportServicesGetVersionHandlerUri
                    ));

            try
            {
                response = (HttpWebResponse) request.GetResponse();
                responseStream = new StreamReader(response.GetResponseStream());
                jsonReturned = responseStream.ReadToEnd();
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned, Text.Matches(@"^\{\s?""Version""\s?:(?:.)+\}$"),
                        "The returned stream is not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void WebAppRetrieval()
        {
            StreamReader responseStream = null;
            HttpWebRequest request;
            HttpWebResponse response;
            string jsonReturned;
            request = TestUtil.CreateRequest(
                new Uri(
                    webAppUriApplication + ":" +
                    webAppPortApplication +
                    webAppGetVersionHandlerUri
                    ));

            try
            {
                response = (HttpWebResponse) request.GetResponse();
                responseStream = new StreamReader(response.GetResponseStream());
                jsonReturned = responseStream.ReadToEnd();
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned, Text.Matches(@"^\{\s?""Version""\s?:(?:.)+\}$"),
                        "The returned stream is not the correct JSON");
            Console.WriteLine("Pass");
        }
    }
}