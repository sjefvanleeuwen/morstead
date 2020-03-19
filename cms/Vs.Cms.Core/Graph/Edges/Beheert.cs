using Dapper.Contrib.Extensions;
using Itenso.TimePeriod;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Graph.Edges
{
    [Table("beheert")]
    public class Beheert : IEdge
    {
        public int Id { get; set; }
        public TimeRange Periode { get; set; }
    }
}
