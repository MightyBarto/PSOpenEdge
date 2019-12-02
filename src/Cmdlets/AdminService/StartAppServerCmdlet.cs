using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Start, NounsCustom.AppServer)]
    public class StartAppServerCmdlet : OeAppserverRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            var args = $"-start {this.GetAppServerNameArgs()}";

            this.WriteVerbose($"{OeCommands.Asbman} {args}");

            new OeCommand(OeCommands.Asbman).Run(args);
        }
    }
}
