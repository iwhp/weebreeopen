namespace WeebreeOpen.SystemLib.FileWatcher;

/// <summary>
/// Event handlers for the watcher events we post back to the containing object.  We
/// only need one handler type because no matter what event is posted, the
/// WatcherEventArgs object contains the correct argument type (as an object).  This
/// is the event handler that the calling object will use.
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void FileSystemWatcherExEventHandler(object sender, FileSystemWatcherExEventArgs e);