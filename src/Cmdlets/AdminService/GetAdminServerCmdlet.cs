using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCommon.Get, NounsCustom.AdminServer)]
    public class GetAdminServerCmdlet : OeAdminServerRelatedCmdletBase
    {
        protected override void ProcessRecord()
        {
            new OeCommand(OeCommands.ProAdsv).Run("-query");
        }
    }
}
