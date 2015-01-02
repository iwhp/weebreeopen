namespace WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class RuleIsPersonTrue : SpecificationRule
    {
        public bool IsSatisfiedBy(BusinessPartyCustomer businessPartyCustomer)
        {
            if (businessPartyCustomer.IsPerson)
            {
                if (string.IsNullOrWhiteSpace(businessPartyCustomer.FirstName) || string.IsNullOrWhiteSpace(businessPartyCustomer.LastName))
                {
                    this.ValidationResult = new ValidationResult(ErpLibResource.RuleIsOrganisationFirstNameLastNameNotEmpty, new string[]
                    {
                        "FirstName",
                        "LastName"
                    });
                    return false;
                }
            }
            return true;
        }
    }
}