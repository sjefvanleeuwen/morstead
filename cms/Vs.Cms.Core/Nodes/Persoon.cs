using Dapper.Contrib.Extensions;
using Itenso.TimePeriod;
using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core.Nodes
{
    [Table("persoon")]
    public class Persoon : INode
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public TimeRange Periode { get; set; }
    }
}
