namespace WeebreeOpen.SystemLib.FileWatcher
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;

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
            this.CreateThread();
        }

        //--------------------------------------------------------------------------------
        public FileSystemWatcherHelper(string path)
            : base(path)
        {
            this.CreateThread();
        }

        //--------------------------------------------------------------------------------
        public FileSystemWatcherHelper(int interval)
            : base()
        {
            this.interval = interval;
            this.CreateThread();
        }

        //--------------------------------------------------------------------------------
        public FileSystemWatcherHelper(string path, int interval)
            : base(path)
        {
            this.interval = interval;
            this.CreateThread();
        }

        //--------------------------------------------------------------------------------
        public FileSystemWatcherHelper(int interval, string name)
            : base()
        {
            this.interval = interval;
            this.Name = name;
            this.CreateThread();
        }

        //--------------------------------------------------------------------------------
        public FileSystemWatcherHelper(string path, int interval, string name)
            : base(path)
        {
            this.interval = interval;
            this.Name = name;
            this.CreateThread();
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
            this.interval = Math.Max(0, Math.Min(this.interval, this.MaxInterval));
            // If the interval is 0, this indicates we don't want to monitor the path 
            // for availability.
            if (this.interval > 0)
            {
                this.thread = new Thread(new ThreadStart(this.MonitorFolderAvailability));
                this.thread.Name = this.Name;
                this.thread.IsBackground = true;
            }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Attempts to start the monitoring thread
        /// </summary>
        public void StartFolderMonitor()
        {
            this.run = true;
            if (this.thread != null)
            {
                this.thread.Start();
            }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Attempts to start the monitoring thread
        /// </summary>
        public void StopFolderMonitor()
        {
            this.run = false;
        }

        #endregion Helper Methods

        /// <summary>
        /// The thread method. It sits and spins making sure the folder exists
        /// </summary>
        public void MonitorFolderAvailability()
        {
            while (this.run)
            {
                if (this.isNetworkAvailable)
                {
                    if (!Directory.Exists(base.Path))
                    {
                        this.isNetworkAvailable = false;
                        this.RaiseEventNetworkPathAvailablity();
                    }
                }
                else
                {
                    if (Directory.Exists(base.Path))
                    {
                        this.isNetworkAvailable = true;
                        this.RaiseEventNetworkPathAvailablity();
                    }
                }
                Thread.Sleep(this.interval);
            }
        }

        private void RaiseEventNetworkPathAvailablity()
        {
            this.EventPathAvailability(this, new PathAvailablitiyEventArgs(this.isNetworkAvailable));
        }
    }
}