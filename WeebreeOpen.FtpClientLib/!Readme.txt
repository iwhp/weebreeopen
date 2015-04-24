============================================================================================================
DEPLOYMENT (NUGET PACKAGE)
------------------------------------------------------------------------------------------------------------
PREPARE NUGET:
> Download NuGet.exe from nuget.com
> Copy NuGet.exe to C:\!App\NuGet
> Register NuGet API-KEY (get the API key from nuget.com)
  nuget setApiKey 1a88b9ad-e068-4f5d-a81c-4f9819a9a35e
------------------------------------------------------------------------------------------------------------
PUSH BY COMMAND TO nuget.com
> C:\!App\NuGet\nuget push C:\!Data\Code\Git.Github\iwhp\weebreeopen\WeebreeOpen.FtpClientLib\!!Out-NuGet\WeebreeOpen.FtpClientLib.0.0.17-beta.nupkg
------------------------------------------------------------------------------------------------------------
DEPLOY BY COMMAND FROM
> https://www.nuget.org/packages/WeebreeOpen.FtpClientLib
  C:\!Data\Code\Git.Github\iwhp\weebreeopen\WeebreeOpen.FtpClientLib\!!Out-NuGet
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

