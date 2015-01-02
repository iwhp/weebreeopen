namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    public partial class Translation : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key
        
        public int TranslationPkId { get; set; }
        
        #endregion
        
        #region References
        
        public int LanguageId { get; set; }
        
        public Language Language { get; set; }
        
        #endregion
        
        #region Properties
        
        public string TableName { get; set; }
        
        public string ColumnName { get; set; }
        
        public string RowId { get; set; }
    
        public string Value { get; set; }

        #endregion
    }
}