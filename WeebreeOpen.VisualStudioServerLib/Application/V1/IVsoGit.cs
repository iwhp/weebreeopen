namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoGit
    {
        Task<Repository> CreateRepository(string name, string projectId);

        Task<string> DeleteRepository(string id);

        Task<JsonCollection<GitBranchInfo>> GetBranchStatistics(string repoId);

        Task<GitBranchInfo> GetBranchStatistics(string repoId, string branchName, BaseVersionType? type = null, string baseVersion = null);

        Task<JsonCollection<GitReference>> GetRefs(string repoId, string filter = null);

        Task<JsonCollection<Repository>> GetRepositories();

        Task<Repository> GetRepository(string id);

        Task<Repository> RenameRepository(string id, string newName);
    }
}