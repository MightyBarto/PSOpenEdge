
param(
    [Switch] $release = $false
)

# Definitions
$version = "2.0.0.1";

# Perform build 

if ($release)
{
    Write-Host "Performing RELEASE build";
    dotnet build -c release PSOpenEdge.sln -p:Version=$version;
}
else
{    
    Write-Host "Performing DEBUG build"
    dotnet build PSOpenEdge.sln -p:Version=$version;
}