using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WeebreeOpen.SystemLib.FileWatcher;

/// <summary>
/// This is the main class (and the one you'll use directly). Create an instance of
/// the class (passing in a WatcherInfo object for intialization), and then attach
/// event handlers to this object.  One or more watchers will be created to handle
/// the various events and filters, and will marshal these evnts into a single set
/// from which you can gather info.
/// </summary>
public class FileSystemWatcherEx : IDisposable
{
    #region Data Members

    private bool disposed = false;
    private readonly FileSystemWatcherExInfo watcherInfo = null;
    private readonly List<FileSystemWatcherHelper> watchers = new();

    #endregion Data Members

    #region Event Definitions

    public event FileSystemWatcherExEventHandler EventChangedAttribute = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedCreationTime = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedDirectoryName = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedFileName = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedLastAccess = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedLastWrite = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedSecurity = delegate { };
    public event FileSystemWatcherExEventHandler EventChangedSize = delegate { };
    public event FileSystemWatcherExEventHandler EventCreated = delegate { };
    public event FileSystemWatcherExEventHandler EventDeleted = delegate { };
    public event FileSystemWatcherExEventHandler EventRenamed = delegate { };
    public event FileSystemWatcherExEventHandler EventError = delegate { };
    public event FileSystemWatcherExEventHandler EventDisposed = delegate { };
    public event FileSystemWatcherExEventHandler EventPathAvailability = delegate { };

    #endregion Event Definitions

    #region Constructors

    //--------------------------------------------------------------------------------
    public FileSystemWatcherEx(FileSystemWatcherExInfo info)
    {
        if (info == null)
        {
            throw new Exception("WatcherInfo object cannot be null");
        }
        watcherInfo = info;

        Initialize();
    }

    #endregion Constructors

