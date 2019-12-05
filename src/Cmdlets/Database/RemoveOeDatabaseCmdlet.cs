using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCommon.Remove, NounsCustom.Database)]
    public class RemoveOeDatabaseCmdlet : OeDatabaseRelatedCmdlet
    {
        [Parameter, Alias("y")]
        public SwitchParameter NoConfirmation { get; set; }

        protected override void ProcessRecord()
        {
            var command = new OeCommand(OeCommands.ProDel, this.GetFullPath());            

            if(this.NoConfirmation)
                command.CustomInputs.Add("y");

            foreach (var db in this.GetDatabases())
            {
                command.Run(db.Name);
            }            
        }
    }
}