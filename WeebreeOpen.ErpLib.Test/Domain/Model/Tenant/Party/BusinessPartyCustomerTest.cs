namespace WeebreeOpen.ErpLib.Test.Domain.Model.Tenant.Party
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;
    using WeebreeOpen.ErpLib.Domain.Validation;

    [TestClass]
    public class BusinessPartyCustomerTest
    {
        #region Constructor

        [TestMethod]
        public void BusinessPartyCustomer_Constructor_Organisation()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("Weebree Inc.");

            // Act

            // Assert
            Assert.AreEqual<string>("Weebree Inc.", sut.Name);
            Assert.AreEqual<bool>(true, sut.IsOrganisation);
            Assert.AreEqual<bool>(false, sut.IsPerson);

            Tester(sut);
        }

        [TestMethod]
        public void BusinessPartyCustomer_Constructor_Person()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("Claude", "Bell");

            // Act

            // Assert
            Assert.AreEqual<string>("Claude Bell", sut.Name);
            Assert.AreEqual<bool>(false, sut.IsOrganisation);
            Assert.AreEqual<bool>(true, sut.IsPerson);

            Tester(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BusinessPartyCustomer_Constructor_Organisation_Invalid()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("");

            // Act

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BusinessPartyCustomer_Constructor_Person_Invalid()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("", "");

            // Act

            // Assert
        }

        #endregion

        #region Properties

        [TestMethod]
        public void BusinessPartyCustomer_Change_FirstName()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("FirstName", "LastName");

            // Act
            sut.FirstName = "FirstName2";

            // Assert
            Assert.AreEqual<string>("FirstName2", sut.FirstName);
            Assert.AreEqual<string>("FirstName2 LastName", sut.Name);

            Tester(sut);
        }

        [TestMethod]
        public void BusinessPartyCustomer_Change_LastName()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("FirstName", "LastName");

            // Act
            sut.LastName = "LastName2";

            // Assert
            Assert.AreEqual<string>("LastName2", sut.LastName);
            Assert.AreEqual<string>("FirstName LastName2", sut.Name);

            Tester(sut);
        }

        [TestMethod]
        public void BusinessPartyCustomer_Change_Person_Name()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("FirstName", "LastName");

            // Act
            sut.Name = "LastName2";

            // Assert
            Assert.AreEqual<string>("LastName", sut.LastName);
            Assert.AreEqual<string>("FirstName LastName", sut.Name);

            Tester(sut);
        }

        [TestMethod]
        public void BusinessPartyCustomer_Change_Organisation_To_Person()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("Weebree Inc.");

            // Act
            sut.FirstName = "FirstName";
            sut.LastName = "LastName";

            // Assert
            Assert.AreEqual<string>("FirstName LastName", sut.Name);
            Assert.AreEqual<bool>(false, sut.IsOrganisation);
            Assert.AreEqual<bool>(true, sut.IsPerson);

            Tester(sut);
        }

        [TestMethod]
        public void BusinessPartyCustomer_Change_Person_To_Organisation()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("FirstName", "LastName");

            // Act
            sut.FirstName = null;
            sut.LastName = null;
            sut.Name = "Weebree Inc.";

            // Assert
            Assert.AreEqual<string>("Weebree Inc.", sut.Name);
            Assert.AreEqual<bool>(true, sut.IsOrganisation);
            Assert.AreEqual<bool>(false, sut.IsPerson);

            Tester(sut);
        }

        #endregion

        #region LocationAdd

        [TestMethod]
        public void BusinessPartyCustomer_LocationAdd()
        {
            // Assign
            BusinessPartyCustomer sut = new BusinessPartyCustomer("Weebree Inc.");
            Location location = new Location(sut.Name);
            sut.LocationAdd(location);

            // Act

            // Assert
            Assert.AreEqual<int>(1, sut.LocationBusinessParties.Count());
            Assert.AreEqual<string>("Weebree Inc.", sut.LocationBusinessParties.First().Location.StreetAddress.Name);

            Tester(sut);
        }

        #endregion

        #region Tester

        /// <summary>
        /// Make sure, system under test is a valid entity.
        /// </summary>
        /// <param name="sut"></param>
        private void Tester(BusinessPartyCustomer sut)
        {
            BusinessPartyCustomerValidator tester = new BusinessPartyCustomerValidator();
            var result = tester.Validate(sut, null);
            Assert.AreEqual<int>(0, result.Count());
        }

        #endregion
    }
}
