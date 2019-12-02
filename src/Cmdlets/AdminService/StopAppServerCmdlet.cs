using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Stop, NounsCustom.AppServer)]
    public class StopAppServerCmdlet : OeAppserverRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            var args = $"-stop {this.GetAppServerNameArgs()}";

            this.WriteVerbose($"{OeCommands.Asbman} {args}");

            new OeCommand(OeCommands.Asbman).Run(args);
        }
    }
}
