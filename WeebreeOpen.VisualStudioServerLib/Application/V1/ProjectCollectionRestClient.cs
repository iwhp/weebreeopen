namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    public class ProjectCollectionRestClient : RestClientVersion1, IVsoProjectCollection
    {
        public ProjectCollectionRestClient(string url, NetworkCredential userCredential)
            : base(url, new BasicAuthenticationFilter(userCredential))
        {
        }

        protected override string SubSystemName
        {
            get
            {
                return "projectcollections";
            }
        }

        /// <summary>
        /// Get team project collection by name
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="includecapabilities"></param>
        /// <returns></returns>
        public async Task<TeamProjectCollection> GetProjectCollection(string projectCollectionName)
        {
            string response = await this.GetResponse(projectCollectionName);
            return JsonConvert.DeserializeObject<TeamProjectCollection>(response);
        }

        /// <summary>
        /// Get team project collection list
        /// </summary>
        /// <param name="stateFilter"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<TeamProjectCollection>> GetProjectCollections(int? top = null, int? skip = null)
        {
            string response = await this.GetResponse(string.Empty,
                new Dictionary<string, object>()
                {
                    {
                            "$top",
                            top
                    },
                    {
                            "$skip",
                            skip
                    }
                });
            return JsonConvert.DeserializeObject<JsonCollection<TeamProjectCollection>>(response);
        }
    }
}