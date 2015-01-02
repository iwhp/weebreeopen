namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/City
    /// </summary>
    public partial class City : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int CityPkId { get; set; }
        
        #endregion
        
        #region References
        
        public int CountryId { get; set; }
        
        public int RegionId { get; set; }
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// UN/Locode is a combination of a 2-character country code and a 3-character location code, e.g. BEANR is known as the city of Antwerp (ANR) which is located in Belgium (BE).
        /// See: http://www.unece.org/cefact/locode/service/main.htm
        /// </summary>
        public string Abbreviation { get; set; }
        
        public string Name { get; set; }
        
        public string PhoneAreaCode { get; set; }
    
        public string PostalCode { get; set; }

        #endregion
    }
}