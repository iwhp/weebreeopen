namespace WeebreeOpen.ErpLib.Test.Domain.Model.Tenant.Party
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Domain.Validation;

    [TestClass]
    public class LocationTest
    {
        #region Constructor

        [TestMethod]
        public void Location_Constructor()
        {
            // Assign
            Location sut = new Location("Weebree Inc.");

            // Act

            // Assert
            Assert.AreEqual<string>("Weebree Inc.", sut.StreetAddress.Name);

            Tester(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Location_Constructor_Invalid()
        {
            // Assign
            Location sut = new Location("");

            // Act

            // Assert
        }

        #endregion

        #region Tester

        /// <summary>
        /// Make sure, system under test is a valid entity.
        /// </summary>
        /// <param name="sut"></param>
        private void Tester(Location sut)
        {
            LocationValidator tester = new LocationValidator();
            var result = tester.Validate(sut, null);
            Assert.AreEqual<int>(0, result.Count());
        }

        #endregion
    }
}
