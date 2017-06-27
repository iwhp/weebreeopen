namespace WeebreeOpen.VisualStudioClientLib.Test.Servcie
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeebreeOpen.VisualStudioClientLib.Model.CSharpProjectFile;
    using WeebreeOpen.VisualStudioClientLib.Service;

    [TestClass]
    public class CSharpProjectFileTest
    {
        [TestMethod]
        public void CSharpProjectFile_GetProjectFiles()
        {
            // Assign
            CSharpProjectFileService sut = new CSharpProjectFileService();

            // Act
            List<string> result = sut.FindProjectFiles(@"D:\CODE\Git.Github\iwhp\weebreeopen");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }

        [TestMethod]
        public void CSharpProjectFile_FindReferences()
        {
            // Assign
            CSharpProjectFileService sut = new CSharpProjectFileService();

            // Act
            List<string> result = sut.FindReferences(@"D:\CODE\Git.Github\iwhp\weebreeopen\WeebreeOpen.GitClientCmd\WeebreeOpen.GitClientCmd.csproj");

            // LOG
            foreach (var item in result)
            {
                Console.WriteLine("Reference Name: {0}", item);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }

        [TestMethod]
        public void CSharpProjectFile_FindProjectFiles()
        {
            //
            List<string> result = new List<string>();

            // Assign
            CSharpProjectFileService sut = new CSharpProjectFileService();
            List<string> projectFiles = sut.FindProjectFiles(@"D:\CODE");

            // Act
            foreach (var projectFile in projectFiles)
            {
                result.AddRange(sut.FindReferences(projectFile));
            }

            // LOG
            foreach (var item in result.ToList().Where(x =>
                !x.ToLower().StartsWith("system")
                & !x.ToLower().StartsWith("microsoft")

                & !x.ToLower().StartsWith("accessibility")
                & !x.ToLower().StartsWith("activitycontrol")
                & !x.ToLower().StartsWith("adodb")
                & !x.ToLower().StartsWith("ajaxcontroltoolkit")
                & !x.ToLower().StartsWith("ajaxmin")
                & !x.ToLower().StartsWith("aspnet")
                & !x.ToLower().StartsWith("auth10")
                & !x.ToLower().StartsWith("autofac")
                & !x.ToLower().StartsWith("automapper")
                & !x.ToLower().StartsWith("entityframework")
                & !x.ToLower().StartsWith("presentationcore")
                & !x.ToLower().StartsWith("presentationframework")
                & !x.ToLower().StartsWith("windowsbase")
                & !x.ToLower().StartsWith("mscorlib")
                & !x.ToLower().StartsWith("newtonsoft")
                & !x.ToLower().StartsWith("uiautomationprovider")
                & !x.ToLower().StartsWith("unautomationtypes")
                & !x.ToLower().StartsWith("webgrease")
                & !x.ToLower().StartsWith("xunit")

                & !x.ToLower().StartsWith("aforge")
                & !x.ToLower().StartsWith("antlr3")
                & !x.ToLower().StartsWith("aspose")
                & !x.ToLower().StartsWith("breeze")
                & !x.ToLower().StartsWith("dotnet")
                & !x.ToLower().StartsWith("dotnetopenauth")
                & !x.ToLower().StartsWith("telerik")
                & !x.ToLower().StartsWith("thinktecture")
                ).OrderBy(x => x))
            {
                Console.WriteLine("Reference Name: {0}", item);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }

        [TestMethod]
        public void CSharpProjectFile_FindProjectFiles_FindLib()
        {
            //
            List<ProjectReference> result = new List<ProjectReference>();

            // Assign
            CSharpProjectFileService sut = new CSharpProjectFileService();
            List<string> projectFiles = sut.FindProjectFiles(@"D:\CODE");

            // Act
            foreach (var projectFile in projectFiles)
            {
                foreach (var reference in sut.FindReferences(projectFile))
                {
                    ProjectReference projectReference = new ProjectReference() { ProjectName = projectFile, ReferenceName = reference };
                    result.Add(projectReference);
                }
            }

            // LOG
            //foreach (dynamic item in result.ToList().Where(x => x.Reference.StartsWith("Am.MicrosoftOffice")))
            foreach (ProjectReference item in result.Where(x => x.ReferenceName.ToLower().StartsWith("am.microsoftoffice")))
            {
                Console.WriteLine("Reference Name: {0} \t Project Name: {1}", item.ReferenceName, item.ProjectName);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count);
        }
    }
}
