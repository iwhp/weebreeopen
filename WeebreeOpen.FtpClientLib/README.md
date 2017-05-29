# DEPLOY NUGET PACKAGE

## PREPARE NUGET:

- Download nuget.exe from nuget.org (save in: C:\APP\NuGet)
- Register NuGet API-KEY (get the API key from nuget.com)

      nuget setApiKey [API-KEY]

## CHANGE VERSION INFORMATION (x.x.x) in the following files

- AssemblyInfo.cs
- WeebreeOpen.FtpClientLib.nuspec

## Build Project (Release)

- Build the project with the Release configuration

## CREATE THE PACKAGE

    D:
    CD \CODE\Git.Github\iwhp\weebreeopen\WeebreeOpen.FtpClientLib
    C:\APP\NuGet\nuget pack -Prop Platform=AnyCPU -Prop Configuration=Release


## PUSH PACKAGE TO nuget.com

    D:
    CD \CODE\Git.Github\iwhp\weebreeopen\WeebreeOpen.FtpClientLib
    C:\APP\NuGet\nuget push WeebreeOpen.FtpClientLib.0.0.21-beta.nupkg -Source https://www.nuget.org/api/v2/package


# LINKS

- http://www.codeguru.com/csharp/csharp/cs_internet/desktopapplications/article.php/c13163/Simple-FTP-Demo-Application-Using-CNET-20.htm
- https://visualstudiogallery.msdn.microsoft.com/455301f5-cc32-4261-b4da-998bd9a6a690
- https://dl.dropboxusercontent.com/u/2557744/documentation.pdf
