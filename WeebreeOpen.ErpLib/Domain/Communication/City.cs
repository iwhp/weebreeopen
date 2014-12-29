namespace WeebreeOpen.ErpLib.Domain.Communication
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/City

    /// <summary>
    /// The Cities Tab defines Cities within a Country or Region. Cities entered here are not referenced when entering the address.
    /// </summary>
    public class City : BaseEntity
    {
        public int CityPkId { get; set; }

        public int CountryId { get; set; }

        public int RegionId { get; set; }

        /// <summary>
        /// A more descriptive identifier (that does need to be unique) of a record/document that is used as a default search option along with the search key (that is unique and mostly shorter). It is up to 60 characters in length.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// UN/Locode is a combination of a 2-character country code and a 3-character location code, e.g. BEANR is known as the city of Antwerp (ANR) which is located in Belgium (BE).
        /// See: http://www.unece.org/cefact/locode/service/main.htm
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// The Postal Code field identifies the postal code for this entity's address.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Phone Area Code
        /// </summary>
        public string PhoneAreaCode { get; set; }
    }
}
