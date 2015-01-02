namespace WeebreeOpen.ErpLib.Domain.Specification.LocationSpecs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Practices;

    public partial class RuleNameNotEmpty : SpecificationRule
    {
        public bool IsSatisfiedBy(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.StreetAddress.Name))
            {
                this.ValidationResult = new ValidationResult(ErpLibResource.RuleNameNotEmpty, new string[] { "Name" });
                return false;
            }
            return true;
        }
    }
}