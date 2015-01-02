namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract partial class SpecificationRule
    {
        public SpecificationRule()
        {
            this.ValidationResult = new ValidationResult("");
        }

        public ValidationResult ValidationResult { get; set; }
    }
}