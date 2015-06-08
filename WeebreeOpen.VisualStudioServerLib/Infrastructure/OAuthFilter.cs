namespace WeebreeOpen.VisualStudioServerLib.Infrastructure
{
    using System;
    using System.Net.Http.Headers;

    public class OAuthFilter : IHttpRequestHeaderFilter
    {
        private readonly string authToken;

        public OAuthFilter(string authToken)
        {
            this.authToken = authToken;
        }

        public HttpRequestHeaders ProcessHeaders(HttpRequestHeaders headers)
        {
            headers.Authorization = new AuthenticationHeaderValue("Bearer", this.authToken);
            return headers;
        }
    }
}