using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Edges
{
    public class Beheert : IEdge, IPeriode
    {
        public DateTime PeriodeBegin { get; set; }
        public DateTime PeriodeEind { get; set; }
        public int Id { get; set; }
    }
}
