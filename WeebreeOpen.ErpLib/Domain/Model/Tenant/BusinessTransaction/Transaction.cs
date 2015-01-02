namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.BusinessTransaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class Transaction : BaseEntityTenant
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int TransactionPkId { get; set; }
        
        #endregion
        
        #region References
        
        public int BusinessPartySenderId { get; set; }
        
        public BusinessParty BusinessPartySender { get; set; }
        
        public int BusinessPartyReceiverId { get; set; }
        
        public BusinessParty BusinessPartyReceiver { get; set; }
        
        #endregion
        
        #region Properties
        
        public string DocumentNo { get; set; }
        
        public DateTimeOffset DateTimeIssued { get; set; }

        #endregion
    }
}