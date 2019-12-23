using System.Linq;
using System.Management.Automation;
using System.Text;
using PSOpenEdge.Powershell;
using PSOpenEdge.OpenEdge;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCommon.Copy, NounsCustom.Database)]
    public class CopyDatabaseCmdlet : DatabaseRelatedCmdlet
    {
        [Parameter, Alias("dpath")]
        public string DestinationPath { get; set; }

        [Parameter, Alias("dname")]
        public string DestinationName { get; set; }

        [Parameter]
        public SwitchParameter Silent { get; set; }

        [Parameter, Alias("newguid")]
        public SwitchParameter NewInstance { get; set; }

        [Parameter, Alias("copyst")]
        public SwitchParameter CopyStructureFile { get; set; }

        protected override void ProcessRecord()
        {
            // Resolve source path
            // Keep in mind that for the procopy command, the sourcepath will not be set as working directory, instead the 
            // working directory will be the destination path
            var sourcePath = this.GetFullPath();

            //Resolve target path
            //When Destination is omitted, Set it to current directory
            var destination = this.DestinationPath ?? this.CurrentDirectory;
            destination = this.ToFullPath(destination);

            //Get the databases
            var databases = this.GetDatabases();

            //When copying only one db -> targetname is usable. Otherwise it has to be omitted.
            var targetName = databases.Count() == 1
                ? this.DestinationName
                : null;

            this.WriteVerbose($"SourcePath: '{sourcePath}' ");
            this.WriteVerbose($"TargetPath:'{destination}'");

            var suffixBuilder = new StringBuilder(30);
            if (this.NewInstance)
                suffixBuilder.Append(" -newinstance");

            if (this.Silent)
                suffixBuilder.Append(" -silent");

            //Process
            foreach (var db in databases)
            {
                //Determine source and target
                var sourceDb = System.IO.Path.Combine(sourcePath, db.Name);
                var targetDb = string.IsNullOrWhiteSpace(targetName)
                    ? db.Name
                    : targetName;

                if (this.CopyStructureFile)
                {
                    var targetSt = System.IO.Path.Combine(destination, targetDb + ".st");

                    this.WriteVerbose($"Copy {sourceDb}.st to {targetSt}");
                    System.IO.File.Copy(sourceDb + ".st", targetSt, true);
                }

                this.WriteVerbose($"[COPY] {sourceDb} => {targetDb}");

                //Execute command.
                new OeCommand(OeCommands.ProCopy, destination).Run($"{sourceDb} {targetDb} {suffixBuilder}");
            }
        }
    }

}
