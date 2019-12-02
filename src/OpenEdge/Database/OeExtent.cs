using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOpenEdge.OpenEdge.Database
{
    public class OeExtent
    {
        public OeArea Area { get; set; }

        public int Number { get; set; }

        public FileInfo File { get; set; }

        public OeExtent()
        { }

        public OeExtent(OeArea area, int number)
            : this()
        {
            this.Area = area;
            this.Number = number;
        }

    }
}
