namespace WeebreeOpen.ErpLib.Domain.Model.Base
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