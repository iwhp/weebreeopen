namespace WeebreeOpen.ErpLib.Domain.Common
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/BusinessPartner

    /// <summary>
    /// The BusinessParty defines any entity with whom an organization transacts. The entity could be a customer or/and a vendor or/and an employee.
    /// Customer defines a Business Partner who is a customer of this organization.
    /// If the Customer check box is selected then the necessary fields will display.
    /// Vendor defines a Business Partner that is a Vendor for this Organization.
    /// If the Vendor check box is selected the necessary fields will display.
    /// Employee defines a Business Partner who is an Employee of this organization.
    /// If the Employee is also a Sales Representative then the check box should be selected.
    /// </summary>
    public class BusinessParty : BaseEntity
    {
        public int BusinessPartyPkId { get; set; }

        /// <summary>
        /// Commercial Name of the Business Partner.
        /// </summary>
        public string Name { get; set; }
    }
}
