if(Get-Module -Name PSOpenEdgeBuild){Remove-Module PSOpenEdgeBuild}
Import-Module ./scripts/PSOpenEdgeBuild.psm1 -DisableNameChecking

### Updates all nuget packages needed by this project ###

Get-Projects | ForEach-Object {    
    dotnet add $_.Name package Newtonsoft.Json;
    dotnet add $_.Name package System.Management.Automation;
}
