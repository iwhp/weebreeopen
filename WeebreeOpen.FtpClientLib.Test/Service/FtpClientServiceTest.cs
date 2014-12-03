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
        #region Test Parameters

        private FtpClientConnection ftpClientConnection = new FtpClientConnection()
        {
            ServerNameOrIp = "ftp-server",
            UserName = @"username",
            Password = "password"
        };

        private string initialDirectory = "subdir";
        private string directoryCreateDelete1 = "subdir/UnitTest-1";
        private string directoryCreateDelete2 = "subdir/UnitTest-2";

        #endregion

        #region GetFileList*

        [TestMethod]
        public void FtpClientService_GetFileList()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);


            // Act
            List<FtpEntry> result = sut.GetDirectoryFileListing(initialDirectory).ToList();

            // Log
            foreach (var ftpEntry in result)
            {
                Console.WriteLine(ftpEntry);
                //Console.WriteLine(ftpEntry.Name);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(11, result.Count);
        }

        [TestMethod]
        public void FtpClientService_GetDirectoryFileListingRecursive()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            List<FtpEntry> result = sut.GetDirectoryFileListingRecursive(initialDirectory).ToList();

            // Log
            foreach (var ftpEntry in result)
            {
                Console.WriteLine(ftpEntry);
                //Console.WriteLine(ftpEntry.Name);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(43, result.Count);
        }

        #endregion

        #region FileDownload


        [TestMethod]
        public void FtpClientService_FileDownload_TextFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.FileDownload(@"BVDWeb/testharry.txt", @"C:\!Data\FtpClient\testharry.txt", true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_FileDownload_BinaryFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.FileDownload(@"BVDWeb/26/40/Picture_40.bmp", @"C:\!Data\FtpClient\Picture_40.bmp", true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileDownloadRecursive


        [TestMethod]
        public void FtpClientService_FileDownloadRecursive()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.FileDownloadRecursive(@"BVDWeb/Test", @"C:\!Data\FtpClient\Test", true, true);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileUpload

        [TestMethod]
        public void FtpClientService_FileUpload()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.FileUpload(@"C:\!Data\FtpClient\testharry.txt", directoryCreateDelete2 + "/testharry.txt");

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region FileDelete

        [TestMethod]
        public void FtpClientService_FileDelete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.FileDelete(directoryCreateDelete2 + "/testharry.txt");

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Directory Create / Delete

        [TestMethod]
        public void FtpClientService_Directory_Create()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.DirectoryCreate(this.directoryCreateDelete1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void FtpClientService_Directory_Delete()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result = sut.DirectoryDelete(this.directoryCreateDelete1);

            // Assert
            Assert.AreEqual<bool>(true, result);
        }

        #endregion

        #region Delete Directory with Files in it

        [TestMethod]
        public void FtpClientService_DeleteDirectoryWithFile()
        {
            // Assign
            FtpClientService sut = new FtpClientService(ftpClientConnection);

            // Act
            bool result1 = sut.DirectoryCreate(directoryCreateDelete2);
            bool result2 = sut.FileUpload(@"C:\!Data\FtpClient\testharry.txt", directoryCreateDelete2 + "/testharry.txt");
            bool result3 = sut.DirectoryDelete(directoryCreateDelete2);
            bool result4 = sut.FileDelete(directoryCreateDelete2 + "/testharry.txt");
            bool result5 = sut.DirectoryDelete(directoryCreateDelete2);

            // Assert
            Assert.AreEqual<bool>(true, result1, "Directory created failed.");
            Assert.AreEqual<bool>(true, result2, "Update File failed.");
            Assert.AreEqual<bool>(false, result3, "Directory was deleted - this should NOT be possible since there are files in the direcory.");
            Assert.AreEqual<bool>(true, result4, "File could not be deleted.");
            Assert.AreEqual<bool>(true, result5, "Directory could not be deleted");
        }

        #endregion
    }
}