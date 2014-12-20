namespace WeebreeOpen.FtpClientLib.Model
{
    using System;
    using System.Linq;

    public class FtpServiceEventArgs : EventArgs
    {
        #region Constructors

        private FtpServiceEventArgs() { }

        #endregion

        #region Properties

        public FtpServiceEventType Type { get; private set; }

        public Exception Exception { get; private set; }

        public string Directory { get; private set; }
        public string DirectoryFrom { get; private set; }
        public string DirectoryTo { get; private set; }

        public string File { get; private set; }
        public string FileFrom { get; private set; }
        public string FileTo { get; private set; }

        public string Message { get; private set; }

        public DateTimeOffset EventOccuredAt { get; private set; }

        #endregion

        #region Methods Error

        public static FtpServiceEventArgs Error(string message)
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.Error;
            e.Message = message;
            e.EventOccuredAt = DateTime.Now;
            return e;
        }

        public static FtpServiceEventArgs Error(string message, Exception exception)
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.Error;
            e.Message += string.IsNullOrWhiteSpace(message) ? "" : " ";
            e.Message += string.Format("Exception: {0}", exception.Message);
            e.EventOccuredAt = DateTime.Now;
            e.Exception = exception;
            return e;
        }

        #endregion

        #region Methods Directory

        public static FtpServiceEventArgs DirectoryCreate(string directory, string message = "")
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.DirectoryCreate;
            e.Message = message;
            e.EventOccuredAt = DateTime.Now;
            e.Directory = directory;
            return e;
        }

        public static FtpServiceEventArgs DirectoryDelete(string directory, string message = "")
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.DirectoryDelete;
            e.Message = message;
            e.EventOccuredAt = DateTime.Now;
            e.Directory = directory;
            return e;
        }

        #endregion

        #region Methods File

        public static FtpServiceEventArgs FileDownload(string fileFrom, string fileTo, string message = "")
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.FileDownload;
            e.Message = message;
            e.EventOccuredAt = DateTime.Now;
            e.FileFrom = fileFrom;
            e.FileTo = fileTo;
            return e;
        }

        public static FtpServiceEventArgs FileUpload(string fileFrom, string fileTo, string message = "")
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.FileUpload;
            e.Message = message;
            e.FileFrom = fileFrom;
            e.FileTo = fileTo;
            return e;
        }

        public static FtpServiceEventArgs FileDelete(string file, string message = "")
        {
            FtpServiceEventArgs e = new FtpServiceEventArgs();
            e.Type = FtpServiceEventType.FileDelete;
            e.Message = message;
            e.File = file;
            return e;
        }

        #endregion
    }
}
