namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Currency
    /// </summary>
    public partial class Currency : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int CurrencyPkId { get; set; }
        
        #endregion
        
        #region References
        
        #endregion
        
        #region Properties
        
        public string Name { get; set; }
        
        /// <summary>
        /// For details - http://www.unece.org/trade/rec/rec09en.htm
        /// </summary>
        public string IsoCode3 { get; set; }
        
        public string Symbol { get; set; }
    
        public int Precision { get; set; }

        #endregion
    }
}