using System;

namespace PSOpenEdge.OpenEdge.Database
{
    [Flags]
    public enum AreaType
    {
        None = 0,
        BeforeImage = 1,
        Data = 2,
        AfterImage = 4,
    }
}