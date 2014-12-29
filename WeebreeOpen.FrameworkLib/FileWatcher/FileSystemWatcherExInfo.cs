namespace WeebreeOpen.FrameworkLib.FileWatcher
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Contains settings that initializes the filesystem watchers created within the Watcher class.
    /// </summary>
    public class FileSystemWatcherExInfo
    {
        //--------------------------------------------------------------------------------
        public FileSystemWatcherExInfo()
        {
            this.WatchPath = "";
            this.IncludeSubFolders = false;
            this.WatchForError = false;
            this.WatchForDisposed = false;
            this.ChangesFilters = NotifyFilters.Attributes;
            this.WatchesFilters = WatcherChangeTypes.All;
            this.FileFilter = "";
            this.BufferKBytes = 8;
            this.MonitorPathInterval = 0;
        }

        public uint BufferKBytes { get; set; }

        public System.IO.NotifyFilters ChangesFilters { get; set; }

        public string FileFilter { get; set; }

        public bool IncludeSubFolders { get; set; }

        // only applicable if using WatcherEx class
        public int MonitorPathInterval { get; set; }

        public WatcherChangeTypes WatchesFilters { get; set; }

        public bool WatchForDisposed { get; set; }

        public bool WatchForError { get; set; }

        public string WatchPath { get; set; }
    }
}