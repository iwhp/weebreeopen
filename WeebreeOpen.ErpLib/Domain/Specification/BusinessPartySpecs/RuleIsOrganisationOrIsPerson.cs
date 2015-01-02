namespace WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class RuleIsOrganisationOrIsPerson : SpecificationRule
    {
        public bool IsSatisfiedBy(BusinessPartyCustomer businessPartyCustomer)
        {
            if (businessPartyCustomer.IsOrganisation && businessPartyCustomer.IsPerson)
            {
                this.ValidationResult = new ValidationResult(ErpLibResource.RuleIsOrganisationOrIsPerson, new string[]
                {
                    "IsOrganisation",
                    "IsPerson"
                });
                return false;
            }
            if (!businessPartyCustomer.IsOrganisation && !businessPartyCustomer.IsPerson)
            {
                this.ValidationResult = new ValidationResult(ErpLibResource.RuleIsOrganisationOrIsPerson, new string[]
                {
                    "IsOrganisation",
                    "IsPerson"
                });
                return false;
            }
            return true;
        }
    }
}