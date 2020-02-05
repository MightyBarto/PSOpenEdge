&scoped-define scriptdir .\temp\scripts
&scoped-define dumpdir .\temp\dump\data
&scoped-define dbold .\dbold
&scoped-define dbnew .\dbnew

define variable cDump as character no-undo.

/* Body */

define stream sOutput.

output stream sOutput to "{&scriptdir}\TableNames.txt".

Loop-File:
for each _file
  where _file-num gt 0 and _file-num lt 32768:
  
  assign cDump = _file._file-name.
  put stream sOutput unformatted cDump skip.
end.

output stream sOutput close.
