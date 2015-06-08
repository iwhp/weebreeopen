namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System;
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoBuild
    {
        Task<string> AddBuildQuality(string projectName, string quality);

        Task<string> CancelBuildRequest(string projectName, int requestId);

        Task<string> DeleteBuild(string projectName, int buildId);

        Task<string> DeleteBuildQuality(string projectName, string quality);

        Task<Build> GetBuild(string projectName, int buildId, params BuildDetails[] details);

        Task<BuildDefinition> GetBuildDefinition(string projectName, int definitionId);

        Task<JsonCollection<BuildDefinition>> GetBuildDefinitions(string projectName);

        Task<JsonCollection<string>> GetBuildQualities(string projectName);

        Task<BuildQueue> GetBuildQueue(int queueId);

        Task<JsonCollection<BuildQueue>> GetBuildQueues();

        Task<JsonCollection<BuildRequest>> GetBuildRequests(string projectName, string requestedFor = null,
            int? definitionId = null, int? queueId = null, int? maxCompletedAge = null,
            BuildStatus? status = null, int? top = null, int? skip = null);

        Task<JsonCollection<Build>> GetBuilds(string projectName, string requestedFor = null,
            int? definitionId = null, DateTime? minFinishTime = null, string quality = null, BuildStatus? status = null, int? top = null, int? skip = null);

        Task<BuildRequest> RequestBuild(string projectName, int buildDefinitionId, BuildReason reason, BuildPriority priority, int? queueId = null);

        Task<Build> UpdateBuild(string projectName, int buildId, BuildStatus? status = null, string quality = null, bool? retainIndefinitely = null);

        Task<string> UpdateBuildRequest(string projectName, int requestId, BuildStatus newStatus);
    }
}