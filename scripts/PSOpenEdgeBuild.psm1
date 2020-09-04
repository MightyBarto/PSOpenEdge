

function Get-Projects
{
    $projects = Get-ChildItem -Filter "*.csproj" -File;

    foreach($p in $projects)
    {
        Write-Output $p;
    }
}

Export-ModuleMember -Function Get-Projects;