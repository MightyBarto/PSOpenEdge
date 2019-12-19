
using System.Management.Automation;
using System.Text;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{

    [Cmdlet(VerbsCustom.Build, NounsCustom.Index)]
    public class BuildIndexesCmdlet : DatabaseRelatedCmdlet
    {
        [Parameter]
        public IndexBuildTypes BuildTypes { get; set; } = IndexBuildTypes.All;

        [Parameter]
        public string TableName { get; set; }

        [Parameter]
        public AiToggleModes AiToggleMode { get; set; } = AiToggleModes.None;

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
                // Switch off ai when requested         
                if(this.AiToggleMode != AiToggleModes.None)
                {
                    this.WriteVerbose($"Disable after-imaging for {db.Name}");
                    new OeCommand(OeCommands.RfUtil, path).Run($"{db.Name} -C aimage end");
                }

                this.WriteVerbose($"{OeCommands.ProUtil} {db.Name} {args}");
                new OeCommand(OeCommands.ProUtil, path).Run($"{db.Name} {args}");

                if(this.AiToggleMode == AiToggleModes.OffAndOn)
                {
                    this.WriteVerbose($"Marking {db.Name} as backed up. Please backup this database.");
                    new OeCommand(OeCommands.RfUtil, path).Run($"{db.Name} -C mark backedup");
                    this.WriteVerbose($"Enable after-imaging for {db.Name}");
                    new OeCommand(OeCommands.RfUtil, path).Run($"{db.Name} -C aimage begin");              
                }
            }
        }
    }
}