namespace WeebreeOpen.SystemLib.FileWatcher
{
    using System;

    public class PathAvailablitiyEventArgs : EventArgs
    {
        public PathAvailablitiyEventArgs(bool available)
        {
            this.PathIsAvailable = available;
        }

        public bool PathIsAvailable { get; set; }
    }
}