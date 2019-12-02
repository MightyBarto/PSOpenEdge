using System.Management.Automation;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCommon.Get,NounsCustom.OeDatabase)]
    public class GetOeDatabaseCmdlet : OeDatabaseRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObject(this.GetDatabases(), true);
        }
    }
}