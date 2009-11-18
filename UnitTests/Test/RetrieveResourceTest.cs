#region Using Directives

using System;
using System.IO;
using System.Net;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SEOToolSet.Common;

#endregion

namespace UnitTests.Test
{
    [TestFixture]
    public class RetrieveResourceTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            portApplication = Settings.Default.WebAppPortApplication;
            uriApplication = Settings.Default.WebAppUriApplication;
            retrieveResourceHandlerUri = Settings.Default.RetrieveResourceHandlerUri;
            specificCulture = Settings.Default.SpecificCulture;
            nonExistentCulture = Settings.Default.NonExistentCulture;
        }

        #endregion

        private string portApplication;
        private string uriApplication;
        private string retrieveResourceHandlerUri;
        private string specificCulture;
        private string nonExistentCulture;

        /// <summary>
        /// The test checks if the handler assigns a specific JSON to the variable __resources in the global 'window' object if it is called without parameters.
        /// </summary>
        [Test]
        public void RetrievalCultureNotSpecified()
        {
            StreamReader responseStream = null;
            HttpWebResponse response;
            string jsReturned;
            var request = TestUtil.CreateRequest(new Uri(
                                                                uriApplication + ":" +
                                                                portApplication +
                                                                retrieveResourceHandlerUri
                                                                ));
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                responseStream = new StreamReader(response.GetResponseStream());
                jsReturned = responseStream.ReadToEnd();
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsReturned,
                        Text.Matches(
                            @"^(window.__resources\s=\s\{\s){1}(?:(?:\w)+(?:\s?\:\s?){1}""(?:.)+?""(?:,\s)?)+\s?\};$"),
                        "The returned stream is not the correct JSON");
            Console.WriteLine("Pass");
        }

        /// <summary>
        /// The test checks if the handler assigns a specific JSON to the variable __resources in the global 'window' object if it is called with a non existent culture.
        /// </summary>
        [Test]
        public void RetrievalNonExistentCulture()
        {
            StreamReader responseStream = null;
            HttpWebResponse response;
            string jsReturned;
            var request = TestUtil.CreateRequest(new Uri(
                                                                uriApplication + ":" +
                                                                portApplication +
                                                                retrieveResourceHandlerUri + "?" +
                                                                Constants.CultureParameter + "=" +
                                                                nonExistentCulture
                                                                ));
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                responseStream = new StreamReader(response.GetResponseStream());
                jsReturned = responseStream.ReadToEnd();
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsReturned,
                        Text.Matches(
                            @"^(window.__resources\s=\s\{\s){1}(?:(?:\w)+(?:\s?\:\s?){1}""(?:.)+?""(?:,\s)?)+\s?\};$"),
                        "The returned stream is not the correct JSON");
            Console.WriteLine("Pass");
        }

        /// <summary>
        /// The test checks if the handler assigns a specific JSON to the variable __resources in the global 'window' object if it is called with a specific culture.
        /// </summary>
        [Test]
        public void RetrievalWithSpecificCulture()
        {
            StreamReader responseStream = null;
            HttpWebResponse response;
            string jsReturned;
            var request = TestUtil.CreateRequest(new Uri(
                                                     uriApplication + ":" +
                                                     portApplication +
                                                     retrieveResourceHandlerUri + "?" +
                                                     Constants.CultureParameter + "=" +
                                                     specificCulture
                                                     ));
            try
            {
                response = (HttpWebResponse) request.GetResponse();
                responseStream = new StreamReader(response.GetResponseStream());
                jsReturned = responseStream.ReadToEnd();
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
            }
            Assert.That(jsReturned,
                        Text.Matches(
                            @"^(window.__resources\s=\s\{\s){1}(?:(?:\w)+(?:\s?\:\s?){1}""(?:.)+?""(?:,\s)?)+\s?\};$"),
                        "The returned stream is not the correct JSON");
            Console.WriteLine("Pass");
        }
    }
}