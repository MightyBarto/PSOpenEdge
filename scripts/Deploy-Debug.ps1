if(Get-Module -Name PSOpenEdgeBuild){Remove-Module PSOpenEdgeBuild}
Import-Module ./scripts/PSOpenEdgeBuild.psm1 -DisableNameChecking

### Deploys all modules to the target folder ###

Copy-Item -Path /home/bart/dev/projects/PSOpenEdge/bin/Debug/netcoreapp3.0/*.dll -Destination  /mnt/d/Tools/Powershell/Modules/PSOpenEdge -Force -Verbose
