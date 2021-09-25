
param(
    [Switch] $Release = $false
)

# Definitions
$version = "2.0.0.1"
$outputDir = "/mnt/d/Tools/Powershell/Modules/PSOpenEdge"

# Perform build 

if ($Release)
{
    Write-Host "Performing RELEASE build";
    dotnet build -c release PSOpenEdge.sln -p:Version=$version
    dotnet publish -c release ./PSOpenEdge.sln -o $outputDir
}
else
{    
    Write-Host "Performing DEBUG build"
    dotnet build PSOpenEdge.sln -p:Version=$version
    dotnet publish ./PSOpenEdge.sln -o $outputDir
}