namespace WeebreeOpen.ErpLib.Domain.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Domain.Specification.BusinessPartySpecs;
    using WeebreeOpen.ErpLib.Domain.Specification.LocationSpecs;
    using WeebreeOpen.ErpLib.Infractructure.MsSql;
    using WeebreeOpen.ErpLib.Practices;

    public partial class BusinessPartyCustomerValidator : Validator<BusinessPartyCustomer>
    {
        /// <summary>
        /// Standard validation.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(BusinessPartyCustomer businessPartyCustomer, ErpDbContext lgtBcDbContext)
        {
            #region Validate Rule*
            
            {
                RuleNameNotEmpty rule = new RuleNameNotEmpty();
                if (!rule.IsSatisfiedBy(businessPartyCustomer))
                {
                    yield return rule.ValidationResult;
                }
            }
            {
                RuleIsOrganisationOrIsPerson rule = new RuleIsOrganisationOrIsPerson();
                if (!rule.IsSatisfiedBy(businessPartyCustomer))
                {
                    yield return rule.ValidationResult;
                }
            }
            {
                RuleIsOrganisationTrue rule = new RuleIsOrganisationTrue();
                if (!rule.IsSatisfiedBy(businessPartyCustomer))
                {
                    yield return rule.ValidationResult;
                }
            }
            {
                RuleIsPersonTrue rule = new RuleIsPersonTrue();
                if (!rule.IsSatisfiedBy(businessPartyCustomer))
                {
                    yield return rule.ValidationResult;
                }
            }
            
            #endregion
            
            #region Validate Consistency
            
            {
                ConsistencyCompanyId rule = new ConsistencyCompanyId();
                if (!rule.IsSatisfiedBy(businessPartyCustomer))
                {
                    yield return rule.ValidationResult;
                }
            }
            //{
            //    ConsistencyLanguageId rule = new ConsistencyLanguageId();
            //    if (!rule.IsSatisfiedBy(businessCard))
            //    {
            //        yield return rule.ValidationResult;
            //    }
            //}
            //{
            //    ConsistencyLocationId rule = new ConsistencyLocationId();
            //    if (!rule.IsSatisfiedBy(businessCard))
            //    {
            //        yield return rule.ValidationResult;
            //    }
            //}
            
            #endregion
            
            #region Validate Dependency
            
            {
                DependencyCompany rule = new DependencyCompany();
                if (!rule.IsSatisfiedBy(businessPartyCustomer, lgtBcDbContext))
                {
                    yield return rule.ValidationResult;
                }
            }
            //{
            //    DependencyLanguage rule = new DependencyLanguage();
            //    if (!rule.IsSatisfiedBy(businessCard, lgtBcDbContext))
            //    {
            //        yield return rule.ValidationResult;
            //    }
            //}
            //{
            //    DependencyLocation rule = new DependencyLocation();
            //    if (!rule.IsSatisfiedBy(businessCard, lgtBcDbContext))
            //    {
            //        yield return rule.ValidationResult;
            //    }
            //}
            //{
            //    DependencyStreetAddress rule = new DependencyStreetAddress();
            //    if (!rule.IsSatisfiedBy(businessCard, lgtBcDbContext))
            //    {
            //        yield return rule.ValidationResult;
            //    }
            //}
        
            #endregion
        }
    }
}