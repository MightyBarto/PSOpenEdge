using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCommon.Get, NounsCustom.User)]
    public class GetUsersCmdlet : DatabaseRelatedCmdlet
    {
        protected override void ProcessRecord()
        {
            var path = this.GetFullPath();

            foreach (var db in this.GetDatabases())
            {
                this.WriteVerbose($"List connected users for {db.Name}");

                new OeCommand(OeCommands.ProShut, path).Run($"{db.Name} -C list");
            }
        }
    }
}