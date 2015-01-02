namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/ADLanguage
    /// </summary>
    public partial class Language : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key
        
        public int LanguagePkId { get; set; }
        
        #endregion

        #region References

        #endregion
        
        #region Properties

        public string Name { get; set; }

        #endregion
    }
}