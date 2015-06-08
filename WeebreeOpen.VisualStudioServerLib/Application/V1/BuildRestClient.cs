namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    public class BuildRestClient : RestClientVersion1, IVsoBuild
    {
        public BuildRestClient(string url, NetworkCredential userCredential)
            : base(url, new BasicAuthenticationFilter(userCredential))
        {
        }

        protected override string SubSystemName
        {
            get
            {
                return "build";
            }
        }

        /// <summary>
        /// Add a quality
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<string> AddBuildQuality(string projectName, string quality)
        {
            string response = await this.PutResponse(string.Format("qualities/{0}", quality), content: null, projectName: projectName);
            return response;
        }

        /// <summary>
        /// Cancel a build request
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<string> CancelBuildRequest(string projectName, int requestId)
        {
            string response = await this.DeleteResponse(string.Format("requests/{0}", requestId), projectName);
            return response;
        }

        /// <summary>
        /// Delete a build
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="buildId"></param>
        /// <returns></returns>
        public async Task<string> DeleteBuild(string projectName, int buildId)
        {
            string response = await this.DeleteResponse(string.Format("builds/{0}", buildId), projectName);
            return response;
        }

        /// <summary>
        /// Remove a quality
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public async Task<string> DeleteBuildQuality(string projectName, string quality)
        {
            string response = await this.DeleteResponse(string.Format("qualities/{0}", quality), projectName);
            return response;
        }

        /// <summary>
        /// Get build details
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="buildId"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public async Task<Build> GetBuild(string projectName, int buildId, params BuildDetails[] details)
        {
            var arguments = new Dictionary<string, object>();

            if (details != null)
            {
                arguments.Add("types", details.Select(d => d.ToString()));
            }

            string response = await this.GetResponse(string.Format("builds/{0}", buildId), arguments, projectName);
            return JsonConvert.DeserializeObject<Build>(response);
        }

        /// <summary>
        /// Get a build definition
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="definitionId"></param>
        /// <returns></returns>
        public async Task<BuildDefinition> GetBuildDefinition(string projectName, int definitionId)
        {
            string response = await this.GetResponse(string.Format("definitions/{0}", definitionId), projectName);
            return JsonConvert.DeserializeObject<BuildDefinition>(response);
        }

        /// <summary>
        /// Get a list of build definitions
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<JsonCollection<BuildDefinition>> GetBuildDefinitions(string projectName)
        {
            string response = await this.GetResponse("definitions", projectName);
            return JsonConvert.DeserializeObject<JsonCollection<BuildDefinition>>(response);
        }

        /// <summary>
        /// Get a list of qualities
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public async Task<JsonCollection<string>> GetBuildQualities(string projectName)
        {
            string response = await this.GetResponse("qualities", projectName);
            return JsonConvert.DeserializeObject<JsonCollection<string>>(response);
        }

        /// <summary>
        /// Get a queue
        /// </summary>
        /// <returns></returns>
        public async Task<BuildQueue> GetBuildQueue(int queueId)
        {
            string response = await this.GetResponse(string.Format("queues/{0}", queueId));
            return JsonConvert.DeserializeObject<BuildQueue>(response);
        }

        /// <summary>
        /// Get a list of queues
        /// </summary>
        /// <returns></returns>
        public async Task<JsonCollection<BuildQueue>> GetBuildQueues()
        {
            string response = await this.GetResponse("queues");
            return JsonConvert.DeserializeObject<JsonCollection<BuildQueue>>(response);
        }

        public async Task<JsonCollection<BuildRequest>> GetBuildRequests(string projectName, string requestedFor = null,
            int? definitionId = null, int? queueId = null, int? maxCompletedAge = null,
            BuildStatus? status = null, int? top = null, int? skip = null)
        {
            string response = await this.GetResponse("requests",
                new Dictionary<string, object>()
                {
                    {
                            "requestedFor",
                            requestedFor
                    },
                    {
                            "definitionId",
                            definitionId
                    },
                    {
                            "queueId",
                            queueId
                    },
                    {
                            "maxCompletedAge",
                            maxCompletedAge
                    },
                    {
                            "status",
                            status != null ? status.Value.ToString() : null
                    },
                    {
                            "$top",
                            top
                    },
                    {
                            "$skip",
                            skip
                    }
                },
                projectName);
            return JsonConvert.DeserializeObject<JsonCollection<BuildRequest>>(response);
        }

        /// <summary>
        /// Get a list of builds
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="requestedFor"></param>
        /// <param name="definitionId"></param>
        /// <param name="minFinishTime"></param>
        /// <param name="quality"></param>
        /// <param name="status"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<Build>> GetBuilds(string projectName, string requestedFor = null,
            int? definitionId = null, DateTime? minFinishTime = null, string quality = null, BuildStatus? status = null, int? top = null, int? skip = null)
        {
            string response = await this.GetResponse("builds",
                new Dictionary<string, object>()
                {
                    {
                            "requestedFor",
                            requestedFor
                    },
                    {
                            "definitionId",
                            definitionId
                    },
                    {
                            "minFinishTime",
                            minFinishTime
                    },
                    {
                            "quality",
                            quality
                    },
                    {
                            "status",
                            status != null ? status.Value.ToString() : null
                    },
                    {
                            "$top",
                            top
                    },
                    {
                            "$skip",
                            skip
                    }
                },
                projectName);
            return JsonConvert.DeserializeObject<JsonCollection<Build>>(response);
        }

        /// <summary>
        /// Request a build
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="buildDefinitionId"></param>
        /// <param name="reason"></param>
        /// <param name="priority"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        public async Task<BuildRequest> RequestBuild(string projectName, int buildDefinitionId, BuildReason reason, BuildPriority priority, int? queueId = null)
        {
            string response = await this.PostResponse("requests",
                new { definition = new { id = buildDefinitionId }, reason = reason.ToString(), priority = priority.ToString(), queue = new { id = queueId } },
                projectName);
            return JsonConvert.DeserializeObject<BuildRequest>(response);
        }

        /// <summary>
        /// Modify a build
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="buildId"></param>
        /// <param name="status"></param>
        /// <param name="quality"></param>
        /// <param name="retainIndefinitely"></param>
        /// <returns></returns>
        public async Task<Build> UpdateBuild(string projectName, int buildId, BuildStatus? status = null, string quality = null, bool? retainIndefinitely = null)
        {
            string response = await this.PatchResponse(string.Format("builds/{0}", buildId),
                new { status = status, quality = quality, retainIndefinitely = retainIndefinitely },
                projectName,
                JsonMediaType);

            return JsonConvert.DeserializeObject<Build>(response);
        }

        /// <summary>
        /// Update the status of a request
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="requestId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public async Task<string> UpdateBuildRequest(string projectName, int requestId, BuildStatus newStatus)
        {
            string response = await this.PatchResponse(string.Format("requests/{0}", requestId), new { status = newStatus.ToString() }, projectName, JsonMediaType);
            return response;
        }
    }
}