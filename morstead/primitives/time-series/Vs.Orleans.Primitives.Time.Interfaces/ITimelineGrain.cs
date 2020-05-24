using Itenso.TimePeriod;
using Orleans;
using System.Threading.Tasks;

namespace Vs.Orleans.Primitives.Time.Interfaces
{
    public interface ITimelineGrain : IGrainWithStringKey
    {
        Task AddMoment(ITimelineParticipant grain, ITimeLineMoment moment);
    }
}
