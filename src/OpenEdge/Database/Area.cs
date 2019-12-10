using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOpenEdge.OpenEdge.Database
{
    public class Area
    {        
        public Database Database { get; set; }

        public AreaType Type { get; set; }

        public string Name { get; set; }

        public IList<Extent> Extents { get; } = new List<Extent>();

        public Area()
        {

        }

        public Area(Database database, string name)
            : this()
        {
            this.Database = database;
            this.Name = name;
        }
    }
}
