namespace WeebreeOpen.GitClientLib.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LibGit2Sharp;

    public class RepositoryDetails
    {
        public RepositoryDetails()
        {
            this.StatusEntries = new List<StatusEntry>();
        }

        public RepositoryDetails(string repositoryPath)
            : this()
        {
            this.RepositoryPath = repositoryPath;
        }

        public string RepositoryPath { get; internal set; }

        public string Version { get; internal set; }

        public List<StatusEntry> StatusEntries { get; internal set; }
    }
}
