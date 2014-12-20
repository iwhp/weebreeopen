namespace WeebreeOpen.FtpClientLib.Model
{
    using System;
    using System.Linq;

    public enum FtpServiceEventType
    {
        Error,

        DirectoryCreate,
        DirectoryDelete,

        FileDownload,
        FileUpload,
        FileDelete
    }
}
