using System.Management.Automation;
using PSOpenEdge.Powershell;
using PSOpenEdge.Common;

namespace PSOpenEdge.Cmdlets.Database.Custom
{
    /// <summary>
    /// Returns table details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, NounsCustom.Table)]
    public class GetOeTablesCmdlet : DatabaseRelatedCmdlet
    {
        [Parameter]
        public string TableFilter { get; set; } = "*";

        [Parameter]
        public string Destination { get; set; }

        [Parameter]
        public string TempDirectory { get; set; }

        // TODO: Accept pipe value of a OeTable object. which can be loaded by the Get-OeTables cmdlet...

        /// <summary>
        /// Executes the Cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (var db in this.GetDatabases())
            {
                // 1. Generate a script file to dump specified tables when not all tables are selected.     
                //    Create support for wildcards.
                //    Allow multiple tablename specification, allowing wildcards for all.       
                var scriptTemplate = TemplateFactory.LoadAblScript("src.Templates.ABL.GetTableInfo.p",
                        new[] { ("TargetFile", @"D:\temp\tables.json"),
                                ("TableFilter", this.TableFilter),
                                ("DatabaseName",db.Name)});

                this.WriteVerbose($"Script template: {scriptTemplate}");
            }
        }

        private string BuildTableDumpCommand()
        {
            return "";
        }

    }

}