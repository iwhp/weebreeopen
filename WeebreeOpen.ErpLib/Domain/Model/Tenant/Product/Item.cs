namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Product
    /// </summary>
    public partial class Item : BaseEntityTenant
    {
        #region Constructors

        protected Item()
        {
        }

        public Item(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw (new ArgumentNullException("name"));
            }

            this.Name = name;
        }

        #endregion

        #region Key

        [Key]
        public int ItemPkId { get; set; }

        #endregion

        #region References

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

        #region Properties Purchages

        public decimal PurchagesUnitPrice { get; set; }

        public string PurchagesUnitOfMeasure { get; set; }

        #endregion

        #region Properties Sales

        public decimal SalesUnitPrice { get; set; }

        public string SalesUnitOfMeasure { get; set; }

        #endregion
    }
}