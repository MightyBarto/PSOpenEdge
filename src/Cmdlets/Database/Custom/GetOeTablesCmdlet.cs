using System.Management.Automation;
using PSOpenEdge.Powershell;
using PSOpenEdge.OpenEdge;
using System;
using PSOpenEdge.Common;
using System.IO;
using Newtonsoft.Json;
using PSOpenEdge.OpenEdge.Model;

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
        public SwitchParameter IsSingleUser {get;set;}

        /// <summary>
        /// Executes the Cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            // determine the correct Temp directory, if omitted, use wrk.
            var tempDir = string.IsNullOrWhiteSpace(this.TempDirectory)
                            ? OeEnvironment.WRK
                            : this.TempDirectory;

            // Get the appropriate procedure. Which is an embedded resource.
            // AblProcedureFactory will place it in the temp dir and return the full path.
            var ablProc = AblProcedureFactory.GetAblProcedure("DumpTables.p");
            var guid = Guid.NewGuid();
            var path = this.GetFullPath();

            // Prapare pattern matching using a WildcardPattern.
            var tableName = this.TableName.Equals("*", StringComparison.InvariantCultureIgnoreCase)
                                ? this.TableName
                                : $"{this.TableName}*";
            var wildcard = new WildcardPattern(tableName);            

            foreach (var db in this.GetDatabases())
            {                               
                // command to be executed:
                // mbpro -db <db> -p <proc> -param "<guid>" -T <temp>    

                var command = this.IsSingleUser 
                                ? OeCommands.Bpro 
                                : OeCommands.Mbpro;

                // Dump the tables to json file to the temp dir
                // will save as follows: <temp>\<guid>.<dbname>.tables.json                
                new OeCommand(command, path).Run($"-db {db.Name} -p {ablProc} -param \"{guid}\" -T {tempDir}");

                // Read the generated file into an object    
                // The file where the .p will dump the tables
                var tablesFile = System.IO.Path.Combine(tempDir,$"{guid}.{db.Name}.tables.json");                
                var tables = default(DatabaseTableJson);

                try
                {
                    var tablesJson = File.ReadAllText(tablesFile);
                    tables = JsonConvert.DeserializeObject<DatabaseTableJson>(tablesJson);
                }
                finally
                {
                    if(File.Exists(tablesFile))
                        File.Delete(tablesFile);
                }
                
                foreach(var table in tables.Tables)
                {
                    if(!wildcard.IsMatch(table.Name))
                        continue;

                    //Console.WriteLine(table.Name);
                    this.WriteObject(table);
                }
            }            
        }
    }
}