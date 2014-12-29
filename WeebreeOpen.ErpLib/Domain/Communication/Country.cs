namespace WeebreeOpen.ErpLib.Domain.Communication
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Country

    /// <summary>
    /// The Country Tab defines any country in which you do business. Values entered here are referenced in location records for Business Partners.
    /// </summary>
    public class Country : BaseEntity
    {
        public int CountryPkId { get; set; }

        /// <summary>
        /// A more descriptive identifier (that does need to be unique) of a record/document that is used as a default search option along with the search key (that is unique and mostly shorter). It is up to 60 characters in length.
        /// </summary>
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// For details - http://www.din.de/gremien/nas/nabd/iso3166ma/codlstp1.html or - http://www.unece.org/trade/rec/rec03en.htm
        /// </summary>
        public string IsoCountryCode { get; set; }

        /// <summary>
        /// The Country has Region checkbox is selected if the Country being defined is divided into regions. If this checkbox is selected, the Region Tab is accessible.
        /// </summary>
        public bool HasRegions { get; set; }

        public string PhoneNumberFormat { get; set; }

        /// <summary>
        /// The Address Print format defines the format to be used when this address prints.
        /// The following notations are used: @C@=City @P@=Postal @A@=PostalAdd @R@=Region
        /// </summary>
        public string AddressPrintFormat { get; set; }

        /// <summary>
        /// The Additional Postal Code checkbox indicates if this address uses an additional Postal Code. If it is selected an additional field displays for entry of the additional Postal Code.
        /// </summary>
        public bool HasAdditioanlPostalCode { get; set; }
    }
}
