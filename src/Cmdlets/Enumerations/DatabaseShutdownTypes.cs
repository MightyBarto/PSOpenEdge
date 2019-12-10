namespace PSOpenEdge.Cmdlets.Database
{
    public enum DatabaseShutdownTypes
    {
        Batch = 1, // -b
        Unconditional = 2, // -by
        BatchIfNoActiveUsers = 3, // -bn
        Emergency = 4, // -F
        DataServerBroker = 5, // -Gw
    }

    internal static class DatabaseShutdownTypesExtensions
    {
        public static string GetArgument(this DatabaseShutdownTypes shutdownType)
        {
            switch (shutdownType)
            {
                case DatabaseShutdownTypes.Batch:
                    return "-b";
                case DatabaseShutdownTypes.Unconditional:
                    return "-by";
                case DatabaseShutdownTypes.BatchIfNoActiveUsers:
                    return "-bn";
                case DatabaseShutdownTypes.Emergency:
                    return "-F";
                case DatabaseShutdownTypes.DataServerBroker:
                    return "-Gw";
            }

            return string.Empty;
        }
    }

}