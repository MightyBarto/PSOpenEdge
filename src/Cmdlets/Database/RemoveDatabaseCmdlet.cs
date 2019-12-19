using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCommon.Remove, NounsCustom.Database)]
    public class RemoveDatabaseCmdlet : DatabaseRelatedCmdlet
    {
        [Parameter, Alias("y","force","confirm")]
        public SwitchParameter NoConfirmationRequired { get; set; }

        protected override void ProcessRecord()
        {
            var command = new OeCommand(OeCommands.ProDel, this.GetFullPath());            

            if(this.NoConfirmationRequired)
                command.CustomInputs.Add("y");

            foreach (var db in this.GetDatabases())
            {
                command.Run(db.Name);
            }            
        }
    }
}