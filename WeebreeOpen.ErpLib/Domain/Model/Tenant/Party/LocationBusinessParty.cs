namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Party
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/BusinessPartnerLocation
    /// </summary>
    public partial class LocationBusinessParty : BaseEntityTenant
    {
        #region Constructors

        public LocationBusinessParty(Location location, BusinessParty businessParty)
        {
            if (location == null)
            {
                throw (new ArgumentNullException("location"));
            }
            if (businessParty == null)
            {
                throw (new ArgumentNullException("businessParty"));
            }

            this.Location = location;
            this.BusinessParty = businessParty;
        }

        #endregion

        #region Key

        [Key]
        public int LocationBusinessPartyPkId { get; set; }

        #endregion

        #region References

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public int BusinessPartyId { get; set; }

        public virtual BusinessParty BusinessParty { get; set; }

        #endregion

        #region Properties

        #endregion
    }
}