namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Party
{
    using System;
    using System.Linq;

    public partial class BusinessPartyEmployee : BusinessParty
    {
        #region Constructors

        protected BusinessPartyEmployee()
        {
        }

        public BusinessPartyEmployee(string name)
            : base(name)
        {
        }

        public BusinessPartyEmployee(string firstName, string lastName)
            : base(firstName, lastName)
        {
        }

        #endregion
    }
}