namespace WeebreeOpen.ErpLib.Domain.Common
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Location

    /// <summary>
    /// The Location Tab defines the location of an Organization.
    /// </summary>
    public class Location : BaseEntity
    {
        public int LocationPkId { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalCode { get; set; }
        /// <summary>
        /// The Additional ZIP or Postal Code identifies, if appropriate, any additional Postal Code information.
        /// </summary>
        public string PostalCodeAdditional { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
    }
}
