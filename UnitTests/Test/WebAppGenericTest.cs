#region Using Directives

using System;
using System.Net;
using NUnit.Framework;

#endregion

namespace UnitTests.Test
{
    [TestFixture]
    public class WebAppGenericTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
            portApplication = Settings.Default.WebAppPortApplication;
            uriApplication = Settings.Default.WebAppUriApplication;
            nonExistentPage = Settings.Default.NonExistentPage;
        }

        #endregion

        private string portApplication;
        private string uriApplication;
        private string nonExistentPage;

        /// <summary>
        /// Checks if a non existent page causes an error (its status code should be 200 if it is correct).
        /// </summary>
        [Test]
        public void PageNonLocated()
        {
            var request = TestUtil.CreateRequest(new Uri(
                                                     uriApplication + ":" +
                                                     portApplication +
                                                     nonExistentPage
                                                     ));
            var response = (HttpWebResponse)request.GetResponse();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Expected a page with Http status code 200 (OK)");
            Console.WriteLine("Pass");
        }
    }
}