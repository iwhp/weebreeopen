namespace WeebreeOpen.VisualStudioServerLib.Infrastructure
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;

    public class BasicAuthenticationFilter : IHttpRequestHeaderFilter
    {
        private readonly string authToken;

        public BasicAuthenticationFilter(NetworkCredential userCredential)
        {
            this.authToken = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userCredential.UserName, userCredential.Password)));
        }

        public HttpRequestHeaders ProcessHeaders(HttpRequestHeaders headers)
        {
            headers.Authorization = new AuthenticationHeaderValue("Basic", this.authToken);
            return headers;
        }
    }
}