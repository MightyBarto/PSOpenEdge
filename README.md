# PSOpenEdge
PowerShell Core tools for OpenEdge

## Disclaimer
I do not claim any kind of ownership of any of the official Progress/OpenEdge products.
The purpose of these tools is aiding in development. I strongly encourage/recommend using only
the official Progress/OpenEdge tools in a production environment.

I don't commit to make this tool support every aspect/feature of the OpenEdge ecosystem. 
I have so far only implemented features that I find useful myself. 
Though I will consider requests, I make no commitments of any kind.

See also the LICENSE file.

## Contains tools for following OpenEdge versions
- 11.5
- 11.7
- 12.2

## Features

Emphasis is on productivity. 
I try to achieve this the following ways:
* Lauch commands against multiple targets. For example all database related commands use the following selectors:
  ** -Path: The Path/Directory/Folder in which to search for databases
  ** -Name: The name of the database. supports wildcards.
* Convenient default values. For example:
  ** When not specifying the -Path parameter the current directory is used.
  ** When not specifying the -Name parameter, * is used, selecting all databases in -Path.
* Location-free operation. 
  You can lauch commands anywhere. The Cmdlets will set the correct working directory if needed. 
  Example: 
  The following command: Repair-OeDatabase -Path C:\Database 
  Will perform a prostrct repair on all databases in the C:\Database directory regardless of the current working directory.




