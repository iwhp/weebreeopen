namespace WeebreeOpen.VisualStudioServerLib.Infrastructure
{
    public abstract class RestClientVersion1 : VsoRestClient
    {
        public RestClientVersion1(string rootUrl, IHttpRequestHeaderFilter authProvider)
            : base(rootUrl, authProvider)
        {
        }

        protected override string ApiVersion
        {
            get
            {
                return VsoApi.Version1;
            }
        }
    }
}