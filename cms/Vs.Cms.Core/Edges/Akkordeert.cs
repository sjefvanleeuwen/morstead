using System;
using Vs.Graph.Core.Data;
using Dapper.Contrib.Extensions;
using Itenso.TimePeriod;

namespace Vs.Cms.Core.Edges
{
    [Table("akkordeert")]
    public class Akkordeert : IEdge, IMoment
    {
        public int Id { get; set; }
        public DateTime Moment { get; set; }
        public TimeRange Periode { get; set; }
    }
}
