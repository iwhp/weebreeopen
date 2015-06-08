namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoProjectCollection
    {
        Task<TeamProjectCollection> GetProjectCollection(string projectCollectionName);

        Task<JsonCollection<TeamProjectCollection>> GetProjectCollections(int? top = null, int? skip = null);
    }
}