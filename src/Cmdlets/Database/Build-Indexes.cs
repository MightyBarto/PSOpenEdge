
using System.Management.Automation;
using System.Text;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{

    [Cmdlet(VerbsCustom.Build, NounsCustom.Index)]
    public class BuildIndexesCmdlet : OeDatabaseRelatedCmdlet
    {
        [Parameter]
        public IndexBuildTypes BuildTypes { get; set; } = IndexBuildTypes.All;

        [Parameter]
        public string TableName { get; set; }

        protected override void ProcessRecord()
        {
            var path = this.GetFullPath();

            this.WriteVerbose($"Working directory {path}");

            var args = new StringBuilder("-C idxbuild", 50);

            // Add the buildType
            args.Append(' ').Append(this.BuildTypes.GetArgument());

            // for buildtype 'table', also add the table.
            if (this.BuildTypes == IndexBuildTypes.Table)
                args.Append(' ').Append(this.TableName);

            foreach (var db in this.GetDatabases())
            {
                this.WriteVerbose($"{OeCommands.ProUtil} {db.Name} {args}");
                new OeCommand(OeCommands.ProUtil, path).Run($"{db.Name} {args}");
            }

        }
    }

}