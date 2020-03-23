using Itenso.TimePeriod;

namespace Vs.Graph.Core.Data
{
    public interface INode : IGraphEntity
    {
        int Id { get; set; }
        TimeRange Periode { get; set; }
    }
}
