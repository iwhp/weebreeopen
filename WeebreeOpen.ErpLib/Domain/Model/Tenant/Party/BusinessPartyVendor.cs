namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Party
{
    using System;
    using System.Linq;

    public partial class BusinessPartyVendor : BusinessParty
    {
        #region Constructors

        protected BusinessPartyVendor()
        {
        }

        public BusinessPartyVendor(string name)
            : base(name)
        {
        }

        public BusinessPartyVendor(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        #endregion
    }
}