using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeebreeOpen.FtpClientLib.Model;
using WeebreeOpen.FtpClientLib.Service;

namespace WeebreeOpen.FtpClientLib.Test
{
    [DeploymentItem(@"TESTDATA", @"TESTDATA")]
    [DebuggerStepThrough]
    public abstract class TestBase
    {
        #region Properties

        public bool IsDeleteFileSystemAndFtpAfterTestRun { get; set; } = true;

        public static FtpClientService FtpClientService { get; private set; }
        public static FtpClientConnection FtpClientConnection { get; private set; }

        /// <summary>
        /// Use this to upload to FTP, or prepare File System for unit test.
        /// </summary>
        public static string TestDataPathText1 = @"TESTDATA\testfile1-text.txt";

        /// <summary>
        /// Use this to upload to FTP, or prepare File System for unit test.
        /// </summary>
        public static string TestDataPathBinary1 = @"TESTDATA\testfile2-image.jpg";

        public static string FtpRootPath = "UnitTest_WeebreeOpenTest";
        public static string FtpDirectoryPath1 = TestBase.FtpRootPath + "/" + "TestDir1";
        public static string FtpDirectoryPath2 = TestBase.FtpRootPath + "/" + "TestDir2";
        public static string FtpFilePathText1 = TestBase.FtpRootPath + "/" + "FileText1.txt";
        public static string FtpFilePathBinary1 = TestBase.FtpRootPath + "/" + "FileBinary1.jpg";
        public static string FtpFilePathText2 = TestBase.FtpDirectoryPath1 + "/" + "FileText2.txt";
        public static string FtpFilePathBinary2 = TestBase.FtpDirectoryPath2 + "/" + "FileBinary2.jpg";

        public static string FileSystemRootPath = @"C:\DATA\FTPCLIENTLIB";

        /// <summary>
        /// Use this to download from FTP, or prepare File System for unit test.
        /// </summary>
        public static string FileSystemPathText1 = Path.Combine(TestBase.FileSystemRootPath, "FileText1.txt");
        public static string FileSystemPathText2 = Path.Combine(TestBase.FileSystemRootPath, "TestDir1", "FileText2.txt");

        /// <summary>
        /// Use this to download from FTP, or prepare File System for unit test.
        /// </summary>
        public static string FileSystemPathBinary1 = Path.Combine(TestBase.FileSystemRootPath, "FileBinary1.jpg");
        public static string FileSystemPathBinary2 = Path.Combine(TestBase.FileSystemRootPath, "TestDir2", "FileBinary2.jpg");

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void TestInitialize()
        {
            // Activate one of the following connection string
            //TestBase.FtpClientConnection = new FtpClientConnection("xxx.ernidruck.xxx", "xxx", "xxx");
            //TestBase.FtpClientConnection = new FtpClientConnection("xxx.ardimedia.xxx", "xxx", "xxx");
            TestBase.FtpClientService = new FtpClientService(TestBase.FtpClientConnection);

            TestBase.FileSystemDeleteAll();
            TestBase.FtpDeleteAll();
            TestBase.FileSystemCreateRoot();
            TestBase.FtpCreateRoot();
        }

        public static void CreateTestEnvironment()
        {
            if (TestBase.FtpClientConnection == null)
            {
                return;
            }

            if (Directory.Exists(TestBase.FileSystemRootPath))
            {
                Directory.Delete(TestBase.FileSystemRootPath, true);
            }

            // Prepare local file system
            Directory.CreateDirectory(TestBase.FileSystemRootPath);
            File.Copy(TestBase.FileSystemPathText1, TestBase.FileSystemPathText1);
            File.Copy(TestBase.FileSystemPathBinary1, TestBase.FileSystemPathBinary1);

            // Prepare FTP server: Delete all
            TestBase.FtpClientService.DeleteDirectoryRecursive(TestBase.FtpRootPath);

            // Prepare FTP server: Create root directory
            TestBase.FtpClientService.CreateDirectory(TestBase.FtpRootPath);

            // Prepare FTP server: Create subdirectories
            TestBase.FtpClientService.CreateDirectory(TestBase.FtpDirectoryPath1);
            TestBase.FtpClientService.CreateDirectory(TestBase.FtpDirectoryPath2);

            // Prepare FTP server: Add test files
            TestBase.FtpClientService.UploadFile(TestBase.FileSystemPathText1, TestBase.FtpFilePathText1);
            TestBase.FtpClientService.UploadFile(TestBase.FileSystemPathBinary1, TestBase.FtpFilePathBinary1);
            TestBase.FtpClientService.UploadFile(TestBase.FileSystemPathText1, TestBase.FtpFilePathText2);
            TestBase.FtpClientService.UploadFile(TestBase.FileSystemPathBinary1, TestBase.FtpFilePathBinary2);
        }

