namespace WeebreeOpen.VisualStudioServerLib.Domain.V1.Common
{
    using System;
    using WeebreeOpen.VisualStudioServerLib.Domain.V1.Enum;

    public struct ChangesetSearchFilter
    {
        public string Author;
        public DateTime? FromDate;
        public int? FromId;
        public string ItemPath;
        public DateTime? ToDate;
        public int? ToId;
        public string Version;
        public VersionOptions? VersionOption;
        public VersionType? VersionType;
    }
}