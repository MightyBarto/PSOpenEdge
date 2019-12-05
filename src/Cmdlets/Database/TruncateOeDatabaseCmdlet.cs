using System.Management.Automation;
using System.Text;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{    
    [Cmdlet(VerbsCustom.Truncate, NounsCustom.Database)]
    public class TruncateOeDatabaseCmdlet : OeDatabaseRelatedCmdlet
    {
        #region --- Parameters ---

        [Parameter, Alias("G")]
        public uint? Wait { get; set; } = null;

        [Parameter, Alias("bi"), ValidateRange(16, 262128)]
        public uint? ClusterSize { get; set; } = null;

        [Parameter, Alias("biblocksize"), ValidateSet("0", "1", "2", "4", "8", "16")]
        public uint? BlockSize { get; set; } = null;

        #endregion --- Parameters ---

        protected override void ProcessRecord()
        {
            var path = this.GetFullPath();
            var databases = this.GetDatabases();

            this.WriteVerbose($"Working directory {path}");

            var args = new StringBuilder("-C truncate bi", 100);

            if (this.Wait != null)
                args.Append($" -G {this.Wait}");

            if (this.ClusterSize != null)
                args.Append($" -bi {this.ClusterSize}");

            if (this.BlockSize != null)
                args.Append($" -biblocksize {this.BlockSize}");

            foreach (var db in databases)
            {
                this.WriteVerbose($"{OeCommands.ProUtil} {db.Name} {args}");               
                new OeCommand(OeCommands.ProUtil, path).Run($"{db.Name} {args}");
            }            
        }

    }
}