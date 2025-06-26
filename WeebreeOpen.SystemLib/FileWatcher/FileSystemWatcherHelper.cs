using System;
using System.IO;
using System.Threading;

namespace WeebreeOpen.SystemLib.FileWatcher;

public class FileSystemWatcherHelper : FileSystemWatcher
{
    // set a reasonable maximum interval time
    public readonly int MaxInterval = 60000;

    public event PathAvailabilityHandler EventPathAvailability = delegate { };

    public string Name = "FileSystemWatcherEx";
    private bool isNetworkAvailable = true;
    private int interval = 100;
    private Thread thread = null;
    private bool run = false;

    #region Constructors

    //--------------------------------------------------------------------------------
    public FileSystemWatcherHelper()
        : base()
    {
        CreateThread();
    }

    //--------------------------------------------------------------------------------
    public FileSystemWatcherHelper(string path)
        : base(path)
    {
        CreateThread();
    }

    //--------------------------------------------------------------------------------
    public FileSystemWatcherHelper(int interval)
        : base()
    {
        this.interval = interval;
        CreateThread();
    }

    //--------------------------------------------------------------------------------
    public FileSystemWatcherHelper(string path, int interval)
        : base(path)
    {
        this.interval = interval;
        CreateThread();
    }

    //--------------------------------------------------------------------------------
    public FileSystemWatcherHelper(int interval, string name)
        : base()
    {
        this.interval = interval;
        Name = name;
        CreateThread();
    }

    //--------------------------------------------------------------------------------
    public FileSystemWatcherHelper(string path, int interval, string name)
        : base(path)
    {
        this.interval = interval;
        Name = name;
        CreateThread();
    }

    #endregion Constructors

    #region Helper Methods

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Creates the thread if the interval is greater than 0 milliseconds 
    /// </summary>
    private void CreateThread()
    {
        // Normalize  the interval
        interval = Math.Max(0, Math.Min(interval, MaxInterval));
        // If the interval is 0, this indicates we don't want to monitor the path 
        // for availability.
        if (interval > 0)
        {
            thread = new Thread(new ThreadStart(MonitorFolderAvailability));
            thread.Name = Name;
            thread.IsBackground = true;
        }
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Attempts to start the monitoring thread
    /// </summary>
    public void StartFolderMonitor()
    {
        run = true;
        if (thread != null)
        {
            thread.Start();
        }
    }

    //--------------------------------------------------------------------------------
    /// <summary>
    /// Attempts to start the monitoring thread
    /// </summary>
    public void StopFolderMonitor()
    {
        run = false;
    }

    #endregion Helper Methods

    /// <summary>
    /// The thread method. It sits and spins making sure the folder exists
    /// </summary>
    public void MonitorFolderAvailability()
    {
        while (run)
        {
            if (isNetworkAvailable)
            {
                if (!Directory.Exists(base.Path))
                {
                    isNetworkAvailable = false;
                    RaiseEventNetworkPathAvailablity();
                }
            }
            else
            {
                if (Directory.Exists(base.Path))
                {
                    isNetworkAvailable = true;
                    RaiseEventNetworkPathAvailablity();
                }
            }
            Thread.Sleep(interval);
        }
    }

    private void RaiseEventNetworkPathAvailablity()
    {
        EventPathAvailability(this, new PathAvailabilityEventArgs(isNetworkAvailable));
    }
}