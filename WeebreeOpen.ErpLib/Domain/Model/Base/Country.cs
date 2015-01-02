namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Country
    /// </summary>
    public partial class Country : BaseEntity
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int CountryPkId { get; set; }
        
        #endregion
        
        #region References
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// The Additional Postal Code checkbox indicates if this address uses an additional Postal Code. If it is selected an additional field displays for entry of the additional Postal Code.
        /// </summary>
        public bool HasAdditioanlPostalCode { get; set; }
        
        /// <summary>
        /// The Country has Region checkbox is selected if the Country being defined is divided into regions.
        /// </summary>
        public bool HasRegions { get; set; }
        
        /// <summary>
        /// For details - http://www.din.de/gremien/nas/nabd/iso3166ma/codlstp1.html or - http://www.unece.org/trade/rec/rec03en.htm
        /// </summary>
        public string IsoCountryCode2 { get; set; }
        
        /// <summary>
        /// For details - http://www.din.de/gremien/nas/nabd/iso3166ma/codlstp1.html or - http://www.unece.org/trade/rec/rec03en.htm
        /// </summary>
        public string IsoCountryCode3 { get; set; }
        
        public string Name { get; set; }
        
        /// <summary>
        /// The Address Print format defines the format to be used when this address prints.
        /// The following notations are used: @COS@=CountryShort @CI@=City @PC@=PostalCode @PCA@=PostalCodeAdditional @RE@=Region
        /// </summary>
        public string AddressPrintFormat { get; set; }
    
        public string PhoneNumberFormat { get; set; }

        #endregion
    }
}