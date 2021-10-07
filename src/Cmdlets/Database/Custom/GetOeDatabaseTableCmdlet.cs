using System.Management.Automation;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database.Custom
{
    /// <summary>
    /// Returns table details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, NounsCustom.Table)]
    public class GetOeDatabaseTableCmdlet : DatabaseRelatedCmdlet
    {
        [Parameter]
        public string TableName { get; set; } = "*";

        [Parameter]
        public string TempDirectory { get; set; }

        [Parameter]
        public SwitchParameter IsSingleUser { get; set; }

        /// <summary>
        /// Executes the Cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (var db in this.GetDatabases())
            {
                foreach (var table in this.GetDatabaseTables(db, this.TempDirectory, this.IsSingleUser, this.TableName))
                {
                    this.WriteObject(table);
                }
            }
        }
    }
}