using OpenEdge.DataAdmin.* from propath. 
using OpenEdge.DataAdmin.Lang.Collections.* from propath. 

&scoped-define TargetFile  [[TargetFile]]
&scoped-define TableFilter [[TableFilter]]
&scoped-define DatabaseName [[DatabaseName]]

define variable oDataAdminService as DataAdminService no-undo. 
define variable oTableSet 			  as ITableSet no-undo. 
define variable oTable			  		as ITable no-undo. 
define variable oTableIterator 		as IIterator no-undo.

define variable cTableFilter as character no-undo.
define variable cTargetFile  as character no-undo.

define temp-table TableInfoTT no-undo  	
  field TableName     as character serialize-name "name"
  field IsMultiTenant as logical   serialize-name "isMultiTenant"

  index Pk is primary unique TableName.

/* Body */

assign cTableFilter = "{&TableFilter}"
       cTargetFile  = "{&TargetFile}".

if cTableFilter eq ?  or
   cTableFilter eq "[[TableFilter]]"
then
  assign cTableFilter = "*".

if cTargetFile eq ? or
   cTargetFile eq "[[TargetFile]]"
then
  assign cTargetFile = "*".   

assign oDataAdminService = new DataAdminService("{&DatabaseName}")
       oTableSet      	 = oDataAdminService:GetTables()
       oTableIterator 	 = oTableSet:Iterator().
     //  oTenants = service:GetTenants()
     //  oIterTenant = oTenants:Iterator().

Loop-Tables:
do while oTableIterator:HasNext():
  assign oTable = cast(oTableIterator:Next(),ITable).
  
  if not oTable:Name matches cTableFilter
  then
  	next Loop-Tables.
  	
  create TableInfoTT.
  assign TableInfoTT.TableName     = oTable:Name
         TableInfoTT.IsMultiTenant = oTable:IsMultitenant.
  release TableInfoTT.
  
end.

temp-table TableInfoTT:write-json("FILE",
                                  "{&TargetFile}",
                                  true,
                                  "UTF-8",
                                  true,
                                  true).

quit.
