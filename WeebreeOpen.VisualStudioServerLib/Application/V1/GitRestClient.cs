namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    public class GitRestClient : RestClientVersion1, IVsoGit
    {
        public GitRestClient(string url, NetworkCredential userCredential)
            : base(url, new BasicAuthenticationFilter(userCredential))
        {
        }

        protected override string SubSystemName
        {
            get
            {
                return "git";
            }
        }

        /// <summary>
        /// Create a repository
        /// </summary>
        /// <param name="name"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<Repository> CreateRepository(string name, string projectId)
        {
            string response = await this.PostResponse("repositories", new { name = name, project = new { id = projectId } });
            return JsonConvert.DeserializeObject<Repository>(response);
        }

        /// <summary>
        /// Delete a repository
        /// </summary>
        /// <param name="repoId"></param>
        /// <returns></returns>
        public async Task<string> DeleteRepository(string repoId)
        {
            string response = await this.DeleteResponse(string.Format("repositories/{0}", repoId));
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="objectId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<string> DownloadTree(string repoId, string objectId, string fileName = null)
        {
            string response = await this.GetResponse(string.Format("repositories/{0}/trees/{1}", repoId, objectId),
                new Dictionary<string, object>()
                {
                    {
                            "fileName",
                            fileName
                    },
                    {
                            "$format",
                            "zip"
                    }
                });
            return response;
        }

        /// <summary>
        /// Get branch statistics
        /// </summary>
        /// <param name="repoId"></param>
        /// <returns></returns>
        public async Task<JsonCollection<GitBranchInfo>> GetBranchStatistics(string repoId)
        {
            string response = await this.GetResponse(string.Format("repositories/{0}/stats/branches", repoId));
            return JsonConvert.DeserializeObject<JsonCollection<GitBranchInfo>>(response);
        }

        /// <summary>
        /// A version of a branch
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="branchName"></param>
        /// <param name="type"></param>
        /// <param name="baseVersion"></param>
        /// <returns></returns>
        public async Task<GitBranchInfo> GetBranchStatistics(string repoId, string branchName, BaseVersionType? type = null, string baseVersion = null)
        {
            string response = await this.GetResponse(string.Format("repositories/{0}/stats/branches/{1}", repoId, branchName),
                new Dictionary<string, object>()
                {
                    {
                            "baseVersionType",
                            type != null ? type.Value.ToString() : null
                    },
                    {
                            "baseVersion",
                            baseVersion
                    }
                });
            return JsonConvert.DeserializeObject<GitBranchInfo>(response);
        }

        /// <summary>
        /// Get a list of references
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<JsonCollection<GitReference>> GetRefs(string repoId, string filter = null)
        {
            string response = await this.GetResponse(string.IsNullOrEmpty(filter) ? string.Format("repositories/{0}/refs", repoId) : string.Format("repositories/{0}/refs/{1}", repoId, filter));
            return JsonConvert.DeserializeObject<JsonCollection<GitReference>>(response);
        }

        /// <summary>
        /// Get a list of repositories
        /// </summary>
        /// <returns></returns>
        public async Task<JsonCollection<Repository>> GetRepositories()
        {
            string response = await this.GetResponse("repositories");
            return JsonConvert.DeserializeObject<JsonCollection<Repository>>(response);
        }

        /// <summary>
        /// Get a repository
        /// </summary>
        /// <param name="repoId"></param>
        /// <returns></returns>
        public async Task<Repository> GetRepository(string repoId)
        {
            string response = await this.GetResponse(string.Format("repositories/{0}", repoId));
            return JsonConvert.DeserializeObject<Repository>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="objectId"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public async Task<string> GetTreeMetadata(string repoId, string objectId, bool? recursive = null)
        {
            string response = await this.GetResponse(string.Format("repositories/{0}/trees/{1}", repoId, objectId));
            return response;
        }

        /// <summary>
        /// Rename a repository
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public async Task<Repository> RenameRepository(string repoId, string newName)
        {
            string response = await this.PatchResponse(string.Format("repositories/{0}", repoId), new { name = newName }, null, JsonMediaType);
            return JsonConvert.DeserializeObject<Repository>(response);
        }
    }
}