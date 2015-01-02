namespace WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs
{
    using System;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Infractructure.MsSql;
    using WeebreeOpen.ErpLib.Practices;

    public partial class DependencyCompany : SpecificationRule
    {
        public bool IsSatisfiedBy(BusinessParty businessCard, ErpDbContext lgtBcDbContext)
        {
            //Company company = lgtBcDbContext.Companies.Find(businessCard.StreetAddress.Location.CompanyId);
            //if (company == null)
            //{
            //    this.ValidationResult = new ValidationResult(string.Format(LgtBcResources.SpecDependencyCompanyMissing, businessCard.Location.CompanyId), new string[] { "CompanyId" });
            //    return false;
            //}
            return true;
        }
    }
}