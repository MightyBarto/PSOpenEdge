using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOpenEdge.OpenEdge.Database
{
    public class Extent
    {
        public Area Area { get; set; }

        public int Number { get; set; }

        public FileInfo File { get; set; }

        public Extent()
        { }

        public Extent(Area area, int number)
            : this()
        {
            this.Area = area;
            this.Number = number;
        }

    }
}
