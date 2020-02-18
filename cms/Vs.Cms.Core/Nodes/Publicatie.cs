using System;
using Vs.Graph.Core.Data;

namespace Vs.Cms.Core
{
    public class Publicatie : INode, IMoment
    {
        public int Id { get; set; }
        public DateTime Moment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
