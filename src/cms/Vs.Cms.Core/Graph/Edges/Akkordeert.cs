using Dapper.Contrib.Extensions;
using Itenso.TimePeriod;
using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Graph.Edges
{
    [Table("akkordeert")]
    public class Akkordeert : IEdge, IMoment
    {
        public int Id { get; set; }
        public DateTime Moment { get; set; }
        public TimeRange Periode { get; set; }
    }
}
