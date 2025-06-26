using System;

namespace WeebreeOpen.SystemLib.FileWatcher;

public class PathAvailabilityEventArgs : EventArgs
{
    public PathAvailabilityEventArgs(bool available)
    {
        PathIsAvailable = available;
    }

    public bool PathIsAvailable { get; set; }
}