using Dapper.Contrib.Extensions;
using Itenso.TimePeriod;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core
{
    [Table("publicatie")]
    public class Publicatie : INode
    {
        public int Id { get; set; }
        public TimeRange Periode { get; set; }
    }
}
