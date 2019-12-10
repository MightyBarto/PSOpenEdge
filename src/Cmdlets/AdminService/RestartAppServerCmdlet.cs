using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Restart, NounsCustom.AppServer)]
    public class RestartAppServerCmdlet : AppserverRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            var command = new OeCommand(OeCommands.Asbman);
            var appServerNameArgs = this.GetAppServerNameArgs();

            // Stop the AppServer(s)
            var args = $"-stop {appServerNameArgs}";
            this.WriteVerbose($"{OeCommands.Asbman} {args}");
            command.Run(args);

            // Start the AppServer(s)
            args = $"-start {appServerNameArgs}";
            this.WriteVerbose($"{OeCommands.Asbman} {args}");
            command.Run(args);
        }
    }
}
