namespace PSOpenEdge.Cmdlets.Database
{
    public enum IndexBuildTypes
    {
        All = 0,
        ActiveIndexes = 1,
        InactiveIndexes = 2,
        Table = 3
    }

    internal static class IndexBuildTypesExtensions
    {
        internal static string GetArgument(this IndexBuildTypes buildType)
        {
            return buildType.ToString().ToLower();
        }
        
    }
}