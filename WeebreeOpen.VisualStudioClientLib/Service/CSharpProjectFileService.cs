namespace WeebreeOpen.VisualStudioClientLib.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using WeebreeOpen.VisualStudioClientLib.Model.CSharpProjectFile;

    public class CSharpProjectFileService
    {
        public List<string> FindProjectFiles(string startingPath, bool isThrowExceptionOnPathToLong = false)
        {
            #region Verify Parameters

            if (startingPath.Length > 255)
            {
                if (isThrowExceptionOnPathToLong)
                {
                    throw new ArgumentException("Path too long.", "startingPath");
                }
                else
                {
                    return new List<string>();
                }
            }

            if (string.IsNullOrWhiteSpace(startingPath))
            {
                throw new ArgumentNullException("startingPath");
            }

            if (!Directory.Exists(startingPath))
            {
                throw new ArgumentException("Path does not exist.", "startingPath");
            }

            #endregion

            List<string> repositories = new List<string>();
            string projectFile = GetProjectFile(startingPath);
            if (!string.IsNullOrWhiteSpace(projectFile))
            {
                repositories.Add(projectFile);
                return repositories;
            }

            foreach (string item in Directory.GetDirectories(startingPath))
            {
                repositories.AddRange(FindProjectFiles(item));
            }

            return repositories;
        }

        public string GetProjectFile(string diretcoryPath)
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

            foreach (string file in Directory.GetFiles(diretcoryPath))
            {
                if (file.EndsWith(".csproj"))
                {
                    return file;
                }
            }
            return "";
        }

        public bool IsDirectoryProjectDirectory(string diretcoryPath)
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

            string projectFile = GetProjectFile(diretcoryPath);

            if (string.IsNullOrWhiteSpace(projectFile))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Project GetProject(string projectFilePath)
        {
            using (var stream = System.IO.File.OpenRead(projectFilePath))
            {
                if (stream.Length == 0)
                {
                    return null;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                return serializer.Deserialize(stream) as Project;
            }
        }

        public List<string> FindReferences(string profileFilePath)
        {
            #region Verify Parameters

            if (string.IsNullOrWhiteSpace(profileFilePath))
            {
                throw new ArgumentNullException("profileFilePath");
            }

            if (!File.Exists(profileFilePath))
            {
                throw new ArgumentException("File does not exist.", "profileFilePath");
            }

            #endregion

            List<string> references = new List<string>();

            Project project = GetProject(profileFilePath);

            if (project == null)
            {
                return references;
            }

            // Add ItemGroup.Reference.Include
            foreach (var itemGroup in project.ItemGroup)
            {
                if (itemGroup.Reference == null)
                {
                    continue;
                }

                foreach (var reference in itemGroup.Reference)
                {
                    references.Add(reference.Include);
                }
            }

            // Add ItemGroup.ProjectReference
            foreach (var itemGroup in project.ItemGroup)
            {
                if (itemGroup.ProjectReference == null)
                {
                    continue;
                }

                foreach (var reference in itemGroup.ProjectReference)
                {
                    references.Add(reference.Name);
                }
            }

            return references;
        }
    }
}
