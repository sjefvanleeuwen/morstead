using Itenso.TimePeriod;

namespace Vs.Graph.Core.Data
{
    public interface INode
    {
        int Id { get; set; }
        TimeRange Periode { get; set; }
    }
}
