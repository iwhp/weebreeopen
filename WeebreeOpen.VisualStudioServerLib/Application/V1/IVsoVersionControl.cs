namespace WeebreeOpen.VisualStudioServerLib.Application.V1
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Common;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Model;

    public interface IVsoVersionControl
    {
        Task<Branch> GetBranch(string path, bool? includeChildren = null, bool? includeParent = null, bool? includeDeleted = null);

        Task<Changeset> GetChangeset(int changesetId, bool? includeDetails = null, bool? includeWorkItems = null, int? maxChangeCount = null, int? maxCommentLength = null);

        Task<JsonCollection<VersionControlItemChange>> GetChangesetChanges(int changesetId, int? top = null, int? skip = null);

        Task<JsonCollection<Changeset>> GetChangesets(ChangesetSearchFilter? filter = null, int? top = null, int? skip = null, OrderBy? order = null, int? maxCommentLength = null);

        Task<JsonCollection<Changeset>> GetChangesets(int[] ids, int? maxCommentLength = null);

        Task<JsonCollection<WorkItemInfo>> GetChangesetWorkItems(int changesetId);

        Task<Label> GetLabel(string labelId, int? maxItemCount = null);

        Task<JsonCollection<VersionControlItem>> GetLabelledItems(string labelId, int? top = null, int? skip = null);

        Task<JsonCollection<Label>> GetLabels(string name = null, string owner = null, string itemLabelFilter = null, int? top = null, int? skip = null);

        Task<JsonCollection<Branch>> GetRootBranches(bool? includeChildren = null, bool? includeDeleted = null);

        Task<Shelveset> GetShelveset(string shelvesetId, bool? includeDetails = null, bool? includeWorkItems = null, int? maxChangeCount = null, string maxCommentLength = null);

        Task<JsonCollection<VersionControlItemChange>> GetShelvesetChanges(string shelvesetId, int? top = null, int? skip = null);

        Task<JsonCollection<Shelveset>> GetShelvesets(string owner = null, string maxCommentLength = null, int? top = null, int? skip = null);

        Task<JsonCollection<WorkItemInfo>> GetShelvesetWorkItems(string shelvesetId);

        Task<VersionControlItemVersion> GetVersionControlItem(VersionSearchFilter filter);

        Task<string> GetVersionControlItemContent(VersionSearchFilter filter);

        Task<JsonCollection<VersionControlItemVersion>> GetVersionControlItemVersions(VersionSearchFilter filter);

        Task<JsonCollection<VersionControlItemVersion>> GetVersionControlItemVersions(IList<VersionSearchFilter> filters);
    }
}