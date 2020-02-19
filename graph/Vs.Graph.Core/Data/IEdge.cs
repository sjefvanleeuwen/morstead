using Itenso.TimePeriod;

namespace Vs.Graph.Core.Data
{
    public interface IEdge
    {
       int Id { get; set; }
       TimeRange Periode {get;set;}
    }
}
