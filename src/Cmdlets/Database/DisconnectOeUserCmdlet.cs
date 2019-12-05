using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCustom.Disconnect, NounsCustom.User)]
    public class DisconnectOeUserCmdlet : OeDatabaseRelatedCmdlet
    {
        [Parameter(Mandatory = true)]
        public int UserNumber { get; set; }

        protected override void ProcessRecord()
        {
            var path = this.GetFullPath();

            foreach (var db in this.GetDatabases())
            {
                this.WriteVerbose($"Disconnect user '{this.UserNumber}' from db {db.Name}");
                new OeCommand(OeCommands.ProShut, path).Run($"{db.Name} -C disconnect {this.UserNumber}");
            }
        }
    }
}