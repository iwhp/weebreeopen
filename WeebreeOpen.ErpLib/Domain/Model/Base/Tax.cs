namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    public partial class Tax : BaseEntityTenant
    {
        #region Constructors

        protected Tax()
        {
        }

        #endregion

        #region Key

        [Key]
        public int TaxPkId { get; set; }

        #endregion

        #region References

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion
    }
}