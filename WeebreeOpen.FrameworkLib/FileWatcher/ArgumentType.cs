namespace WeebreeOpen.FrameworkLib.FileWatcher
{
    using System;
    using System.Linq;

    /// <summary>
    /// This enum is used to indicate which argument type is valid in the WatcherEventArgs object.
    /// </summary>
    public enum ArgumentType
    {
        FileSystem,
        Renamed,
        Error,
        StandardEvent,
        PathAvailability
    }
}