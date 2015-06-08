namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    public class SimpleRestClient : RestClientVersion1, IVsoSimple
    {
        public SimpleRestClient(string rootUrl, NetworkCredential userCredential)
            : base(rootUrl, new BasicAuthenticationFilter(userCredential))
        {
        }

        protected override string SubSystemName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get object of type T from the URL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string url)
        {
            string response = await this.GetResponse(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected override string ConstructUrl(string projectName, string path, IDictionary<string, object> arguments)
        {
            return path;
        }
    }
}