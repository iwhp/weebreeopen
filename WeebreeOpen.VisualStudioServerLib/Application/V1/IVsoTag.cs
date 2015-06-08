namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoTag
    {
        Task<Tag> CreateTag(string scopeId, string name);

        Task<string> DeleteTag(string scopeId, Tag tag);

        Task<Tag> GetTag(string scopeId, string nameOrId);

        Task<JsonCollection<Tag>> GetTagList(string scopeId, bool includeInactive = false);

        Task<Tag> UpdateTag(string scopeId, Tag tag);
    }
}