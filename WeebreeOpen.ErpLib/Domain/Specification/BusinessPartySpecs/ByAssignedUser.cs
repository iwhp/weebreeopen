namespace WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class ByAssignedUser : Specification<BusinessParty>
    {
        public override IQueryable<BusinessParty> SatisfyingElementsFrom(IQueryable<BusinessParty> candidates)
        {
            return candidates.Where(x => x.FirstName != "");
        }
    }
}