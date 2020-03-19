using Dapper.Contrib.Extensions;
using Itenso.TimePeriod;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Graph.Edges
{
    [Table("reviewed")]
    public class Reviewed : IEdge
    {
        public int Id { get; set; }
        public TimeRange Periode { get; set; }
    }
}
