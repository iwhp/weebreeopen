============================================================================================================
DEPLOYMENT (NUGET PACKAGE)
------------------------------------------------------------------------------------------------------------
> DEPLOY LOCAL
  CLS
  CD D:\CODE\Git.Github\iwhp\weebreeopen\WeebreeOpen.SystemLib
  devenv ../WeebreeOpen.sln /project WeebreeOpen.SystemLib.csproj /rebuild Debug

  C:\!APP\nuget\nuget-3.2.1-rc.exe pack .\nuspec-packager.nuspec -OutputDirectory .\!NuGet-Out

  COPY .\!NuGet-Out\WeebreeOpen.SystemLib.0.0.2-beta.nupkg \\OBSOLETE
------------------------------------------------------------------------------------------------------------
> DEPLOY FROM
    D:\CODE\Git.Github\iwhp\weebreeopen\WeebreeOpen.SystemLib\!!Out-NuGet
  TO:
    https://www.nuget.org/packages/WeebreeOpen.SystemLib
============================================================================================================

============================================================================================================
> INSTALL VS EXTENSTION: NuGet Deploy
  > INSTALL https://visualstudiogallery.msdn.microsoft.com/455301f5-cc32-4261-b4da-998bd9a6a690
  > DOCU:   https://dl.dropboxusercontent.com/u/2557744/documentation.pdf
------------------------------------------------------------------------------------------------------------
> CHANGE VERSION INFORMATION (1.x.x) in the following files
  > AssemblyInfo.cs
  > Package.nuspec
------------------------------------------------------------------------------------------------------------
> Build Project (Release)
------------------------------------------------------------------------------------------------------------
> Right-Click VS Project
> NuGet Deploy...
============================================================================================================

============================================================================================================
LINKS
------------------------------------------------------------------------------------------------------------
http://www.codeguru.com/csharp/csharp/cs_internet/desktopapplications/article.php/c13163/Simple-FTP-Demo-Application-Using-CNET-20.htm
============================================================================================================

