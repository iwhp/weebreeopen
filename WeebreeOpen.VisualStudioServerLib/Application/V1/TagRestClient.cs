namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    /// <summary>
    /// Tagging REST API client
    /// </summary>
    public class TagRestClient : RestClientVersion1, IVsoTag
    {
        public TagRestClient(string url, NetworkCredential userCredential)
            : base(url, new BasicAuthenticationFilter(userCredential))
        {
        }

        protected override string SubSystemName
        {
            get
            {
                return "tagging";
            }
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Tag> CreateTag(string scopeId, string name)
        {
            string response = await this.PostResponse(string.Format("scopes/{0}/tags", scopeId), new Dictionary<string, object>(), new Tag() { Name = name });
            return JsonConvert.DeserializeObject<Tag>(response);
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<string> DeleteTag(string scopeId, Tag tag)
        {
            return await this.DeleteResponse(string.Format("scopes/{0}/tags/{1}", scopeId, tag.Id));
        }

        /// <summary>
        /// Get tag by name or id
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Tag> GetTag(string scopeId, string nameOrId)
        {
            string response = await this.GetResponse(string.Format("scopes/{0}/tags/{1}", scopeId, nameOrId));
            return JsonConvert.DeserializeObject<Tag>(response);
        }

        /// <summary>
        /// Get tag list
        /// </summary>
        /// <param name="scopeId">e.g. project id</param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Tag>> GetTagList(string scopeId, bool includeInactive = false)
        {
            string response = await this.GetResponse(string.Format("scopes/{0}/tags", scopeId), new Dictionary<string, object>()
            {
                {
                        "includeInactive",
                        includeInactive
                }
            });
            return JsonConvert.DeserializeObject<JsonCollection<Tag>>(response);
        }

        /// <summary>
        /// Update existing tag
        /// </summary>
        /// <param name="scopeId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<Tag> UpdateTag(string scopeId, Tag tag)
        {
            string response = await this.PatchResponse(string.Format("scopes/{0}/tags/{1}", scopeId, tag.Id), tag, null, JsonMediaType);
            JsonConvert.PopulateObject(response, tag);
            return tag;
        }
    }
}