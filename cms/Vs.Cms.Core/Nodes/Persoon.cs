using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Nodes
{
    public class Persoon : INode, IPeriode
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public DateTime PeriodeBegin { get; set; }
        public DateTime PeriodeEind { get; set; }
    }
}
