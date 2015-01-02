namespace WeebreeOpen.ErpLib.Test.Domain.Model.Tenant.Party
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Domain.Validation;

    [TestClass]
    public class LocationBusinessPartyTest
    {
        #region Constructor

        [TestMethod]
        public void LocationBusinessParty_Constructor()
        {
            // Assign
            LocationBusinessParty sut = new LocationBusinessParty(new Location("Weebree Inc."), new BusinessParty("Weebree Inc."));

            // Act

            // Assert
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LocationBusinessParty_Constructor_Invalid()
        {
            // Assign
            LocationBusinessParty sut = new LocationBusinessParty(null, null);

            // Act

            // Assert
        }

        #endregion
    }
}
