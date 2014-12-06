namespace WeebreeOpen.FtpClientLib.Test.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.FtpClientLib.Model;
    using WeebreeOpen.FtpClientLib.Service;

    [TestClass]
    public class FtpClientServiceTest
    {
        #region GetDirectoryListing

        [TestMethod]
        public void FtpClientService_GetDirectoryListing()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            List<FtpEntry> result = sut.DirectoryListingGet(TestSetupCleanup.FtpTestRootFolder).ToList();

            // Log
            foreach (var ftpEntry in result)
            {
                Console.WriteLine(ftpEntry);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(4, result.Count, "We assume that there are 2 files and 2 directories on the FTP server in the root test directory.");
        }

        #endregion

        #region GetDirectoryListingRecursive

        [TestMethod]
        public void FtpClientService_GetDirectoryListingRecursive()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            List<FtpEntry> result = sut.DirectoryListingRecursiveGet(TestSetupCleanup.FtpTestRootFolder).ToList();

            // Log
            foreach (var ftpEntry in result)
            {
                Console.WriteLine(ftpEntry);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(6, result.Count, "We assume that there are 4 files or 2 directories on the FTP Server, starting from the root test directory downwards.");
        }

        #endregion

        #region FileDownload

        [TestMethod]
        public void FtpClientService_FileDownload_TextFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            bool result = sut.FileDownload(TestSetupCleanup.FtpFileText1, TestSetupCleanup.LocalFileText1, true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_FileDownload_BinaryFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            bool result = sut.FileDownload(TestSetupCleanup.FtpFileBinary1, TestSetupCleanup.LocalFileBinary1, true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileDownloadRecursive

        [TestMethod]
        public void FtpClientService_FileDownloadRecursive()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            bool result = sut.FileDownloadRecursive(TestSetupCleanup.FtpTestRootFolder, TestSetupCleanup.LocalTestRootFolder, true, false);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileUpload

        [TestMethod]
        public void FtpClientService_FileUpload()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            bool result = sut.FileUpload(TestSetupCleanup.TestDataFileText1, TestSetupCleanup.FtpFileText1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileDelete

        [TestMethod]
        public void FtpClientService_FileDelete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            bool result = sut.FileDelete(TestSetupCleanup.FtpFileText1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Directory Create / Delete

        [TestMethod]
        public void FtpClientService_Directory_Create()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);
            sut.DirectoryDeleteRecursive(TestSetupCleanup.FtpTestRootFolder);
            sut.DirectoryCreate(TestSetupCleanup.FtpTestRootFolder);

            // Act
            bool result = sut.DirectoryCreate(TestSetupCleanup.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result);

            // Reset Test Environment
            TestSetupCleanup.CreateTestEnvironment();
        }

        [TestMethod]
        public void FtpClientService_Directory_Delete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);
            sut.DirectoryDeleteRecursive(TestSetupCleanup.FtpTestRootFolder);
            sut.DirectoryCreate(TestSetupCleanup.FtpTestRootFolder);
            sut.DirectoryCreate(TestSetupCleanup.FtpDirectory1);

            // Act
            bool result = sut.DirectoryDelete(TestSetupCleanup.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result);

            // Reset Test Environment
            TestSetupCleanup.CreateTestEnvironment();
        }

        #endregion

        #region Delete Directory with Files in it

        [TestMethod]
        public void FtpClientService_DeleteDirectoryWithFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);
            sut.DirectoryDeleteRecursive(TestSetupCleanup.FtpTestRootFolder);
            sut.DirectoryCreate(TestSetupCleanup.FtpTestRootFolder);

            // Act
            bool result1 = sut.DirectoryCreate(TestSetupCleanup.FtpDirectory1);
            bool result2 = sut.FileUpload(TestSetupCleanup.LocalFileText1, TestSetupCleanup.FtpFileText3);
            bool result3 = sut.DirectoryDelete(TestSetupCleanup.FtpDirectory1);
            bool result4 = sut.FileDelete(TestSetupCleanup.FtpFileText3);
            bool result5 = sut.DirectoryDelete(TestSetupCleanup.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result1, "Directory created failed.");
            Assert.AreEqual<bool>(true, result2, "Update File failed.");
            Assert.AreEqual<bool>(false, result3, "Directory was deleted - this should NOT be possible since there are files in the direcory.");
            Assert.AreEqual<bool>(true, result4, "File could not be deleted.");
            Assert.AreEqual<bool>(true, result5, "Directory could not be deleted");

            // Reset Test Environment
            TestSetupCleanup.CreateTestEnvironment();
        }

        #endregion
    }
}