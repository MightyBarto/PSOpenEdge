using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCommon.Get, NounsCustom.AppServer)]
    public class GetAppServerCmdlet : OeAppserverRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            var args = $"-query {this.GetAppServerNameArgs()}";

            this.WriteVerbose($"{OeCommands.Asbman} {args}");

            new OeCommand(OeCommands.Asbman).Run(args);
        }
    }
}
