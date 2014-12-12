namespace WeebreeOpen.GitClientCmd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LibGit2Sharp;
    using WeebreeOpen.GitClientLib.Model;
    using WeebreeOpen.GitClientLib.Service;

    class Program
    {
        static void Main(string[] args)
        {

            GitClientService service = new GitClientService();
            List<string> repositoriesRoots = service.FindRepositories(Properties.Settings.Default.SearchForGitRepositoryStartDirectory);
            List<RepositoryDetails> repositoryDetails = service.GetRepositoryDetails(repositoriesRoots, true);

            #region Show Repositories

            Console.WriteLine("\nShow Repositories ==============================================");
            foreach (var repositoryDetail in repositoryDetails)
            {
                Console.WriteLine(string.Format("{0}", repositoryDetail.RepositoryPath));
            }

            #endregion

            #region Show StatusEntries for repositories

            Console.WriteLine("\nShow StatusEntries for repositories =============================");
            foreach (var repositoryDetail in repositoryDetails)
            {
                foreach (var statusEntry in repositoryDetail.StatusEntries.Where(x => x.State != FileStatus.Ignored))
                {
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}", repositoryDetail.RepositoryPath, statusEntry.State, statusEntry.FilePath));
                }
            }

            #endregion
        }
    }
}
