namespace WeebreeOpen.VisualStudioServerLib.Infrastructure
{
    using System.Net.Http.Headers;

    public interface IHttpRequestHeaderFilter
    {
        HttpRequestHeaders ProcessHeaders(HttpRequestHeaders headers);
    }
}