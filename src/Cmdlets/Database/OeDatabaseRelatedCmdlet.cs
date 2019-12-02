using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using PSOpenEdge.OpenEdge.Database;

namespace PSOpenEdge.Cmdlets.Database
{
    public abstract class OeDatabaseRelatedCmdlet : PSOpenEdgeCmdletBase
    {
        #region --- Parameters ---

        [Parameter(ValueFromPipeline = true, ParameterSetName = ParamSetPipeline)]
        public OeDatabase FromPipeline { get; set; }

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

        #endregion --- Parameters ---

        /// <summary>
        /// Returns all selected databases for this CmdLet.
        /// - Through pipeline
        /// - Through the 'Name' parameter (supports wildcards). 
        /// </summary>
        /// <returns>List of Databases</returns>
        protected virtual IEnumerable<OeDatabase> GetDatabases()
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

            foreach (var database in OeDatabaseRelatedCmdlet.GetDatabases(path, name))
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
        protected static IEnumerable<OeDatabase> GetDatabases(string path, string name)
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

                yield return new OeDatabase
                {
                    FileName = file.Name,
                    Path = dir.FullName,
                    Name = dbName
                };
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
