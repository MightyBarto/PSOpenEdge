using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Newtonsoft.Json;
using PSOpenEdge.Common;
using PSOpenEdge.OpenEdge;
using PSOpenEdge.OpenEdge.Database;

namespace PSOpenEdge.Cmdlets.Database
{
    public abstract class DatabaseRelatedCmdlet : PSOpenEdgeCmdletBase
    {
        [Parameter(ValueFromPipeline = true, ParameterSetName = ParamSetPipeline)]
        public OpenEdge.Database.Database FromPipeline { get; set; }

        private string _Path = null;
        [Parameter]
        public string Path
        {
            get
            {
                return this._Path ?? this.CurrentDirectory;
            }
            set { this._Path = value; }
        }

        private string _Name = null;
        [Parameter]
        public string Name
        {
            get { return this._Name ?? "*"; }
            set { this._Name = value; }
        }

        /// <summary>
        /// Returns all selected databases for this CmdLet.
        /// - Through pipeline
        /// - Through the 'Name' parameter (supports wildcards). 
        /// </summary>
        /// <returns>List of Databases</returns>
        protected virtual IEnumerable<OpenEdge.Database.Database> GetDatabases()
        {
            var db = this.FromPipeline;
            if (db != null)
            {
                yield return db;
                yield break;
            }

            var path = this.GetFullPath();
            var name = this.Name;

#if DEBUG
            Console.WriteLine($"Path: '{path}' Name: '{name}'");
#endif

            this.WriteVerbose($"Path: '{path}' Name: '{name}'");

            foreach (var database in DatabaseRelatedCmdlet.GetDatabases(path, name))
            {
                if (database == null)
                    continue;

                yield return database;
            }
        }

        /// <summary>
        /// Returns the databases in path with argument name.        
        /// </summary>
        /// <param name="path">The directory that the .db files reside in.</param>
        /// <param name="name">The name of the databases, supports wildcards.</param>
        /// <returns>a new <see cref="IEnumerable{T}"/></returns>
        protected static IEnumerable<OpenEdge.Database.Database> GetDatabases(string path, string name)
        {
            if (path == null)
                yield break;

            var dir = new System.IO.DirectoryInfo(path);
            if (!dir.Exists)
                yield break;

            if (!name.Equals("*", StringComparison.InvariantCultureIgnoreCase))
                name = $"{name}*";

            var wildCard = new WildcardPattern($"{name}");

            foreach (var file in dir.GetFiles($"*.db"))
            {
                var dbName = file.Name.Replace(".db", string.Empty);

                if (!wildCard.IsMatch(dbName))
                    continue;

                yield return new OpenEdge.Database.Database
                {
                    FileName = file.Name,
                    Path = dir.FullName,
                    Name = dbName
                };
            }
        }

        /// <summary>
        /// Loads the requested tables from argument database.
        /// Allows for a lambda expression to apply custom filtering.
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<DatabaseTable> GetDatabaseTables(OpenEdge.Database.Database db, string tempDir = null, bool isSingleUser = false, string tableName = "*")
        {
            // Use a cumstom procedure to get the tables in the database
            // This procedure will return all (non VST) tables in the database.
            // It is up to this method to apply filtering.
            var tables = this.CustomProcedure<DatabaseTableJson>(db, "DumpTables.p", tempDir, isSingleUser);

            // Initialize the wildcard pattern.
            if (!tableName.EndsWith("*"))
                tableName = tableName + "*";

            var wildcard = new WildcardPattern(tableName);

            // Loop the tables, assign the databbase, and yield each table.
            foreach (var table in tables.Tables)
            {
                table.Database = db;

                if (!wildcard.IsMatch(table.Name))
                    continue;

                yield return table;
            }
        }

        private T CustomProcedure<T>(PSOpenEdge.OpenEdge.Database.Database db, string procedure, string tempDir = null, bool isSingleUser = false)
        {
            db = db ?? throw new ArgumentNullException(nameof(db));

            if (string.IsNullOrWhiteSpace(procedure))
                throw new ArgumentNullException(nameof(db));

            // Get the appropriate procedure. Which is an embedded resource.
            // AblProcedureFactory will place it in the temp dir and return the full path.
            var ablProc = AblProcedureFactory.GetAblProcedure(procedure);
            var guid = Guid.NewGuid();
            var path = this.GetFullPath();
            tempDir = string.IsNullOrWhiteSpace(tempDir)
                        ? OeEnvironment.WRK
                        : tempDir;
            var dumpFile = System.IO.Path.Combine(tempDir, $"{guid}.{db.Name}.json");

            try
            {
                // command to be executed:
                // (m)bpro -db <db> -p <proc> -param "<guid>" -T <temp>    
                var command = isSingleUser
                                ? OeCommands.Bpro
                                : OeCommands.Mbpro;

                // Run custom procedure. Which will dump data in a file according to the guid.
                // will save as follows: <temp>\<guid>.<dbname>.tables.json                
                new OeCommand(command, path, isSilent: !this.MyInvocation.BoundParameters.ContainsKey("Verbose")).Run($"-db {db.Name} -p {ablProc} -param \"{guid}\" -T {tempDir}");

                // Read the generated file into an object    
                // The file where the .p will dump the tables                
                var json = File.ReadAllText(dumpFile);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return default(T);
            }
            finally
            {
                if (File.Exists(dumpFile))
                    File.Delete(dumpFile);
            }
        }

        /// <summary>
        /// Returns the fully qualified path for this.Path.
        /// </summary>
        /// <returns>The fully qualified path.</returns>
        protected string GetFullPath()
        {
            var path = this.ParameterSetName == ParamSetPipeline
                        ? this.FromPipeline.Path
                        : this.Path;

            path = this.ToFullPath(path);

#if DEBUG
            Console.WriteLine($"ParameterSet: '{this.ParameterSetName}'");
            Console.WriteLine($"Path set on cli: '{this._Path}'");
            Console.WriteLine($"CurrentDirectory: '{this.CurrentDirectory}'");
            Console.WriteLine($"Used path: '{path}'");
#endif           

            return new DirectoryInfo(path).FullName;
        }

        protected string ToFullPath(string relativePath) =>
            System.IO.Path.IsPathRooted(relativePath)
                ? relativePath
                : System.IO.Path.Combine(this.CurrentDirectory, relativePath);
    }
}
