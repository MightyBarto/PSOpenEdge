namespace PSOpenEdge.Powershell
{
    /// <summary>
    /// Defines the Nouns used by these cmdlets.
    /// I chose to use the 'Oe' prefix for every noun in attempt
    /// to minimize naming collisions with other Modules.
    /// </summary>
    internal static class NounsCustom
    {
        public const string AdminServer = "OeAdminServer";
        public const string AppServer = "OeAppServer";
        public const string Database = "OeDatabase";
        public const string Table = "OeDatabaseTable";
        public const string Backup = "OeBackup";
        public const string Environment = "OeEnvironment";
        public const string Version = "OeVersion";
        public const string User = "OeUser";
        public const string Index = "OeIndex";
    }
}