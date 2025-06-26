using System.IO;

namespace WeebreeOpen.SystemLib.FileWatcher;

/// <summary>
/// Contains settings that initializes the filesystem watchers created within the Watcher class.
/// </summary>
public class FileSystemWatcherExInfo
{
    //--------------------------------------------------------------------------------
    public FileSystemWatcherExInfo()
    {
        WatchPath = "";
        IncludeSubFolders = false;
        WatchForError = false;
        WatchForDisposed = false;
        ChangesFilters = NotifyFilters.Attributes;
        WatchesFilters = WatcherChangeTypes.All;
        FileFilter = "";
        BufferKBytes = 8;
        MonitorPathInterval = 0;
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