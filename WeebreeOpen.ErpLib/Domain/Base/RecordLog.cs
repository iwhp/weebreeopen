namespace WeebreeOpen.ErpLib.Domain.Base
{
    using System;
    using System.Linq;

    public class RecordLog
    {
        public int RecordLogPkId { get; set; }

        public string EntityName { get; set; }

        public RecordLogAction RecordLogAction { get; set; }

        public string RecordLogBy { get; set; }

        public DateTimeOffset RecordLogAt { get; set; }
    }
}
