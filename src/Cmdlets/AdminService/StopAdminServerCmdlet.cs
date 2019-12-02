using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Stop, NounsCustom.AdminServer)]
    public class StopAdminServerCmdlet : OeAdminServerRelatedCmdletBase
    {
        protected override void ProcessRecord()
        {
            new OeCommand(OeCommands.ProAdsv).Run("-stop");
        }
    }
}
