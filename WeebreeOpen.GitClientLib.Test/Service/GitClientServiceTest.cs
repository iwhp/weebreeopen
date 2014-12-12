namespace WeebreeOpen.GitClientLib.Test.Service
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.GitClientLib.Service;
    using LibGit2Sharp;
    using WeebreeOpen.GitClientLib.Model;

    [TestClass]
    public class GitClientServiceTest
    {
        #region Test Data

        private string GitRepositoryPath { get; set; }

        #endregion

        #region GitClientService_FindRepositories

        [TestMethod]
        public void GitClientService_FindRepositories()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github";

            // Act
            List<string> result = sut.FindRepositories(this.GitRepositoryPath);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
            Assert.AreEqual<int>(58, result.Count);
        }

        [TestMethod]
        public void GitClientService_FindRepositories_GitRootDirectory()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github\iwhp\weebreeopen";

            // Act
            List<string> result = sut.FindRepositories(this.GitRepositoryPath);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(1, result.Count);
        }

        [TestMethod]
        public void GitClientService_FindRepositories_Inside_Git_Directory()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github\iwhp\weebreeopen\.git";

            // Act
            List<string> result = sut.FindRepositories(this.GitRepositoryPath);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual<int>(0, result.Count);
        }

        #endregion

        #region GitClientService_GetRepositoryDetails

        [TestMethod]
        public void GitClientService_GetRepositoryDetails()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github\iwhp\weebreeopen";

            // Act
            RepositoryDetails result = sut.GetRepositoryDetails(this.GitRepositoryPath, isGetStatusEntries: true);

            // Log
            foreach (var item in result.StatusEntries.OrderBy(x => x.State))
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}", result.RepositoryPath, item.State, item.FilePath));
            }
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.StatusEntries.Count);
        }

        [TestMethod]
        public void GitClientService_GetRepositoryDetails_MoreThanOneRepository()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github";
            List<string> gitRepositories = sut.FindRepositories(this.GitRepositoryPath);

            // Act
            List<RepositoryDetails> result = sut.GetRepositoryDetails(gitRepositories, isGetStatusEntries: true);

            // Log
            foreach (var repositoryDetails in result)
            {
                foreach (var item in repositoryDetails.StatusEntries.OrderBy(x => x.State))
                {
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}", repositoryDetails.RepositoryPath, item.State, item.FilePath));
                }
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }

        #endregion

        #region GitClientService_GetStatus

        [TestMethod]
        public void GitClientService_GetStatus()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github\iwhp\weebreeopen";

            // Act
            List<StatusEntry> result = sut.GetStatus(this.GitRepositoryPath);

            // Log
            foreach (var item in result.OrderBy(x => x.State))
            {
                Console.WriteLine(string.Format("{0}\t{1}", item.State, item.FilePath));
            }
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }

        [TestMethod]
        public void GitClientService_GetStatus_MoreThanOneRepository()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github";
            List<string> gitRepositories = sut.FindRepositories(this.GitRepositoryPath);

            // Act
            List<StatusEntry> result = sut.GetStatus(gitRepositories);

            // Log
            foreach (var item in result.OrderBy(x => x.State))
            {
                Console.WriteLine(string.Format("{0}\t{1}", item.State, item.FilePath));
            }
            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }

        #endregion

        #region GitClientService_IsDirectoryRootDirectory

        [TestMethod]
        public void GitClientService_IsDirectoryRootDirectory_Yes()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github\iwhp\weebreeopen";

            // Act
            bool result = sut.IsDirectoryRootDirectory(this.GitRepositoryPath);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GitClientService_IsDirectoryRootDirectory_No()
        {
            // Assign
            GitClientService sut = new GitClientService();
            this.GitRepositoryPath = @"D:\!Data\Source\Git.Github\iwhp";

            // Act
            bool result = sut.IsDirectoryRootDirectory(this.GitRepositoryPath);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
