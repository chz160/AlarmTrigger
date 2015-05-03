using System;
using System.Net;

namespace AlarmFinder
{
    public class CustomWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            var webRequest = base.GetWebRequest(uri);
            if (webRequest != null)
            {
                webRequest.Timeout = Timeout;
            }
            return webRequest;
        }

        public int Timeout { get; set; }
    }
}