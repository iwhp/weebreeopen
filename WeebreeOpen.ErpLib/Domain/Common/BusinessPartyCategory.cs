namespace WeebreeOpen.ErpLib.Domain.Common
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/BusinessPartnerCategory

    /// <summary>
    /// </summary>
    public class BusinessPartyCategory : BaseEntity
    {
        public int BusinessPartyCategoryPkId { get; set; }
    }
}
