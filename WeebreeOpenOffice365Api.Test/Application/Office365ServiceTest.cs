namespace WeebreeOpen.Office365Api.Test.Application
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.Application;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api;

    [TestClass]
    public class Office365ServiceTest
    {
        private readonly string userName = "UU";
        private readonly string password = "PP";

        [TestMethod]
        public void Office365Service_Constructor_UserName_Password_OK()
        {
            // Assign

            // Act
            Office365Service sut = new Office365Service(userName, password);

            // Assert
            Assert.IsNotNull(sut);
            Assert.AreEqual<string>(userName, sut.Office365UnitOfWork.Office365Context.UserName);
            Assert.AreEqual<string>(password, sut.Office365UnitOfWork.Office365Context.Password);
        }

        [TestMethod]
        public void Office365Service_Constructor_UnitOfWork_OK()
        {
            // Assign
            Office365UnitOfWork office365UnitOfWork = new Office365UnitOfWork(userName, password);

            // Act
            Office365Service sut = new Office365Service(office365UnitOfWork);

            // Assert
            Assert.IsNotNull(sut);
            Assert.AreEqual<string>(userName, sut.Office365UnitOfWork.Office365Context.UserName);
            Assert.AreEqual<string>(password, sut.Office365UnitOfWork.Office365Context.Password);
        }
    }
}
