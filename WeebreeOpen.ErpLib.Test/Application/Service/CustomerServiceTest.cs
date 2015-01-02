namespace WeebreeOpen.ErpLib.Test.Application.Service
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.ErpLib.Application.Service;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;

    [TestClass]
    public class CustomerServiceTest
    {
        #region CustomerService_CustomerNew

        [TestMethod]
        public void CustomerService_CustomerNew()
        {
            // Assign
            CustomerService sut = new CustomerService();
            BusinessPartyCustomer businessPartyCustomer = new BusinessPartyCustomer("Weebree Inc.");

            // Act
            BusinessPartyCustomer result = sut.CustomerNew(businessPartyCustomer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.BusinessPartyPkId);
        }

        #endregion
    }
}
