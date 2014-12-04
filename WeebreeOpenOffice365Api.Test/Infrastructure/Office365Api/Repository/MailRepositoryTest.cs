namespace WeebreeOpen.Office365Api.Test.Infrastructure.Office365Api.Repository
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.Office365Api.Domain.Mail;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api;
    using WeebreeOpen.Office365Api.Infrastructure.Office365Api.Repository;

    [TestClass]
    public class MailRepositoryTest
    {
        private readonly Office365Context office365Context = new Office365Context("UU", "PP");

        [TestMethod]
        public void MailRepository_GetFolderInbox_OK()
        {
            MailRepository sut = new MailRepository(this.office365Context);

            Folder result = sut.GetFolderInbox();

            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.ChildFolderCount);
        }

        [TestMethod]
        public void MailRepository_GetFolders_OK()
        {
            MailRepository sut = new MailRepository(this.office365Context);

            List<Folder> result = sut.GetFolders();

            Assert.IsNotNull(result);
        }
    }
}
