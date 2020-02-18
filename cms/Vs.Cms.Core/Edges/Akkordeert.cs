using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Edges
{
    public class Akkordeert : IEdge, IMoment
    {
        public int Id { get; set; }
        public DateTime Moment { get; set; }
    }
}
