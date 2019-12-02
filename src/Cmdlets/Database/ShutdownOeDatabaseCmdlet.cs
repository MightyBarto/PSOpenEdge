using System.Management.Automation;
using System.Text;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database
{
    [Cmdlet(VerbsCustom.Shutdown, NounsCustom.OeDatabase)]
    public class ShutdownOeDatabaseCmdlet : OeDatabaseRelatedCmdlet
    {
        [Parameter, Alias("type")]
        public DatabaseShutdownTypes ShutdownType { get; set; } = DatabaseShutdownTypes.Unconditional;

        [Parameter]
        public string HostName { get; set; } = string.Empty;

        [Parameter]
        public string ServiceName { get; set; } = string.Empty;

        [Parameter, Alias("totype")]
        public DatabaseShutdownTimeoutTypes TimeoutType { get; set; } = DatabaseShutdownTimeoutTypes.None;

        [Parameter, Alias("to")]
        public int Timeout { get; set; } = 10;

        [Parameter, Alias("cpinternal")]
        public string CodePageInternal { get; set; }

        [Parameter, Alias("cpstream")]
        public string CodePageStream { get; set; }

        protected override void ProcessRecord()
        {       
            var suffixArguments = new StringBuilder(this.ShutdownType.GetArgument(), 50);       

            if (!string.IsNullOrWhiteSpace(this.HostName))
                suffixArguments.Append(" -H ").Append(this.HostName);

            if (!string.IsNullOrWhiteSpace(this.ServiceName))
                suffixArguments.Append(" -S ").Append(this.ServiceName);

            if (this.TimeoutType != DatabaseShutdownTimeoutTypes.None)
            {
                suffixArguments.Append(" -shutdownTimeout");

                switch (this.TimeoutType)
                {
                    case DatabaseShutdownTimeoutTypes.Immediately:
                        suffixArguments.Append(" immed");
                        break;
                    case DatabaseShutdownTimeoutTypes.Maximum:
                        suffixArguments.Append(" maximum");
                        break;
                    case DatabaseShutdownTimeoutTypes.Hours:
                        suffixArguments.Append(" nh ").Append(this.Timeout);
                        break;
                    case DatabaseShutdownTimeoutTypes.Minutes:
                        suffixArguments.Append(" nm ").Append(this.Timeout);
                        break;
                    case DatabaseShutdownTimeoutTypes.Seconds:
                        suffixArguments.Append(" ns ").Append(this.Timeout);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(this.CodePageInternal))
                suffixArguments.Append(" -cpinternal ").Append(this.CodePageInternal);

            if (!string.IsNullOrWhiteSpace(this.CodePageStream))
                suffixArguments.Append(" -cpstream").Append(this.CodePageStream);

            foreach (var db in this.GetDatabases())
            {             
                this.WriteVerbose($"{OeCommands.ProShut} {db.Name} {suffixArguments}");  

                new OeCommand(OeCommands.ProShut, this.Path).Run($"{db.Name} {suffixArguments}");
            }            
        }
    }    
}
