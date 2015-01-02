namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.Linq;

    public partial class RecordLog
    {
        #region Constructors
        
        #endregion
        
        #region Key
        
        public int RecordLogPkId { get; set; }
        
        #endregion
        
        #region References
        
        public RecordLogAction RecordLogAction { get; set; }
        
        #endregion
        
        #region Properties
        
        public string EntityName { get; set; }
        
        public DateTimeOffset RecordLogAt { get; set; }
    
        public string RecordLogBy { get; set; }

        #endregion
    }
}