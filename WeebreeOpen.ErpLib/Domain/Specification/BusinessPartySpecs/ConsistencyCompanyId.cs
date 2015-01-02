namespace WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs
{
    using System;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class ConsistencyCompanyId : SpecificationRule
    {
        public bool IsSatisfiedBy(BusinessParty businessCard)
        {
            //// The associated CompanyId must match StreetAddress.Location.Company.CompanyPkId.
            //if (businessCard.StreetAddress != null && businessCard.StreetAddress.Location != null
            //    && (businessCard.StreetAddress.Location.CompanyId != businessCard.CompanyId))
            //{
            //    this.ValidationResult = new ValidationResult(string.Format(LgtBcResources.SpecConsistencyCompanyId, businessCard.CompanyId), new string[] { "CompanyId" });
            //    return false;
            //}
            return true;
        }
    }
}