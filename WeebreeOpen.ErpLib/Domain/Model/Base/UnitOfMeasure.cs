namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/UOM
    /// </summary>
    public partial class UnitOfMeasure : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int UnitOfMeasurePkId { get; set; }
        
        #endregion
        
        #region References
        
        #endregion
        
        #region Properties
        
        public string Code { get; set; }
        
        /// <summary>
        /// The Unit of Measure Code indicates the EDI X12 Code Data Element 355 (Unit or Basis for Measurement).
        /// </summary>
        public string CodeEdi { get; set; }
        
        public string Symbol { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        /// <summary>
        /// The Standard Precision defines the number of decimal places that amounts will be rounded to for accounting transactions and documents.
        /// </summary>
        public int Precision { get; set; }
        
        /// <summary>
        /// The Costing Precision defines the number of decimal places that amounts will be rounded to when performing costing calculations.
        /// </summary>
        public int PrecisionCost { get; set; }

        #endregion
    }
}