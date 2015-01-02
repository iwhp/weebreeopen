namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Party
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Base;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Location
    /// </summary>
    public partial class Location : BaseEntityTenant
    {
        #region Constructors

        protected Location()
        {
            this.StreetAddress = new StreetAddress();
        }

        public Location(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw (new ArgumentNullException("name"));
            }

            this.StreetAddress = new StreetAddress();
            this.StreetAddress.Name = name;
        }

        #endregion

        #region Key

        [Key]
        public int LocationPkId { get; set; }

        #endregion

        #region Compound

        public StreetAddress StreetAddress { get; set; }

        #endregion
    }
}