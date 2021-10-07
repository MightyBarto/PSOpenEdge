using System.Management.Automation;
using PSOpenEdge.Powershell;

namespace PSOpenEdge.Cmdlets.Database.Custom
{
    /// <summary>
    /// Returns table details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, NounsCustom.Tenant)]
    public class GetOeDatabaseTenants : DatabaseRelatedCmdlet
    {
        [Parameter]
        public string TenantName { get; set; } = "*";

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
                foreach (var table in this.GetDatabaseTenants(db, this.TempDirectory, this.IsSingleUser, this.TenantName))
                {
                    this.WriteObject(table);
                }
            }
        }
    }
}