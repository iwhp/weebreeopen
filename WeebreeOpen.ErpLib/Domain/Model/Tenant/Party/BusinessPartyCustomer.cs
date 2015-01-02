namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Party
{
    using System;
    using System.Linq;

    public partial class BusinessPartyCustomer : BusinessParty
    {
        #region Constructors

        protected BusinessPartyCustomer()
        {
        }

        public BusinessPartyCustomer(string name)
            : base(name)
        {
        }

        public BusinessPartyCustomer(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        #endregion
    }
}