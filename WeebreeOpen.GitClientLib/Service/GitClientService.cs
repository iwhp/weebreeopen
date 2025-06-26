using System;
using System.Collections.Generic;
using System.IO;
using LibGit2Sharp;
using WeebreeOpen.GitClientLib.Model;

namespace WeebreeOpen.GitClientLib.Service;

public class GitClientService
{
    public void DeleteRepositories(string gitRootDirectory)
    {
        #region Verify Parameters

        if (string.IsNullOrWhiteSpace(gitRootDirectory))
        {
            throw new ArgumentNullException("gitRootDirectory");
        }

        if (!Directory.Exists(gitRootDirectory))
        {
            throw new ArgumentException("Path does not exist.", "gitRootDirectory");
        }

        if (!IsDirectoryRootDirectory(gitRootDirectory))
        {
            throw new ArgumentException("Path is not a git root directory.", "gitRootDirectory");
        }

        #endregion

        Directory.Delete(gitRootDirectory);
    }

    public List<string> FindRepositories(string startingPath)
    {
        #region Verify Parameters

        if (string.IsNullOrWhiteSpace(startingPath))
        {
            throw new ArgumentNullException("startingPath");
        }

        if (!Directory.Exists(startingPath))
        {
            throw new ArgumentException("Path does not exist.", "startingPath");
        }

        #endregion

        List<string> repositories = new();
        if (IsDirectoryRootDirectory(startingPath))
        {
            repositories.Add(startingPath);
            return repositories;
        }

        foreach (string item in Directory.GetDirectories(startingPath))
        {
            repositories.AddRange(FindRepositories(item));
        }

        return repositories;
    }

    public RepositoryDetails GetRepositoryDetails(string gitRootDirectory, bool isGetStatusEntries = false)
    {
        #region Verify Parameters

        if (string.IsNullOrWhiteSpace(gitRootDirectory))
        {
            throw new ArgumentNullException("gitRootDirectory");
        }

        if (!Directory.Exists(gitRootDirectory))
        {
            throw new ArgumentException("Path does not exist.", "gitRootDirectory");
        }

        if (!IsDirectoryRootDirectory(gitRootDirectory))
        {
            throw new ArgumentException("Path is not a git root directory.", "gitRootDirectory");
        }

        #endregion

        RepositoryDetails repositoryDetails = new(gitRootDirectory);
        if (isGetStatusEntries)
        {
            repositoryDetails.StatusEntries = GetStatus(repositoryDetails.RepositoryPath);
        }

        return repositoryDetails;
    }

    public List<RepositoryDetails> GetRepositoryDetails(List<string> gitRootDirectories, bool isGetStatusEntries = false)
    {
        #region Verify Parameters

        if (gitRootDirectories == null)
        {
            throw new ArgumentNullException("gitRootDirectories");
        }

        #endregion

        List<RepositoryDetails> repositoryDetails = new();

        foreach (string item in gitRootDirectories)
        {
            repositoryDetails.Add(GetRepositoryDetails(item, isGetStatusEntries));
        }

        return repositoryDetails;
    }

    public List<StatusEntry> GetStatus(string gitRootDirectory)
    {
        #region Verify Parameters

        if (string.IsNullOrWhiteSpace(gitRootDirectory))
        {
            throw new ArgumentNullException("gitRootDirectory");
        }

        if (!Directory.Exists(gitRootDirectory))
        {
            throw new ArgumentException("Path does not exist.", "gitRootDirectory");
        }

        if (!IsDirectoryRootDirectory(gitRootDirectory))
        {
            throw new ArgumentException("Path is not a git root directory.", "gitRootDirectory");
        }

        #endregion

        List<StatusEntry> statusEntries = new();

        using (IRepository repository = new LibGit2Sharp.Repository(gitRootDirectory))
        {
            foreach (LibGit2Sharp.StatusEntry item in repository.RetrieveStatus())
            {
                statusEntries.Add(item);
            }
        }

        return statusEntries;
    }

    public List<StatusEntry> GetStatus(List<string> gitRootDirectories)
    {
        #region Verify Parameters

        if (gitRootDirectories == null)
        {
            throw new ArgumentNullException("gitRootDirectories");
        }

        #endregion

        List<StatusEntry> statusEntries = new();

        foreach (string item in gitRootDirectories)
        {
            statusEntries.AddRange(GetStatus(item));
        }

        return statusEntries;
    }

    public bool IsDirectoryRootDirectory(string diretcoryPath)
    {
        #region Verify Parameters

        if (string.IsNullOrWhiteSpace(diretcoryPath))
        {
            throw new ArgumentNullException("diretcoryPath");
        }

        if (!Directory.Exists(diretcoryPath))
        {
            throw new ArgumentException("Path does not exist.", "diretcoryPath");
        }

        #endregion

        if (Directory.Exists(diretcoryPath + @"\.git"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
