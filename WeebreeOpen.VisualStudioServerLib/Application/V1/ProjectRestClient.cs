namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;
    using WeebreeOpen.VisualStudioServerLib.Infrastructure;

    public class ProjectRestClient : RestClientVersion1, IVsoProject
    {
        public ProjectRestClient(string url, NetworkCredential userCredential)
            : base(url, new BasicAuthenticationFilter(userCredential))
        {
        }

        protected override string SubSystemName
        {
            get
            {
                return "projects";
            }
        }

        /// <summary>
        /// Get team by name or id
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="teamNameOrId"></param>
        /// <returns></returns>
        public async Task<ProjectTeam> GetProjectTeam(string projectNameOrId, string teamNameOrId)
        {
            string response = await this.GetResponse(string.Format("{0}/teams/{1}", projectNameOrId, teamNameOrId));
            return JsonConvert.DeserializeObject<ProjectTeam>(response);
        }

        /// <summary>
        /// Get all teams within the project that the authenticated user has access to.
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<ProjectTeam>> GetProjectTeams(string projectNameOrId, int? top = null, int? skip = null)
        {
            string response = await this.GetResponse(string.Format("{0}/teams", projectNameOrId), new Dictionary<string, object>()
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
            return JsonConvert.DeserializeObject<JsonCollection<ProjectTeam>>(response);
        }

        /// <summary>
        /// Get a list of identity references for the team's members.
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="teamNameOrId"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<UserIdentity>> GetTeamMembers(string projectNameOrId, string teamNameOrId, int? top = null, int? skip = null)
        {
            string response = await this.GetResponse(string.Format("{0}/teams/{1}/members", projectNameOrId, teamNameOrId),
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
            return JsonConvert.DeserializeObject<JsonCollection<UserIdentity>>(response);
        }

        /// <summary>
        /// Get team project by name or id
        /// </summary>
        /// <param name="projectNameOrId"></param>
        /// <param name="includecapabilities"></param>
        /// <returns></returns>
        public async Task<TeamProject> GetTeamProject(string projectNameOrId, bool? includecapabilities = null)
        {
            string response = await this.GetResponse(projectNameOrId, new Dictionary<string, object>()
            {
                {
                        "includecapabilities",
                        includecapabilities
                }
            });
            return JsonConvert.DeserializeObject<TeamProject>(response);
        }

        /// <summary>
        /// Get team project list
        /// </summary>
        /// <param name="stateFilter"></param>
        /// <param name="top"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<JsonCollection<TeamProject>> GetTeamProjects(ProjectState? stateFilter = null, int? top = null, int? skip = null)
        {
            string response = await this.GetResponse(string.Empty,
                new Dictionary<string, object>()
                {
                    {
                            "$stateFilter",
                            stateFilter
                    },
                    {
                            "$top",
                            top
                    },
                    {
                            "$skip",
                            skip
                    }
                });
            return JsonConvert.DeserializeObject<JsonCollection<TeamProject>>(response);
        }

        /// <summary>
        /// Update team project description
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<TeamProject> UpdateTeamProject(TeamProject project)
        {
            string response = await this.PatchResponse(project.Id.ToString(), new { description = project.Description }, null, JsonMediaType);
            JsonConvert.PopulateObject(response, project);
            return project;
        }
    }
}