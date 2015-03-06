namespace WeebreeOpen.FtpClientLib.Test.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.FtpClientLib.Model;
    using WeebreeOpen.FtpClientLib.Service;
    using WeebreeOpen.FtpClientLib.Test.Test;

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
            List<FtpEntry> result = sut.GetDirectoryListing(TestSetupCleanup.FtpTestRootFolder).ToList();

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
            List<FtpEntry> result = sut.GetDirectoryListingRecursive(TestSetupCleanup.FtpTestRootFolder).ToList();

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
            bool result = sut.DownloadFile(TestSetupCleanup.FtpFileText1, TestSetupCleanup.LocalFileText1, true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_FileDownload_BinaryFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Act
            bool result = sut.DownloadFile(TestSetupCleanup.FtpFileBinary1, TestSetupCleanup.LocalFileBinary1, true);

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
            bool result = sut.DownloadFileRecursive(TestSetupCleanup.FtpTestRootFolder, TestSetupCleanup.LocalTestRootFolder, true, false);

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
            bool result = sut.UploadFile(TestSetupCleanup.TestDataFileText1, TestSetupCleanup.FtpFileText1);

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
            bool result = sut.DeleteFile(TestSetupCleanup.FtpFileText1);

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
            sut.DeleteDirectoryRecursive(TestSetupCleanup.FtpTestRootFolder);
            sut.CreateDirectory(TestSetupCleanup.FtpTestRootFolder);

            // Act
            bool result = sut.CreateDirectory(TestSetupCleanup.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Delete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestSetupCleanup.FtpTestRootFolder);
            sut.CreateDirectory(TestSetupCleanup.FtpTestRootFolder);
            sut.CreateDirectory(TestSetupCleanup.FtpDirectory1);

            // Act
            bool result = sut.DeleteDirectory(TestSetupCleanup.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Delete Directory with Files in it

        [TestMethod]
        public void FtpClientService_DeleteDirectoryWithFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestSetupCleanup.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestSetupCleanup.FtpTestRootFolder);
            sut.CreateDirectory(TestSetupCleanup.FtpTestRootFolder);

            // Act
            sut.EventMessages.Clear();
            bool result1 = sut.CreateDirectory(TestSetupCleanup.FtpDirectory1);
            bool result2 = sut.UploadFile(TestSetupCleanup.LocalFileText1, TestSetupCleanup.FtpFileText3);
            bool result3 = sut.DeleteDirectory(TestSetupCleanup.FtpDirectory1);
            bool result4 = sut.DeleteFile(TestSetupCleanup.FtpFileText3);
            bool result5 = sut.DeleteDirectory(TestSetupCleanup.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result1, "Directory created failed.");
            Assert.AreEqual<bool>(true, result2, "Update File failed.");
            Assert.AreEqual<bool>(false, result3, "Directory was deleted - this should NOT be possible since there are files in the direcory.");
            Assert.AreEqual<bool>(true, result4, "File could not be deleted.");
            Assert.AreEqual<bool>(true, result5, "Directory could not be deleted");

            // Log
            foreach (var message in sut.EventMessages)
            {
                Console.WriteLine(message.ToString());
            }
        }

        #endregion

        #region BuildHtmlMessage

        [TestMethod]
        public void FtpClientService_BuildHtmlMessage_Empty()
        {
            // Assign
            List<FtpServiceEventArgs> input = new List<FtpServiceEventArgs>();

            // Act
            string result = FtpClientService.BuildHtmlMessage(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count());
        }

        [TestMethod]
        public void FtpClientService_BuildHtmlMessage_OK_1()
        {
            // Assign
            List<FtpServiceEventArgs> input = new List<FtpServiceEventArgs>();
            input.Add(FtpServiceEventArgs.FileDownload("fileFrom", "fileTo"));

            // Act
            string result = FtpClientService.BuildHtmlMessage(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count());
        }

        #endregion
    }
}