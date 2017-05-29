using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeebreeOpen.FtpClientLib.Model;
using WeebreeOpen.FtpClientLib.Service;

namespace WeebreeOpen.FtpClientLib.Test.Service
{
    [TestClass]
    public class FtpClientServiceTest : TestBase
    {
        #region GetDirectoryListing

        [TestMethod]
        public void FtpClientService_GetDirectoryListing()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            TestBase.CreateTestEnvironment();

            // Act
            List<FtpEntry> result = sut.GetDirectoryListing(TestBase.FtpTestRootFolder).ToList();

            // Log
            foreach (FtpEntry ftpEntry in result)
            {
                Console.WriteLine(ftpEntry);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(6, result.Count, "We assume that there are 4 files and 2 directories on the FTP server in the root test directory.");
        }

        #endregion

        #region GetDirectoryListingRecursive

        [TestMethod]
        public void FtpClientService_GetDirectoryListingRecursive()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            List<FtpEntry> result = sut.GetDirectoryListingRecursive(TestBase.FtpTestRootFolder).ToList();

            // Log
            foreach (FtpEntry ftpEntry in result)
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
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.DownloadFile(TestBase.FtpFileText1, TestBase.LocalFileText1, true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_FileDownload_BinaryFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            TestBase.CreateTestEnvironment();

            // Act
            bool result = sut.DownloadFile(TestBase.FtpFileBinary1, TestBase.LocalFileBinary1, true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileDownloadRecursive

        [TestMethod]
        public void FtpClientService_FileDownloadRecursive()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.DownloadFileRecursive(TestBase.FtpTestRootFolder, TestBase.LocalTestRootFolder, true, false);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileUpload

        [TestMethod]
        public void FtpClientService_FileUpload()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.UploadFile(TestBase.TestDataFileText1, TestBase.FtpFileText1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileDelete

        [TestMethod]
        public void FtpClientService_FileDelete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            TestBase.CreateTestEnvironment();

            // Act
            bool result = sut.DeleteFile(TestBase.FtpFileText1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Directory Create / Delete

        [TestMethod]
        public void FtpClientService_Directory_Exists_true()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.DirectoryExists(TestBase.FtpTestRootFolder);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Exists_false()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestBase.FtpTestRootFolder);

            // Act
            bool result = sut.DirectoryExists(TestBase.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(false, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Create()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestBase.FtpDirectory1);

            // Act
            bool result = sut.CreateDirectory(TestBase.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Create_Sub()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.CreateDirectory(TestBase.FtpDirectory1 + "subdir1/");

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Delete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestBase.FtpDirectory1);
            sut.CreateDirectory(TestBase.FtpDirectory1);

            // Act
            bool result = sut.DeleteDirectory(TestBase.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Delete Directory with Files in it

        [TestMethod]
        public void FtpClientService_DeleteDirectoryWithFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            sut.EventMessages.Clear();
            bool result3 = sut.DeleteDirectory(TestBase.FtpDirectory1);
            bool result4 = sut.DeleteFile(TestBase.FtpFileText3);
            bool result5 = sut.DeleteDirectory(TestBase.FtpDirectory1);

            // Assert
            Assert.AreEqual<bool>(false, result3, "Directory was deleted - this should NOT be possible since there are files in the directory.");
            Assert.AreEqual<bool>(true, result4, "File could not be deleted.");
            Assert.AreEqual<bool>(true, result5, "Directory could not be deleted");

            // Log
            foreach (FtpServiceEventArgs message in sut.EventMessages)
            {
                Console.WriteLine(message.ToString());
            }
        }

        #endregion

        #region BuildHtmlMessage

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FtpClientService_BuildHtmlMessage_Null()
        {
            // Assign
            List<FtpServiceEventArgs> input = null;

            // Act
            FtpClientService.BuildHtmlMessage(input);
        }

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
            input.Add(FtpServiceEventArgs.Information("message"));
            input.Add(FtpServiceEventArgs.DirectoryCreate("directory", "message"));
            input.Add(FtpServiceEventArgs.DirectoryDelete("directory", "message"));
            input.Add(FtpServiceEventArgs.Error("message", new Exception("exception")));
            input.Add(FtpServiceEventArgs.FileDelete("file", "message"));
            input.Add(FtpServiceEventArgs.FileDownload("fileFrom", "fileTO", "message"));
            input.Add(FtpServiceEventArgs.FileUpload("fileFrom", "fileTo", "message"));
            input.Add(FtpServiceEventArgs.Information("message"));

            // Act
            string result = FtpClientService.BuildHtmlMessage(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count());
        }

        #endregion
    }
}