        #endregion

        #region Test Cleanup

        [TestCleanup()]
        public void TestCleanup()
        {
            if (this.IsDeleteFileSystemAndFtpAfterTestRun)
            {
                TestBase.FileSystemDeleteAll();
                TestBase.FtpDeleteAll();
            }
        }

        #endregion

        #region Test Helper

        public void LogEventMessages(FtpClientService ftpClientService)
        {
            foreach (FtpServiceEventArgs item in ftpClientService.EventMessages)
            {
                Console.WriteLine("MESSAGE: " + item.Message
                    + Environment.NewLine + " DIRECTORY: " + item.Directory
                    + Environment.NewLine + " DIRECTORYFROM: " + item.DirectoryFrom
                    + Environment.NewLine + " DIRECTORYTO: " + item.DirectoryTo
                    + Environment.NewLine + " FILE: " + item.File
                    + Environment.NewLine + " FILEFROM: " + item.FileFrom
                    + Environment.NewLine + " FILETO: " + item.FileTo
                    );
            }
        }

        #endregion

        #region Static Methods

        #region Create

        public static void FileSystemCreateRoot()
        {
            Directory.CreateDirectory(TestBase.FileSystemRootPath);
        }

        public static void FtpCreateRoot()
        {
            TestBase.FtpClientService.CreateDirectory(TestBase.FtpRootPath);
        }

        public static void FtpCreateDirectory1()
        {
            TestBase.FtpClientService.CreateDirectory(TestBase.FtpDirectoryPath1);
        }

        public static void FtpCreateDirectory2()
        {
            TestBase.FtpClientService.CreateDirectory(TestBase.FtpDirectoryPath2);
        }

        #endregion

        #region Copy

        #region File System

        public static void FileSystemCopyFileText1()
        {
            File.Copy(TestBase.TestDataPathText1, TestBase.FileSystemPathText1);
        }

        public static void FileSystemCopyFileBinary1()
        {
            File.Copy(TestBase.TestDataPathBinary1, TestBase.FileSystemPathBinary1);
        }

        #endregion

        #region FTP Copy

        public static void FtpCopyFileText1()
        {
            TestBase.FtpClientService.UploadFile(TestBase.TestDataPathText1, TestBase.FtpFilePathText1, isOverrideExisting: true);
        }

        public static void FtpCopyFileText2()
        {
            TestBase.FtpClientService.UploadFile(TestBase.TestDataPathText1, TestBase.FtpFilePathText2, isOverrideExisting: true);
        }

        public static void FtpCopyFileBinary1()
        {
            TestBase.FtpClientService.UploadFile(TestBase.TestDataPathBinary1, TestBase.FtpFilePathBinary1, isOverrideExisting: true);
        }

        public static void FtpCopyFileBinary2()
        {
            TestBase.FtpClientService.UploadFile(TestBase.TestDataPathBinary1, TestBase.FtpFilePathBinary2, isOverrideExisting: true);
        }

        #endregion

        #region FTP Exits

        public static bool FtpExistsFileText1()
        {
            return TestBase.FtpClientService.FtpFileExists(TestBase.FtpFilePathText1);
        }

        public static bool FtpExistsFileText2()
        {
            return TestBase.FtpClientService.FtpFileExists(TestBase.FtpFilePathText2);
        }

        public static bool FtpExistsFileBinary1()
        {
            return TestBase.FtpClientService.FtpFileExists(TestBase.FtpFilePathBinary1);
        }

        public static bool FtpExistsFileBinary2()
        {
            return TestBase.FtpClientService.FtpFileExists(TestBase.FtpFilePathBinary2);
        }

        public static bool FtpExistsDirectoryPath1()
        {
            return TestBase.FtpClientService.DirectoryExists(TestBase.FtpDirectoryPath1);
        }

        public static bool FtpExistsDirectoryPath2()
        {
            return TestBase.FtpClientService.DirectoryExists(TestBase.FtpDirectoryPath2);
        }

        #endregion

        #endregion

        #region Delete

        public static void FileSystemDeleteAll()
        {
            // Delete file system all
            if (Directory.Exists(TestBase.FileSystemRootPath))
            {
                Directory.Delete(TestBase.FileSystemRootPath, true);
            }

            // Delete FTP all
            TestBase.FtpClientService.DeleteDirectoryRecursive(TestBase.FtpRootPath);
        }

        public static void FtpDeleteAll()
        {
            // Delete file system all
            if (Directory.Exists(TestBase.FileSystemRootPath))
            {
                Directory.Delete(TestBase.FileSystemRootPath, true);
            }

            // Delete FTP all
            TestBase.FtpClientService.DeleteDirectoryRecursive(TestBase.FtpRootPath);
        }

        #endregion

        #endregion
    }
}
