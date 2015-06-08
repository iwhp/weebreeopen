namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoProject
    {
        Task<ProjectTeam> GetProjectTeam(string projectNameOrId, string teamNameOrId);

        Task<JsonCollection<ProjectTeam>> GetProjectTeams(string projectNameOrId, int? top = null, int? skip = null);

        Task<JsonCollection<UserIdentity>> GetTeamMembers(string projectNameOrId, string teamNameOrId, int? top = null, int? skip = null);

        Task<TeamProject> GetTeamProject(string projectNameOrId, bool? includecapabilities = null);

        Task<JsonCollection<TeamProject>> GetTeamProjects(ProjectState? stateFilter = null, int? top = null, int? skip = null);

        Task<TeamProject> UpdateTeamProject(TeamProject project);
    }
}