using Dapper.Contrib.Extensions;
using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core
{
    [Table("publicatie")]
    public class Publicatie : INode, IMoment
    {
        public int Id { get; set; }
        public DateTime Moment { get; set; }
    }
}
