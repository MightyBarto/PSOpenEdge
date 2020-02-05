using System.Collections.Generic;
using System.Management.Automation;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database.Custom
{
    /// <summary>
    /// Performa a Binary dump of the specified databases.
    /// </summary>
    // [Cmdlet(VerbsCustom.Repair, NounsCustom.Database)]
    public class DumpOeDatabaseCmdlet : DatabaseRelatedCmdlet
    {
        [Parameter]
        public string[] Tables { get; set; } = new[] { "*" };

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
            // 1. Generate a script file to dump specified tables when not all tables are selected.     
            //    Create support for wildcards.
            //    Allow multiple tablename specification, allowing wildcards for all.       
        }

        /// <summary>
        /// Launches a custom ABL script to load the table names from the connected db.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetTableNames()
        {
            return null;
        }


        private string BuildTableDumpCommand()
        {
            return "";
        }

    }

}