    #region Dispose Methods

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Disposes all of the FileSystemWatcher objects, and disposes this object.
    /// </summary>
    public void Dispose()
    {
        Debug.WriteLine("WatcherEx.Dispose()");
        if (!disposed)
        {
            DisposeWatchers();
            disposed = true;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Disposes of all of our watchers (called from Dispose, or as a result of 
    /// loosing access to a folder)
    /// </summary>
    public void DisposeWatchers()
    {
        Debug.WriteLine("WatcherEx.DisposeWatchers()");
        for (int i = 0; i < watchers.Count; i++)
        {
            watchers[i].Dispose();
        }
        watchers.Clear();
    }

    #endregion Dispose Methods

    #region Helper Methods

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Determines if the specified NotifyFilter item has been specified to be 
    /// handled by this object.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public bool HandleNotifyFilter(NotifyFilters filter)
    {
        return (((NotifyFilters)(watcherInfo.ChangesFilters & filter)) == filter);
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Determines if the specified WatcherChangeType item has been specified to be 
    /// handled by this object.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public bool HandleWatchesFilter(WatcherChangeTypes filter)
    {
        return (((WatcherChangeTypes)(watcherInfo.WatchesFilters & filter)) == filter);
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Initializes this oibject by creating all of the required internal 
    /// FileSystemWatcher objects necessary to mointor the folder/file for the 
    /// desired changes
    /// </summary>
    private void Initialize()
    {
        Debug.WriteLine("WatcherEx.Initialize()");
        // the buffer can be from 4 to 64 kbytes.  Default is 8
        watcherInfo.BufferKBytes = Math.Max(4, Math.Min(watcherInfo.BufferKBytes, 64));

        // create the main watcher (handles create/delete, rename, error, and dispose)
        // can't pass a null enum type, so we just pass ta dummy one on the first call
        CreateWatcher(false, watcherInfo.ChangesFilters);
        // create a change watcher for each NotifyFilter item
        CreateWatcher(true, NotifyFilters.Attributes);
        CreateWatcher(true, NotifyFilters.CreationTime);
        CreateWatcher(true, NotifyFilters.DirectoryName);
        CreateWatcher(true, NotifyFilters.FileName);
        CreateWatcher(true, NotifyFilters.LastAccess);
        CreateWatcher(true, NotifyFilters.LastWrite);
        CreateWatcher(true, NotifyFilters.Security);
        CreateWatcher(true, NotifyFilters.Size);

        Debug.WriteLine(string.Format("WatcherEx.Initialize() - {0} watchers created", watchers.Count));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Actually creates the necessary FileSystemWatcher objects, depending oin which 
    /// notify filters and change types the user specified.
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="filter"></param>
    private void CreateWatcher(bool changedWatcher, NotifyFilters filter)
    {
        Debug.WriteLine(string.Format("WatcherEx.CreateWatcher({0}, {1})", changedWatcher.ToString(), filter.ToString()));

        FileSystemWatcherHelper watcher = null;
        int bufferSize = (int)watcherInfo.BufferKBytes * 1024;
        // Each "Change" filter gets its own watcher so we can determine *what* 
        // actually changed. This will allow us to react only to the change events 
        // that we actually want.  The reason I do this is because some programs 
        // fire TWO events for  certain changes. For example, Notepad sends two 
        // events when a file is created. One for CreationTime, and one for 
        // Attributes.
        if (changedWatcher)
        {
            // if we're not handling the currently specified filter, get out
            if (HandleNotifyFilter(filter))
            {
                watcher = new FileSystemWatcherHelper(watcherInfo.WatchPath);
                watcher.IncludeSubdirectories = watcherInfo.IncludeSubFolders;
                watcher.Filter = watcherInfo.FileFilter;
                watcher.NotifyFilter = filter;
                watcher.InternalBufferSize = bufferSize;
                switch (filter)
                {
                    case NotifyFilters.Attributes:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedAttribute);
                        break;
                    case NotifyFilters.CreationTime:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedCreationTime);
                        break;
                    case NotifyFilters.DirectoryName:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedDirectoryName);
                        break;
                    case NotifyFilters.FileName:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedFileName);
                        break;
                    case NotifyFilters.LastAccess:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedLastAccess);
                        break;
                    case NotifyFilters.LastWrite:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedLastWrite);
                        break;
                    case NotifyFilters.Security:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedSecurity);
                        break;
                    case NotifyFilters.Size:
                        watcher.Changed += new FileSystemEventHandler(watcher_ChangedSize);
                        break;
                }
            }
        }
        // All other FileSystemWatcher events are handled through a single "main" 
        // watcher.
        else
        {
            if (HandleWatchesFilter(WatcherChangeTypes.Created) ||
                HandleWatchesFilter(WatcherChangeTypes.Deleted) ||
                HandleWatchesFilter(WatcherChangeTypes.Renamed) ||
                watcherInfo.WatchForError ||
                watcherInfo.WatchForDisposed)
            {
                watcher = new FileSystemWatcherHelper(watcherInfo.WatchPath, watcherInfo.MonitorPathInterval);
                watcher.IncludeSubdirectories = watcherInfo.IncludeSubFolders;
                watcher.Filter = watcherInfo.FileFilter;
                watcher.InternalBufferSize = bufferSize;
            }

            if (HandleWatchesFilter(WatcherChangeTypes.Created))
            {
                watcher.Created += new FileSystemEventHandler(watcher_CreatedDeleted);
            }
            if (HandleWatchesFilter(WatcherChangeTypes.Deleted))
            {
                watcher.Deleted += new FileSystemEventHandler(watcher_CreatedDeleted);
            }
            if (HandleWatchesFilter(WatcherChangeTypes.Renamed))
            {
                watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            }
            if (watcherInfo.MonitorPathInterval > 0)
            {
                watcher.EventPathAvailability += new PathAvailabilityHandler(watcher_EventPathAvailability);
            }
        }
        if (watcher != null)
        {
            if (watcherInfo.WatchForError)
            {
                watcher.Error += new ErrorEventHandler(watcher_Error);
            }
            if (watcherInfo.WatchForDisposed)
            {
                watcher.Disposed += new EventHandler(watcher_Disposed);
            }
            watchers.Add(watcher);
        }
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Starts all of the internal FileSystemWatcher objects by setting their 
    /// EnableRaisingEvents property to true.
    /// </summary>
    public void Start()
    {
        Debug.WriteLine("WatcherEx.Start()");
        watchers[0].StartFolderMonitor();
        for (int i = 0; i < watchers.Count; i++)
        {
            watchers[i].EnableRaisingEvents = true;
        }
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Stops all of the internal FileSystemWatcher objects by setting their 
    /// EnableRaisingEvents property to true.
    /// </summary>
    public void Stop()
    {
        Debug.WriteLine("WatcherEx.Stop()");
        watchers[0].StopFolderMonitor();
        for (int i = 0; i < watchers.Count; i++)
        {
            watchers[i].EnableRaisingEvents = false;
        }
    }

    #endregion Helper Methods

    #region Native Watcher Events

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring attribute changes is 
    /// triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedAttribute(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed Attribute");
        EventChangedAttribute(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.Attributes));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring creation time changes is 
    /// triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedCreationTime(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed CreationTime");
        EventChangedCreationTime(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.CreationTime));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring directory name changes is 
    /// triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedDirectoryName(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed DirectoryName");
        EventChangedDirectoryName(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.DirectoryName));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring file name changes is 
    /// triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedFileName(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed FileName");
        EventChangedFileName(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.FileName));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring last access date/time 
    /// changes is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedLastAccess(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed LastAccess");
        EventChangedLastAccess(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.LastAccess));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring last write date/time 
    /// changes is triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedLastWrite(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed LastWrite");
        EventChangedLastWrite(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.LastWrite));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring security changes is 
    /// triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedSecurity(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed Security");
        EventChangedSecurity(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.Security));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the watcher responsible for monitoring size changes is 
    /// triggered.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_ChangedSize(object sender, FileSystemEventArgs e)
    {
        Debug.WriteLine("EVENT - Changed Size");
        EventChangedSize(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.Size));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when an internal watcher is disposed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_Disposed(object sender, EventArgs e)
    {
        Debug.WriteLine("EVENT - Disposed");
        EventDisposed(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.StandardEvent));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the main watcher detects an error (the watcher that detected the 
    /// error is part of the event's arguments object)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_Error(object sender, ErrorEventArgs e)
    {
        Debug.WriteLine("EVENT - Error");
        EventError(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.Error));
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Fired when the main watcher detects a file rename.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void watcher_Renamed(object sender, RenamedEventArgs e)
    {
        Debug.WriteLine("EVENT - Renamed");
        EventRenamed(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.Renamed));
    }

    //--------------------------------------------------------------------------------
    private void watcher_CreatedDeleted(object sender, FileSystemEventArgs e)
    {
        switch (e.ChangeType)
        {
            case WatcherChangeTypes.Created:
                Debug.WriteLine("EVENT - Created");
                EventCreated(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem));
                break;
            case WatcherChangeTypes.Deleted:
                Debug.WriteLine("EVENT - Changed Deleted");
                EventDeleted(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem));
                break;
        }
    }

    //--------------------------------------------------------------------------------
    private void watcher_EventPathAvailability(object sender, PathAvailabilityEventArgs e)
    {
        Debug.WriteLine("EVENT - PathAvailability");
        EventPathAvailability(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.PathAvailability));
        if (e.PathIsAvailable)
        {
            DisposeWatchers();
            Initialize();
        }
    }

    #endregion Native Watcher Events
}