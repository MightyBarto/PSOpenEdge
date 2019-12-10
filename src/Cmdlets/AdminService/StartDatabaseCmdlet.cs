using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Start, NounsCustom.Database)]
    public class StartDatabaseCmdlet : AdminServerRelatedCmdletBase
    {
        [Parameter(ParameterSetName = "ByName", Mandatory = true)]
        public string Name { get; set; }

        [Parameter(ParameterSetName = "All", Mandatory = true)]
        public bool All { get; set; }

        protected override void ProcessRecord()
        {
            var args = this.All 
                ? "-start -all"
                : $"-start -name {this.Name}";

            new OeCommand(OeCommands.DbMan).Run(args);
        }
    }
}