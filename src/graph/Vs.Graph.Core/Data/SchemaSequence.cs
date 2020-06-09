using System.Collections.Generic;

namespace Vs.Graph.Core.Data
{
    public class SchemaSequence
    {
        public List<Schema> Schemas { get; set; }
    }

    public class Schema
    {
        public string Name { get; set; }
    }

}
