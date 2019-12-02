using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.AdminService
{
    [Cmdlet(VerbsCustom.Stop, NounsCustom.OeDatabase)]
    public class StopOeDatabaseCmdlet : OeAdminServerRelatedCmdletBase
    {
        [Parameter(ParameterSetName = "ByName", Mandatory = true)]
        public string Name { get; set; }

        [Parameter(ParameterSetName = "All", Mandatory = true)]
        public bool All { get; set; }

        protected override void ProcessRecord()
        {
            var args = this.All 
                ? "-stop -all"
                : $"-stop -name {this.Name}";

            new OeCommand(OeCommands.DbMan).Run(args);
        }
    }
}