using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOpenEdge.OpenEdge.Database
{
    public class OeArea
    {        
        public OeDatabase Database { get; set; }

        public AreaType Type { get; set; }

        public string Name { get; set; }

        public IList<OeExtent> Extents { get; } = new List<OeExtent>();

        public OeArea()
        {

        }

        public OeArea(OeDatabase database, string name)
            : this()
        {
            this.Database = database;
            this.Name = name;
        }
    }
}
