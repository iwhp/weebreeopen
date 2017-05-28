# DEPLOY NUGET PACKAGE

## PREPARE NUGET:

- Download NuGet.exe from nuget.com
- Copy NuGet.exe to C:\!App\NuGet
- Register NuGet API-KEY (get the API key from nuget.com)

      nuget setApiKey [API-KEY]

## CHANGE VERSION INFORMATION (x.x.x) in the following files

- AssemblyInfo.cs
- Package.nuspec
- Build Project (Release)

## PUSH PACKAGE TO nuget.com

    C:\!App\NuGet\nuget push C:\!Data\Code\Git.Github\iwhp\weebreeopen\WeebreeOpen.FtpClientLib\!!Out-NuGet\WeebreeOpen.FtpClientLib.0.0.17-beta.nupkg

# LINKS

- http://www.codeguru.com/csharp/csharp/cs_internet/desktopapplications/article.php/c13163/Simple-FTP-Demo-Application-Using-CNET-20.htm
- https://visualstudiogallery.msdn.microsoft.com/455301f5-cc32-4261-b4da-998bd9a6a690
- https://dl.dropboxusercontent.com/u/2557744/documentation.pdf
