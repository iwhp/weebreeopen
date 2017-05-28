using System;
using System.Linq;

namespace WeebreeOpen.FtpClientLib.Model
{
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
