using System.Linq;
using System.Management.Automation;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCustom.Repair, NounsCustom.OeDatabase)]
    public class RepairOeDatabaseCmdlet : OeDatabaseRelatedCmdlet
    {
        #region --- Parameters ---

        [Parameter, Alias("stfile")]
        public string StructureFile { get; set; } = null;

        #endregion --- Parameters ---


        protected override void ProcessRecord()
        {
            var path = this.GetFullPath();
            var databases = this.GetDatabases();

            this.WriteVerbose($"Working directory {path}");

            var multiDb = databases.Count() > 1;

            foreach (var db in databases)
            {
                if (multiDb || string.IsNullOrWhiteSpace(this.StructureFile))
                {
                    this.WriteVerbose($"{OeCommands.ProStrct} repair {db.Name}");
                    new OeCommand(OeCommands.ProStrct, path).Run($"repair {db.Name}");
                    continue;
                }
               
                this.WriteVerbose($"{OeCommands.ProStrct} repair {db.Name} {this.StructureFile}");
                new OeCommand(OeCommands.ProStrct, path).Run($"repair {db.Name} {this.StructureFile}");
            }
        }
    }
}
