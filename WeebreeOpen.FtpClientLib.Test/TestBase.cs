using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeebreeOpen.FtpClientLib.Model;
using WeebreeOpen.FtpClientLib.Service;

namespace WeebreeOpen.FtpClientLib.Test
{
    [DeploymentItem(@"TestData", @"TestData")]
    public abstract class TestBase
    {
        #region Properties

        public static FtpClientConnection FtpClientConnection { get; private set; }

        public static string TestDataFileText1 = @"TestData\testfile1-text.txt";
        public static string TestDataFileBinary1 = @"TestData\testfile2-image.jpg";

        public static string FtpTestRootFolder = "UnitTest_WeebreeOpenTest/";    // if filled, end with a slash (/)
        public static string FtpDirectory1 = TestBase.FtpTestRootFolder + "TestDir1/";
        public static string FtpDirectory2 = TestBase.FtpTestRootFolder + "TestDir2/";
        public static string FtpFileText1 = TestBase.FtpTestRootFolder + "FtpToFileText1.txt";
        public static string FtpFileBinary1 = TestBase.FtpTestRootFolder + "FtpToFileBinary1.jpg";
        public static string FtpFileText3 = TestBase.FtpDirectory1 + "FtpToFileText3.txt";
        public static string FtpFileBinary4 = TestBase.FtpDirectory2 + "FtpToFileBinary4.jpg";


        public static string LocalTestRootFolder = @"C:\!Data\FtpClientLib\"; // end with a backslash (\)
        public static string LocalFileText1 = TestBase.LocalTestRootFolder + "LocalToFileText1.txt";
        public static string LocalFileBinary1 = TestBase.LocalTestRootFolder + "LocalToFileBinary1.jpg";

        #endregion

        #region Initialize

        [AssemblyInitialize]
        public void AssemblyInitialize()
        {
            TestBase.FtpClientConnection = new FtpClientConnection("xxx", @"xxx", "xxx");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestBase.FtpClientConnection = new FtpClientConnection("xxx", @"xxx", "xxx");
            TestBase.CreateTestEnvironment();
        }

        public static void CreateTestEnvironment()
        {
            if (TestBase.FtpClientConnection == null)
            {
                return;
            }

            if (Directory.Exists(TestBase.LocalTestRootFolder))
            {
                Directory.Delete(TestBase.LocalTestRootFolder, true);
            }
            Directory.CreateDirectory(TestBase.LocalTestRootFolder);
            File.Copy(TestBase.TestDataFileText1, TestBase.LocalFileText1);
            File.Copy(TestBase.TestDataFileBinary1, TestBase.LocalFileBinary1);

            FtpClientService ftpClientService = new FtpClientService(TestBase.FtpClientConnection);

            // Delete all
            ftpClientService.DeleteDirectoryRecursive(TestBase.FtpTestRootFolder);

            // Create root directory
            ftpClientService.CreateDirectory(TestBase.FtpTestRootFolder);

            // Create subdirectories
            ftpClientService.CreateDirectory(TestBase.FtpDirectory1);
            ftpClientService.CreateDirectory(TestBase.FtpDirectory2);

            // Add test files
            ftpClientService.UploadFile(TestBase.TestDataFileText1, TestBase.FtpFileText1);
            ftpClientService.UploadFile(TestBase.TestDataFileBinary1, TestBase.FtpFileBinary1);
            ftpClientService.UploadFile(TestBase.TestDataFileText1, TestBase.FtpFileText3);
            ftpClientService.UploadFile(TestBase.TestDataFileBinary1, TestBase.FtpFileBinary4);
        }

        #endregion

        #region Cleanup

        [TestCleanup()]
        public void TestCleanup()
        {
            if (TestBase.FtpClientConnection == null)
            {
                return;
            }

            Directory.Delete(TestBase.LocalTestRootFolder, true);

            FtpClientService ftpClientService = new FtpClientService(TestBase.FtpClientConnection);
            ftpClientService.DeleteDirectoryRecursive(TestBase.FtpTestRootFolder);
        }

        #endregion
    }
}
