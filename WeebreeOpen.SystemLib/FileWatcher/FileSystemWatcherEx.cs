namespace WeebreeOpen.SystemLib.FileWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using WeebreeOpen.SystemLib.FileWatcher;

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
        private readonly List<FileSystemWatcherHelper> watchers = new List<FileSystemWatcherHelper>();

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
            this.watcherInfo = info;

            this.Initialize();
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
            if (!this.disposed)
            {
                this.DisposeWatchers();
                this.disposed = true;
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
            for (int i = 0; i < this.watchers.Count; i++)
            {
                this.watchers[i].Dispose();
            }
            this.watchers.Clear();
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
            return (((NotifyFilters)(this.watcherInfo.ChangesFilters & filter)) == filter);
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
            return (((WatcherChangeTypes)(this.watcherInfo.WatchesFilters & filter)) == filter);
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
            this.watcherInfo.BufferKBytes = Math.Max(4, Math.Min(this.watcherInfo.BufferKBytes, 64));

            // create the main watcher (handles create/delete, rename, error, and dispose)
            // can't pass a null enum type, so we just pass ta dummy one on the first call
            this.CreateWatcher(false, this.watcherInfo.ChangesFilters);
            // create a change watcher for each NotifyFilter item
            this.CreateWatcher(true, NotifyFilters.Attributes);
            this.CreateWatcher(true, NotifyFilters.CreationTime);
            this.CreateWatcher(true, NotifyFilters.DirectoryName);
            this.CreateWatcher(true, NotifyFilters.FileName);
            this.CreateWatcher(true, NotifyFilters.LastAccess);
            this.CreateWatcher(true, NotifyFilters.LastWrite);
            this.CreateWatcher(true, NotifyFilters.Security);
            this.CreateWatcher(true, NotifyFilters.Size);

            Debug.WriteLine(string.Format("WatcherEx.Initialize() - {0} watchers created", this.watchers.Count));
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
            int bufferSize = (int)this.watcherInfo.BufferKBytes * 1024;
            // Each "Change" filter gets its own watcher so we can determine *what* 
            // actually changed. This will allow us to react only to the change events 
            // that we actually want.  The reason I do this is because some programs 
            // fire TWO events for  certain changes. For example, Notepad sends two 
            // events when a file is created. One for CreationTime, and one for 
            // Attributes.
            if (changedWatcher)
            {
                // if we're not handling the currently specified filter, get out
                if (this.HandleNotifyFilter(filter))
                {
                    watcher = new FileSystemWatcherHelper(this.watcherInfo.WatchPath);
                    watcher.IncludeSubdirectories = this.watcherInfo.IncludeSubFolders;
                    watcher.Filter = this.watcherInfo.FileFilter;
                    watcher.NotifyFilter = filter;
                    watcher.InternalBufferSize = bufferSize;
                    switch (filter)
                    {
                        case NotifyFilters.Attributes:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedAttribute);
                            break;
                        case NotifyFilters.CreationTime:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedCreationTime);
                            break;
                        case NotifyFilters.DirectoryName:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedDirectoryName);
                            break;
                        case NotifyFilters.FileName:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedFileName);
                            break;
                        case NotifyFilters.LastAccess:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedLastAccess);
                            break;
                        case NotifyFilters.LastWrite:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedLastWrite);
                            break;
                        case NotifyFilters.Security:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedSecurity);
                            break;
                        case NotifyFilters.Size:
                            watcher.Changed += new FileSystemEventHandler(this.watcher_ChangedSize);
                            break;
                    }
                }
            }
            // All other FileSystemWatcher events are handled through a single "main" 
            // watcher.
            else
            {
                if (this.HandleWatchesFilter(WatcherChangeTypes.Created) ||
                    this.HandleWatchesFilter(WatcherChangeTypes.Deleted) ||
                    this.HandleWatchesFilter(WatcherChangeTypes.Renamed) ||
                    this.watcherInfo.WatchForError ||
                    this.watcherInfo.WatchForDisposed)
                {
                    watcher = new FileSystemWatcherHelper(this.watcherInfo.WatchPath, this.watcherInfo.MonitorPathInterval);
                    watcher.IncludeSubdirectories = this.watcherInfo.IncludeSubFolders;
                    watcher.Filter = this.watcherInfo.FileFilter;
                    watcher.InternalBufferSize = bufferSize;
                }

                if (this.HandleWatchesFilter(WatcherChangeTypes.Created))
                {
                    watcher.Created += new FileSystemEventHandler(this.watcher_CreatedDeleted);
                }
                if (this.HandleWatchesFilter(WatcherChangeTypes.Deleted))
                {
                    watcher.Deleted += new FileSystemEventHandler(this.watcher_CreatedDeleted);
                }
                if (this.HandleWatchesFilter(WatcherChangeTypes.Renamed))
                {
                    watcher.Renamed += new RenamedEventHandler(this.watcher_Renamed);
                }
                if (this.watcherInfo.MonitorPathInterval > 0)
                {
                    watcher.EventPathAvailability += new PathAvailabilityHandler(this.watcher_EventPathAvailability);
                }
            }
            if (watcher != null)
            {
                if (this.watcherInfo.WatchForError)
                {
                    watcher.Error += new ErrorEventHandler(this.watcher_Error);
                }
                if (this.watcherInfo.WatchForDisposed)
                {
                    watcher.Disposed += new EventHandler(this.watcher_Disposed);
                }
                this.watchers.Add(watcher);
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
            this.watchers[0].StartFolderMonitor();
            for (int i = 0; i < this.watchers.Count; i++)
            {
                this.watchers[i].EnableRaisingEvents = true;
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
            this.watchers[0].StopFolderMonitor();
            for (int i = 0; i < this.watchers.Count; i++)
            {
                this.watchers[i].EnableRaisingEvents = false;
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
            this.EventChangedAttribute(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.Attributes));
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
            this.EventChangedCreationTime(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.CreationTime));
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
            this.EventChangedDirectoryName(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.DirectoryName));
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
            this.EventChangedFileName(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.FileName));
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
            this.EventChangedLastAccess(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.LastAccess));
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
            this.EventChangedLastWrite(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.LastWrite));
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
            this.EventChangedSecurity(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.Security));
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
            this.EventChangedSize(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem, NotifyFilters.Size));
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
            this.EventDisposed(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.StandardEvent));
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
            this.EventError(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.Error));
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
            this.EventRenamed(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.Renamed));
        }

        //--------------------------------------------------------------------------------
        private void watcher_CreatedDeleted(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    Debug.WriteLine("EVENT - Created");
                    this.EventCreated(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem));
                    break;
                case WatcherChangeTypes.Deleted:
                    Debug.WriteLine("EVENT - Changed Deleted");
                    this.EventDeleted(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.FileSystem));
                    break;
            }
        }

        //--------------------------------------------------------------------------------
        private void watcher_EventPathAvailability(object sender, PathAvailablitiyEventArgs e)
        {
            Debug.WriteLine("EVENT - PathAvailability");
            this.EventPathAvailability(this, new FileSystemWatcherExEventArgs(sender as FileSystemWatcherHelper, e, ArgumentType.PathAvailability));
            if (e.PathIsAvailable)
            {
                this.DisposeWatchers();
                this.Initialize();
            }
        }

        #endregion Native Watcher Events
    }
}