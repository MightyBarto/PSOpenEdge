using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOpenEdge.Cmdlets.Database
{
    public enum DatabaseShutdownTimeoutTypes
    {
        None = 0,
        Immediately = 1,
        Maximum = 2,
        Seconds = 3,
        Minutes = 4,
        Hours = 5
    }
}
