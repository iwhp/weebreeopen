namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Region
    /// </summary>
    public partial class Region : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int RegionPkId { get; set; }
        
        #endregion
        
        #region References
        
        public int CountryId { get; set; }
        
        #endregion
        
        #region Properties
    
        public string Name { get; set; }

        #endregion
    }
}