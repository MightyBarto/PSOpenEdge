
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PSOpenEdge.OpenEdge.Model
{
    internal class DatabaseTableJson
    {
        public List<DatabaseTable> Tables { get; set; } = new List<DatabaseTable>();
    }

}