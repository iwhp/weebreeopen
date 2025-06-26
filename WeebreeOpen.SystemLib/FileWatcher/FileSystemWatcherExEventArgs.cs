using System.IO;

namespace WeebreeOpen.SystemLib.FileWatcher;

/// <summary>
/// This class allows us to pass any type of watcher arguments to the calling object's
/// handler via a single object instead of having to add a lot of event handlers for
/// the various event args types.
/// </summary>
public class FileSystemWatcherExEventArgs
{
    #region Constructors

    public FileSystemWatcherExEventArgs(FileSystemWatcherHelper watcher, object arguments, ArgumentType argType, NotifyFilters filter)
    {
        Watcher = watcher;
        Arguments = arguments;
        ArgType = argType;
        Filter = filter;
    }

    public FileSystemWatcherExEventArgs(FileSystemWatcherHelper watcher, object arguments, ArgumentType argType)
    {
        Watcher = watcher;
        Arguments = arguments;
        ArgType = argType;
        Filter = NotifyFilters.Attributes;
    }

    #endregion Constructors

    #region Properties

    public FileSystemWatcherHelper Watcher { get; set; }

    public object Arguments { get; set; }

    public ArgumentType ArgType { get; set; }

    public NotifyFilters Filter { get; set; }

    #endregion Properties
}