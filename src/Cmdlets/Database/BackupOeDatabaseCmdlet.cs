using System.Management.Automation;
using System.Text;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCustom.Backup, NounsCustom.OeDatabase)]
    public class BackupOeDatabaseCmdlet : OeDatabaseRelatedCmdlet
    {
        [Parameter]
        public SwitchParameter Online { get; set; }

        [Parameter]
        public string DestinationPath { get; set; }

        [Parameter]
        public string DestinationName { get; set; }

        [Parameter]
        public SwitchParameter Incremental { get; set; }

        [Parameter, Alias("io"), ValidateSet("0", "1", "2")]
        public uint IncrementalOverlap { get; set; }

        [Parameter]
        public BiBackupTypes BiBackup { get; set; } = BiBackupTypes.None;

        [Parameter, Alias("Bp")]
        public uint PrivateBuffers { get; set; } = 64;

        [Parameter, Alias("com")]
        public SwitchParameter Compress { get; set; }

        [Parameter, Alias("red")]
        public uint Redundancy { get; set; } = 0;

        [Parameter, Alias("vs")]
        public uint VolumeSize { get; set; } = 0;

        [Parameter, Alias("bf"), ValidateRange(0, 1024)]
        public uint BlockingFactor { get; set; } = 0;

        [Parameter]
        public SwitchParameter NoRecover { get; set; }

        [Parameter]
        public SwitchParameter Scan { get; set; }

        [Parameter]
        public SwitchParameter Estimate { get; set; }

        #region After-Imaging

        [Parameter, Alias("enableai")]
        public SwitchParameter EnableAfterImaging { get; set; }

        [Parameter]
        public EnableTypes AiEncryption { get; set; }

        [Parameter, Alias("aiarcdircreate")]
        public SwitchParameter CreateAiArchiveDir { get; set; }

        [Parameter]
        public string EnableAiArchiver { get; set; }

        [Parameter, Alias("aiarcinterval")]
        public uint AiArchiverInterval { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            var args = new StringBuilder(100);

            if (this.Online)
                args.Append(" online");

            args.Append(" {0}"); // add the dbname placeholder

            if (this.Incremental)
                args.Append(" incremental");

            args.Append(" {1}"); // add the device-name placeholder.

            // General arguments
            if (this.BiBackup != BiBackupTypes.None)
                args.Append($" -bibackup {this.BiBackup}");
            if (this.Estimate)
                args.Append(" -estimate");
            if (this.Scan)
                args.Append(" -scan");
            if (this.VolumeSize > 0)
                args.Append($" -vs {this.VolumeSize}");
            if (this.BlockingFactor > 0)
                args.Append($" -bf {this.BlockingFactor}");
            if (this.Compress)
                args.Append(" -com");
            if (this.Redundancy > 0)
                args.Append($" -red {this.Redundancy}");
            if (this.NoRecover)
                args.Append(" -norecover");

            args.Append($" -Bp {this.PrivateBuffers}");
            args.Append(" -verbose");

            // After-imaging
            if (this.EnableAfterImaging)
                args.Append(" enableai");
            if (this.AiEncryption != EnableTypes.None)
                args.Append($" -aiencryption {this.AiEncryption}");
            if (!string.IsNullOrWhiteSpace(this.EnableAiArchiver))
                args.Append($" enableaiarchiver -aiarcdir {this.EnableAiArchiver}");
            if (this.CreateAiArchiveDir)
                args.Append(" -aiarcdircreate");
            if (this.AiArchiverInterval > 0)
                args.Append($" -aiarcinterval {this.AiArchiverInterval}");

            var theArgs = args.ToString();

            foreach (var db in this.GetDatabases())
            {
                var targetPath = string.IsNullOrWhiteSpace(this.DestinationPath)
                    ? db.Path
                    : this.DestinationPath;
                var targetFile = string.IsNullOrWhiteSpace(this.DestinationName)
                    ? db.Name + ".bkp"
                    : this.DestinationName;

                var target = System.IO.Path.Combine(targetPath, targetFile);

                var fullArgs = string.Format(theArgs, db.Name, target);

                this.WriteVerbose(OeCommands.ProBkup + " " + fullArgs);
                new OeCommand(OeCommands.ProBkup, this.Path).Run(fullArgs);
            }
        }
    }
}