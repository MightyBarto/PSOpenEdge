using System.Management.Automation;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCommon.Get,NounsCustom.Database)]
    public class GetDatabaseCmdlet : DatabaseRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObject(this.GetDatabases(), true);
        }
    }
}