using System;
using System.Collections.Generic;
using System.IO;
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
            TestBase.FtpCreateDirectory1();
            TestBase.FtpCreateDirectory2();
            TestBase.FtpCopyFileText1();
            TestBase.FtpCopyFileText2();
            TestBase.FtpCopyFileBinary1();
            TestBase.FtpCopyFileBinary2();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            List<FtpEntry> result = sut.GetDirectoryListing(TestBase.FtpRootPath).ToList();

            // Log
            this.LogEventMessages(sut);

            foreach (FtpEntry ftpEntry in result)
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
            TestBase.FtpCreateDirectory1();
            TestBase.FtpCreateDirectory2();
            TestBase.FtpCopyFileText1();
            TestBase.FtpCopyFileText2();
            TestBase.FtpCopyFileBinary1();
            TestBase.FtpCopyFileBinary2();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            List<FtpEntry> result = sut.GetDirectoryListingRecursive(TestBase.FtpRootPath).ToList();

            // Log
            this.LogEventMessages(sut);

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
            TestBase.FtpCopyFileText1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathText1));

            // Act
            bool result = sut.DownloadFile(TestBase.FtpFilePathText1, TestBase.FileSystemPathText1, false);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathText1));
        }

        [TestMethod]
        public void FtpClientService_FileDownload_BinaryFile()
        {
            // Assign
            TestBase.FtpCopyFileBinary1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathBinary1));

            // Act
            bool result = sut.DownloadFile(TestBase.FtpFilePathBinary1, TestBase.FileSystemPathBinary1, false);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathBinary1));
        }

        #endregion

        #region DownloadFilesInDirectory

        [TestMethod]
        public void FtpClientService_DownloadFilesInDirectory()
        {
            // Assign
            TestBase.FtpCreateDirectory1();
            TestBase.FtpCreateDirectory2();
            TestBase.FtpCopyFileText1();
            TestBase.FtpCopyFileText2();
            TestBase.FtpCopyFileBinary1();
            TestBase.FtpCopyFileBinary2();

            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathText1));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathBinary1));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathText2));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathBinary2));
            Assert.IsTrue(TestBase.FtpExistsFileBinary1());
            Assert.IsTrue(TestBase.FtpExistsFileBinary2());
            Assert.IsTrue(TestBase.FtpExistsFileText1());
            Assert.IsTrue(TestBase.FtpExistsFileText2());
            Assert.IsTrue(TestBase.FtpExistsDirectoryPath1());
            Assert.IsTrue(TestBase.FtpExistsDirectoryPath2());

            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.DownloadFilesInDirectory(TestBase.FtpRootPath, TestBase.FileSystemRootPath, true, true);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathText1));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathText2));
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathBinary1));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathBinary2));
            Assert.IsFalse(TestBase.FtpExistsFileBinary1());
            Assert.IsTrue(TestBase.FtpExistsFileBinary2());
            Assert.IsFalse(TestBase.FtpExistsFileText1());
            Assert.IsTrue(TestBase.FtpExistsFileText2());
            Assert.IsTrue(TestBase.FtpExistsDirectoryPath1());
            Assert.IsTrue(TestBase.FtpExistsDirectoryPath2());
        }

        #endregion

        #region FileDownloadRecursive

        [TestMethod]
        public void FtpClientService_FileDownloadRecursive()
        {
            // Assign
            TestBase.FtpCreateDirectory1();
            TestBase.FtpCreateDirectory2();
            TestBase.FtpCopyFileText1();
            TestBase.FtpCopyFileText2();
            TestBase.FtpCopyFileBinary1();
            TestBase.FtpCopyFileBinary2();

            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathText1));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathBinary1));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathText2));
            Assert.AreEqual<bool>(false, File.Exists(TestBase.FileSystemPathBinary2));
            Assert.IsTrue(TestBase.FtpExistsFileBinary1());
            Assert.IsTrue(TestBase.FtpExistsFileBinary2());
            Assert.IsTrue(TestBase.FtpExistsFileText1());
            Assert.IsTrue(TestBase.FtpExistsFileText2());
            Assert.IsTrue(TestBase.FtpExistsDirectoryPath1());
            Assert.IsTrue(TestBase.FtpExistsDirectoryPath2());

            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.DownloadFilesRecursive(TestBase.FtpRootPath, TestBase.FileSystemRootPath, true, true, true);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathText1));
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathText2));
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathBinary1));
            Assert.AreEqual<bool>(true, File.Exists(TestBase.FileSystemPathBinary2));
            Assert.IsFalse(TestBase.FtpExistsFileBinary1());
            Assert.IsFalse(TestBase.FtpExistsFileBinary2());
            Assert.IsFalse(TestBase.FtpExistsFileText1());
            Assert.IsFalse(TestBase.FtpExistsFileText2());
            Assert.IsFalse(TestBase.FtpExistsDirectoryPath1());
            Assert.IsFalse(TestBase.FtpExistsDirectoryPath2());
        }

        #endregion

        #region FileUpload

        [TestMethod]
        public void FtpClientService_FileUpload_FileText1_DoNotDelete()
        {
            // Assign
            TestBase.FileSystemCopyFileText1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            Assert.IsTrue(File.Exists(TestBase.FileSystemPathText1));

            // Act
            bool result = sut.UploadFile(TestBase.FileSystemPathText1, TestBase.FtpFilePathText1, isDeleteSourceAfterUpload: false);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<int>(1, sut.EventMessages.Count);
            Assert.IsTrue(File.Exists(TestBase.FileSystemPathText1));
        }

        [TestMethod]
        public void FtpClientService_FileUpload_FileText1_Delete()
        {
            // Assign
            TestBase.FileSystemCopyFileText1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            Assert.IsTrue(File.Exists(TestBase.FileSystemPathText1));

            // Act
            bool result = sut.UploadFile(TestBase.FileSystemPathText1, TestBase.FtpFilePathText1, isDeleteSourceAfterUpload: true);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<int>(1, sut.EventMessages.Count);
            Assert.IsFalse(File.Exists(TestBase.FileSystemPathText1));
        }

        [TestMethod]
        public void FtpClientService_FileUpload_FileText1_AlreadyExists()
        {
            // Assign
            TestBase.FtpCopyFileText1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            try
            {
                bool result = sut.UploadFile(TestBase.TestDataPathText1, TestBase.FtpFilePathText1);

                // Assert
                Assert.Fail("UploadFile should throw an Exception, since file already exists and override not specified.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("already exists and override not request. Either delete by hand or set"));
            }

            // Log
            this.LogEventMessages(sut);
        }

        [TestMethod]
        public void FtpClientService_FileUpload_FileText1_AlreadyExists_Override()
        {
            // Assign
            TestBase.FtpCopyFileText1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.UploadFile(TestBase.TestDataPathText1, TestBase.FtpFilePathText1, isOverrideExisting: true);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<int>(2, sut.EventMessages.Count);
        }

        [TestMethod]
        public void FtpClientService_FileUpload_FileBinary1()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.UploadFile(TestBase.TestDataPathBinary1, TestBase.FtpFilePathBinary1);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
            Assert.AreEqual<int>(1, sut.EventMessages.Count);
        }

        [TestMethod]
        public void FtpClientService_FileUpload_BackSlash()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            TestBase.FtpFilePathText1 = TestBase.FtpFilePathText1.Replace("/", @"\"); // Simulate a "Windows file path"

            // Act
            bool result = sut.UploadFile(TestBase.TestDataPathText1, TestBase.FtpFilePathText1);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);

            // Fix
            TestBase.FtpFilePathText1 = TestBase.FtpFilePathText1.Replace(@"\", "/");
        }

        #endregion

        #region FileDelete

        [TestMethod]
        public void FtpClientService_FileDelete()
        {
            // Assign
            TestBase.FtpCopyFileText1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.DeleteFile(TestBase.FtpFilePathText1);

            // Log
            this.LogEventMessages(sut);

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
            bool result = sut.DirectoryExists(TestBase.FtpRootPath);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Exists_false()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestBase.FtpRootPath);

            // Act
            bool result = sut.DirectoryExists(TestBase.FtpDirectoryPath1);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(false, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Create()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestBase.FtpDirectoryPath1);

            // Act
            bool result = sut.CreateDirectory(TestBase.FtpDirectoryPath1);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Create_Sub()
        {
            // Assign
            TestBase.FtpCreateDirectory1();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            bool result = sut.CreateDirectory(TestBase.FtpDirectoryPath1 + "/subdir1");

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Delete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);
            sut.DeleteDirectoryRecursive(TestBase.FtpDirectoryPath1);
            sut.CreateDirectory(TestBase.FtpDirectoryPath1);

            // Act
            bool result = sut.DeleteDirectory(TestBase.FtpDirectoryPath1);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Delete Directory with Files in it

        [TestMethod]
        public void FtpClientService_DeleteDirectoryWithFile()
        {
            // Assign
            TestBase.FtpCreateDirectory1();
            TestBase.FtpCreateDirectory2();
            TestBase.FtpCopyFileText1();
            TestBase.FtpCopyFileText2();
            TestBase.FtpCopyFileBinary1();
            TestBase.FtpCopyFileBinary2();
            FtpClientService sut = new FtpClientService(TestBase.FtpClientConnection);

            // Act
            sut.EventMessages.Clear();
            bool result1 = sut.DeleteDirectory(TestBase.FtpDirectoryPath1);
            bool result2 = sut.DeleteFile(TestBase.FtpFilePathText2);
            bool result3 = sut.DeleteDirectory(TestBase.FtpDirectoryPath1);

            // Log
            this.LogEventMessages(sut);

            // Assert
            Assert.AreEqual<bool>(false, result1, "Directory should not be delete, since there are files in the directory.");
            Assert.AreEqual<bool>(true, result2, "File could not be deleted.");
            Assert.AreEqual<bool>(true, result3, "Directory could not be deleted.");
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
            List<FtpServiceEventArgs> input = new List<FtpServiceEventArgs>
            {
                FtpServiceEventArgs.Information("message"),
                FtpServiceEventArgs.DirectoryCreate("directory", "message"),
                FtpServiceEventArgs.DirectoryDelete("directory", "message"),
                FtpServiceEventArgs.Error("message", new Exception("exception")),
                FtpServiceEventArgs.FileDelete("file", "message"),
                FtpServiceEventArgs.FileDownload("fileFrom", "fileTO", "message"),
                FtpServiceEventArgs.FileUpload("fileFrom", "fileTo", "message"),
                FtpServiceEventArgs.Information("message")
            };

            // Act
            string result = FtpClientService.BuildHtmlMessage(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count());
        }

        #endregion
    }
}