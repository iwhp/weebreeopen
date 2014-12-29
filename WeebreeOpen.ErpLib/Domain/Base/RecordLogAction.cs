namespace WeebreeOpen.ErpLib.Domain.Base
{
    using System;
    using System.Linq;

    public enum RecordLogAction
    {
        Created,
        Modified,
        Deactivated,    // from active to inactive
        Activated,      // from inactive to active
        Deleted
    }
}
