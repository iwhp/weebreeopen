namespace WeebreeOpen.VisualStudioServerLib.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;

    /// <summary>
    /// Base class for TFS subsystem REST API client
    /// </summary>
    public abstract class VsoRestClient
    {
        public VsoRestClient(string rootUrl, IHttpRequestHeaderFilter authProvider)
        {
            this.rootUrl = rootUrl;
            this.authProvider = authProvider;
        }

        protected const string JsonMediaType = "application/json";
        protected const string JsonPatchMediaType = "application/json-patch+json";
        protected const string HtmlMediaType = "text/html";
        private readonly string rootUrl;
        private readonly IHttpRequestHeaderFilter authProvider;

        protected abstract string SubSystemName { get; }

        protected abstract string ApiVersion { get; }

        #region GET operation

        protected async Task<string> GetResponse(string path, string projectName = null)
        {
            return await this.GetResponse(path, new Dictionary<string, object>(), projectName);
        }

        protected async Task<string> GetResponse(string path, IDictionary<string, object> arguments, string projectName = null, string mediaType = JsonMediaType)
        {
            using (HttpClient client = this.GetHttpClient(mediaType))
            {
                using (HttpResponseMessage response = client.GetAsync(this.ConstructUrl(projectName, path, arguments)).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw JsonConvert.DeserializeObject<VsoException>(responseBody);
                    }

                    return responseBody;
                }
            }
        }

        #endregion

        #region POST operation

        protected async Task<string> PostResponse(string path, object content, string projectName = null)
        {
            return await this.PostResponse(path, new Dictionary<string, object>(), content, projectName);
        }

        protected async Task<string> PostResponse(string path, IDictionary<string, object> arguments, object content, string projectName = null, string mediaType = JsonMediaType)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, mediaType);

            using (HttpClient client = this.GetHttpClient(mediaType))
            {
                using (HttpResponseMessage response = client.PostAsync(this.ConstructUrl(projectName, path, arguments), httpContent).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw JsonConvert.DeserializeObject<VsoException>(responseBody);
                    }

                    return responseBody;
                }
            }
        }

        #endregion

        #region PATCH operation

        protected async Task<string> PatchResponse(string path, object content, string projectName = null, string mediaType = JsonPatchMediaType)
        {
            return await this.PatchResponse(path, new Dictionary<string, object>(), content, projectName, mediaType);
        }

        protected async Task<string> PatchResponse(string path, IDictionary<string, object> arguments, object content, string projectName = null, string mediaType = JsonPatchMediaType)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, mediaType);

            using (HttpClient client = this.GetHttpClient(mediaType))
            {
                using (HttpResponseMessage response = client.PatchAsync(this.ConstructUrl(projectName, path, arguments), httpContent).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw JsonConvert.DeserializeObject<VsoException>(responseBody);
                    }

                    return responseBody;
                }
            }
        }

        #endregion

        #region PUT operation

        protected async Task<string> PutResponse(string path, object content, string projectName = null, string mediaType = JsonMediaType)
        {
            return await this.PutResponse(path, new Dictionary<string, object>(), content, projectName, mediaType);
        }

        protected async Task<string> PutResponse(string path, IDictionary<string, object> arguments, object content, string projectName = null, string mediaType = JsonMediaType)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, mediaType);

            using (HttpClient client = this.GetHttpClient(mediaType))
            {
                using (HttpResponseMessage response = client.PutAsync(this.ConstructUrl(projectName, path, arguments), httpContent).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw JsonConvert.DeserializeObject<VsoException>(responseBody);
                    }

                    return responseBody;
                }
            }
        }

        #endregion

        #region DELETE operation

        protected async Task<string> DeleteResponse(string path, string projectName = null)
        {
            using (HttpClient client = this.GetHttpClient())
            {
                using (HttpResponseMessage response = client.DeleteAsync(this.ConstructUrl(projectName, path)).Result)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw JsonConvert.DeserializeObject<VsoException>(responseBody);
                    }

                    return responseBody;
                }
            }
        }

        #endregion

        private HttpClient GetHttpClient(string mediaType = JsonMediaType)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            this.authProvider.ProcessHeaders(client.DefaultRequestHeaders);
            return client;
        }

        private string ConstructUrl(string projectName, string path)
        {
            return this.ConstructUrl(projectName, path, new Dictionary<string, object>());
        }

        protected virtual string ConstructUrl(string projectName, string path, IDictionary<string, object> arguments)
        {
            if (!arguments.ContainsKey("api-version"))
            {
                arguments.Add("api-version", this.ApiVersion);
            }

            StringBuilder resultUrl = new StringBuilder(
                string.IsNullOrEmpty(projectName) ? string.Format("{0}/_apis/{1}", this.rootUrl, this.SubSystemName) : string.Format("{0}/{1}/_apis/{2}", this.rootUrl, projectName, this.SubSystemName));

            if (!string.IsNullOrEmpty(path))
            {
                resultUrl.AppendFormat("/{0}", path);
            }

            resultUrl.AppendFormat("?{0}", string.Join("&", arguments.Where(kvp => kvp.Value != null).Select(kvp =>
            {
                if (kvp.Value is IEnumerable<string>)
                {
                    return string.Join("&", ((IEnumerable<string>)kvp.Value).Select(v => string.Format("{0}={1}", kvp.Key, v)));
                }
                else
                {
                    return string.Format("{0}={1}", kvp.Key, kvp.Value);
                }
            })));
            return resultUrl.ToString();
        }
    }
}