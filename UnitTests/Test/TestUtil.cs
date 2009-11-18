#region Using Directives

using System;
using System.Net;

#endregion

namespace UnitTests.Test
{
    public static class TestUtil
    {
        public static HttpWebRequest CreateRequest(Uri requestUri)
        {
            Console.WriteLine("Requested URI: {0}", requestUri.AbsoluteUri);
            var request = (HttpWebRequest) WebRequest.Create(requestUri);
            Console.WriteLine("The URI was requested");
            return request;
        }
    }
}