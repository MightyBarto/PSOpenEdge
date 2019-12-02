using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Start, NounsCustom.AdminServer)]
    public class StartAdminServerCmdlet : OeAdminServerRelatedCmdletBase
    {
        protected override void ProcessRecord()
        {
            new OeCommand(OeCommands.ProAdsv).Run("-start");
        }
    }
}
