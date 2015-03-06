namespace WeebreeOpen.FtpClientLib.Model
{
    using System;
    using System.Linq;

    public enum FtpServiceEventType
    {
        Information,
        Error,

        DirectoryCreate,
        DirectoryDelete,

        FileDownload,
        FileUpload,
        FileDelete
    }
}
