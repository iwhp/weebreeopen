namespace WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class RuleIsOrganisationTrue : SpecificationRule
    {
        public bool IsSatisfiedBy(BusinessPartyCustomer businessPartyCustomer)
        {
            if (businessPartyCustomer.IsOrganisation)
            {
                if (!string.IsNullOrWhiteSpace(businessPartyCustomer.FirstName) || !string.IsNullOrWhiteSpace(businessPartyCustomer.LastName))
                {
                    this.ValidationResult = new ValidationResult(ErpLibResource.RuleIsOrganisationFirstNameLastNameEmpty, new string[]
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