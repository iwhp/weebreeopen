namespace WeebreeOpen.ErpLib.Test.Domain.Validation
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.ErpLib;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Domain.Validation;

    [TestClass]
    public class BusinessPartyCustomerValidatorTest
    {
        #region BusinessPartyCustomerValidator

        [TestMethod]
        public void BusinessPartyCustomerValidator_New_Organisation()
        {
            // Assign
            BusinessPartyCustomer businessPartyCustomer = new BusinessPartyCustomer("Weebree Inc.");
            BusinessPartyCustomerValidator sut = new BusinessPartyCustomerValidator();

            // Act
            var result = sut.Validate(businessPartyCustomer, null);

            // Assert
            Assert.AreEqual<int>(0, result.Count());
        }

        [TestMethod]
        public void BusinessPartyCustomerValidator_New_Person()
        {
            // Assign
            BusinessPartyCustomer businessPartyCustomer = new BusinessPartyCustomer("Claude", "Bell");
            BusinessPartyCustomerValidator sut = new BusinessPartyCustomerValidator();

            // Act
            var result = sut.Validate(businessPartyCustomer, null);

            // Assert
            Assert.AreEqual<int>(0, result.Count());
        }

        #endregion
    }
}
