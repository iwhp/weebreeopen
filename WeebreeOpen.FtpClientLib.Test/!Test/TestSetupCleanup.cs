namespace WeebreeOpen.FtpClientLib.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.FtpClientLib.Model;
    using WeebreeOpen.FtpClientLib.Service;

    [TestClass()]
    [DeploymentItem(@"!Test\Data", @"!Test\Data")]
    public class TestSetupCleanup
    {
        #region Properties

        public static FtpClientConnection FtpClientConnection { get; private set; }

        public static string TestDataFileText1 = @"!Test\Data\testfile1-text.txt";
        public static string TestDataFileBinary1 = @"!Test\Data\testfile2-image.jpg";

        public static string FtpTestRootFolder = "WeebreeOpenTest/";    // if filled, end with a slash (/)
        public static string FtpFileText1 = TestSetupCleanup.FtpTestRootFolder + "FtpToFileText1.txt";
        public static string FtpFileBinary1 = TestSetupCleanup.FtpTestRootFolder + "FtpToFileBinary1.jpg";
        public static string FtpDirectory1 = TestSetupCleanup.FtpTestRootFolder + "TestDir1/";
        public static string FtpDirectory2 = TestSetupCleanup.FtpTestRootFolder + "TestDir2/";
        public static string FtpFileText3 = TestSetupCleanup.FtpDirectory1 + "FtpToFileText3.txt";
        public static string FtpFileBinary4 = TestSetupCleanup.FtpDirectory2 + "FtpToFileBinary4.jpg";


        public static string LocalTestRootFolder = @"C:\!Data\FtpClientLib\"; // end with a backslash (\)
        public static string LocalFileText1 = TestSetupCleanup.LocalTestRootFolder + "LocalToFileText1.txt";
        public static string LocalFileBinary1 = TestSetupCleanup.LocalTestRootFolder + "LocalToFileBinary1.jpg";

        #endregion

        #region Initialize

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //TestSetupCleanup.FtpClientConnection = new FtpClientConnection("ftp-server", @"username", "password");
            //TestSetupCleanup.CreateTestEnvironment();
        }

        public static void CreateTestEnvironment() {
            FtpClientService ftpClientService = new FtpClientService(TestSetupCleanup.FtpClientConnection);

            // Delete all
            ftpClientService.DirectoryDeleteRecursive(TestSetupCleanup.FtpTestRootFolder);

            // Create root directory
            ftpClientService.DirectoryCreate(TestSetupCleanup.FtpTestRootFolder);

            // Create subdirectories
            ftpClientService.DirectoryCreate(TestSetupCleanup.FtpDirectory1);
            ftpClientService.DirectoryCreate(TestSetupCleanup.FtpDirectory2);

            // Add test files
            ftpClientService.FileUpload(TestSetupCleanup.TestDataFileText1, TestSetupCleanup.FtpFileText1);
            ftpClientService.FileUpload(TestSetupCleanup.TestDataFileBinary1, TestSetupCleanup.FtpFileBinary1);
            ftpClientService.FileUpload(TestSetupCleanup.TestDataFileText1, TestSetupCleanup.FtpFileText3);
            ftpClientService.FileUpload(TestSetupCleanup.TestDataFileBinary1, TestSetupCleanup.FtpFileBinary4);
        }

        #endregion

        #region Cleanup

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            FtpClientService ftpClientService = new FtpClientService(TestSetupCleanup.FtpClientConnection);
            ftpClientService.DirectoryDeleteRecursive(TestSetupCleanup.FtpTestRootFolder);
        }

        #endregion
    }
}
