using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Restart, NounsCustom.OeDatabase)]
    public class RestartOeDatabaseCmdlet : OeAdminServerRelatedCmdletBase
    {
        [Parameter(ParameterSetName = "ByName", Mandatory = true)]
        public string Name { get; set; }

        [Parameter(ParameterSetName = "All", Mandatory = true)]
        public bool All { get; set; }

        protected override void ProcessRecord()
        {
            var command = new OeCommand(OeCommands.DbMan);
            var nameArgs = this.All ? " -all" : $" -name {this.Name}";

            // Stop the database(s)
            this.WriteVerbose($"Stop {nameArgs}");
            command.Run($"-stop {nameArgs}");
            
            // Start the database(s)
            this.WriteVerbose($"Start {nameArgs}");
            command.Run($"-start {nameArgs}");
        }
    }
}