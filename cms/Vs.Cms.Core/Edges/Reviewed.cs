using Dapper.Contrib.Extensions;
using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Edges
{
    [Table("reviewed")]
    public class Reviewed : IEdge, IPeriode
    {
        public DateTime PeriodeBegin { get; set; }
        public DateTime PeriodeEind { get; set; }
        public int Id { get; set; }
    }
}
