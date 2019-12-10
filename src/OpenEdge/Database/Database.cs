using System;

namespace PSOpenEdge.OpenEdge.Database
{
    public class Database
    {
        #region --- Properties ---

        public string Path { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }

        #endregion 

        #region --- Constructors ---

        public Database()
        { }

        public Database(string dbFile)
        {
            if (dbFile == null)
                throw new ArgumentNullException(nameof(dbFile));
            if (!dbFile.EndsWith(".db"))
                throw new ArgumentException("The db file must end with '.db'");

            this.Name = dbFile.Substring(0, dbFile.Length - 2);
        }

        #endregion

        #region --- Methods ---

        public override string ToString()
        {
            return this.Name;
        }

        #endregion 
    }
}
