namespace WeebreeOpen.FrameworkLib.FileWatcher
{
    using System.IO;

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
            this.Watcher = watcher;
            this.Arguments = arguments;
            this.ArgType = argType;
            this.Filter = filter;
        }

        public FileSystemWatcherExEventArgs(FileSystemWatcherHelper watcher, object arguments, ArgumentType argType)
        {
            this.Watcher = watcher;
            this.Arguments = arguments;
            this.ArgType = argType;
            this.Filter = NotifyFilters.Attributes;
        }

        #endregion Constructors

        #region Properties

        public FileSystemWatcherHelper Watcher { get; set; }

        public object Arguments { get; set; }

        public ArgumentType ArgType { get; set; }

        public NotifyFilters Filter { get; set; }

        #endregion Properties
    }
}