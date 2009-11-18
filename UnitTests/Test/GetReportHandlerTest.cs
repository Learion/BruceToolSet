#region Using Directives

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SEOToolSet.Common;

#endregion

namespace UnitTests.Test
{
    /// <summary>
    /// Tests the possible scenarios when calling the GetReport class
    /// </summary>
    [TestFixture]
    public class GetReportHandlerTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            var rnd = new Random(DateTime.Now.Second);
            portApplication = Settings.Default.ReportServicesPortApplication;
            uriApplication = Settings.Default.ReportServicesUriApplication;
            //ConfigurationManager.AppSettings["ReportServicesUriApplication"];
            uriToGetReport = Settings.Default.UriToGetReport; //ConfigurationManager.AppSettings["UriToGetReport"];
            badUriParameter = Settings.Default.BadUriParameter; //ConfigurationManager.AppSettings["BadUriParameter"];
            correctUriParameter = Settings.Default.CorrectUriParameter;
            //ConfigurationManager.AppSettings["CorrectUriParameter"];
            randomReport = rnd.Next(1, Enum.GetNames(typeof(GeneralReport)).Length);
        }

        #endregion

        private int randomReport;
        private string portApplication;
        private string uriApplication;
        private string uriToGetReport;
        private string badUriParameter;
        private string correctUriParameter;

        private string CreateUrlWithParameters(string uriPageParameterValue, string kindOfReportParameterValue)
        {
            var requestUri = new StringBuilder();
            requestUri.Append(uriApplication);
            requestUri.Append(":" + portApplication);
            requestUri.Append(uriToGetReport);
            requestUri.Append(string.Format(CultureInfo.InvariantCulture, "?{0}={1}&{2}={3}", Constants.UriPageParameter,
                                            uriPageParameterValue, Constants.KindOfReportParameter,
                                            kindOfReportParameterValue));
            return requestUri.ToString();
        }

        [Test, ExpectedException(typeof(WebException))]
        public void BadUriPageRequested()
        {
            try
            {
                var request = TestUtil.CreateRequest(
                    new Uri(CreateUrlWithParameters(badUriParameter,
                                                    randomReport.ToString(NumberFormatInfo.InvariantInfo))));
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                    Console.WriteLine("Pass");
                }
                throw;
            }
        }

        [Test, ExpectedException(typeof(WebException))]
        public void KindOfReportParameterNotDeclared()
        {
            try
            {
                var request = TestUtil.CreateRequest(new Uri(CreateUrlWithParameters(correctUriParameter, string.Empty)));
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                    Console.WriteLine("Pass");
                }

                throw;
            }
        }

        [Test, ExpectedException(typeof(WebException))]
        public void KindOfReportParameterNotDefined()
        {
            try
            {
                var request = TestUtil.CreateRequest(
                    new Uri(CreateUrlWithParameters(correctUriParameter,
                                                    (randomReport + Enum.GetNames(typeof(GeneralReport)).Length).
                                                        ToString(NumberFormatInfo.InvariantInfo))));
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                    Console.WriteLine("Pass");
                }

                throw;
            }
        }

        [Test]
        public void SuccessfulLinearKeywordDistributionReport()
        {
            Stream responseStream = null;
            HttpWebResponse response;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.LinearKeywordDistribution).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned.ToString(),
                        Text.Matches(@"^(\{""Keywords"":)(.+)(""Name"":)+(.+)(""SrcImage"":)+(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void SuccessfulLinkTextReport()
        {
            Stream responseStream = null;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.LinkText).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned.ToString(),
                        Text.Matches(@"^(\{""SummaryLinks"":)(.+)(""Total"":){1}(.+)(""UniqueTargets"":){1}(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void SuccessfulOptimizedKeywordsReport()
        {
            Stream responseStream = null;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.OptimizedKeywords).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned.ToString(),
                        Text.Matches(@"^(\{""Keywords"":)(.+)(""Name"":)+(.+)(""Used"":)+(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void SuccessfulTagInformationReport()
        {
            Stream responseStream = null;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.TagInformation).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned.ToString(),
                        Text.Matches(
                            @"^(\{""Tags"":)(.+)(""Name"":)+(.+)(""StopWords"":)+(.+)(""UsedWords"":)+(.+)(""Length"":)+(.+)(""Data"":)+(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void SuccessfulToolSetKeywordsReport()
        {
            Stream responseStream = null;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.ToolSetKeywords).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }

            Assert.That(jsonReturned.ToString(),
                        Text.Matches(@"^(\{""Keywords"":)(.+)(""Name"":)+(.+)(""Used"":)+(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void SuccessfulWordMetricsReport()
        {
            Stream responseStream = null;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.WordMetrics).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned.ToString(),
                        Text.Matches(
                            @"^(\{""WordMetrics"":)(.+)(""Name"":)+(.+)(""Value"":)+(.+)(""Description"":)+(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test]
        public void SuccessfulWordPhrasesReport()
        {
            Stream responseStream = null;
            var jsonReturned = new StringBuilder();
            var request = TestUtil.CreateRequest(
                new Uri(CreateUrlWithParameters(correctUriParameter,
                                                ((int)GeneralReport.WordPhrases).ToString(
                                                    NumberFormatInfo.InvariantInfo))));
            var response = (HttpWebResponse)request.GetResponse();
            try
            {
                responseStream = response.GetResponseStream();
                int currentByte;
                while ((currentByte = responseStream.ReadByte()) > -1)
                {
                    jsonReturned.Append(char.ConvertFromUtf32(currentByte));
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsonReturned.ToString(),
                        Text.Matches(
                            @"^(\{""Phrases"":\[\{)(""Count"":)+(.+)(""Name"":)+(.+)(""MetaTitle"":)+(.+)(""MetaTitle"":)+(.+)(""MetaTitle"":)+(.+)(""MetaDesc"":)+(.+)(""MetaKeywords"":)+(.+)(""Heads"":)+(.+)(""AltTags"":)+(.+)(""FirstWords"":)+(.+)(""BodyWords"":)+(.+)(""AllWords"":)+(.+)\}$"),
                        "The returned streams not the correct JSON");
            Console.WriteLine("Pass");
        }

        [Test, ExpectedException(typeof(WebException))]
        public void UriPageParameterNotDeclared()
        {
            try
            {
                var request = TestUtil.CreateRequest(
                    new Uri(CreateUrlWithParameters(string.Empty,
                                                    randomReport.ToString(NumberFormatInfo.InvariantInfo))));
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                    Console.WriteLine("Pass");
                }

                throw;
            }
        }
    }
}