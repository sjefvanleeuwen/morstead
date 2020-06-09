using Itenso.TimePeriod;
using Orleans;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Primitives.Time
{
    public interface ITimelineGrain : IGrainWithStringKey
    {
        Task AddMoment(ITimelineParticipant grain, ITimeLineMoment moment);
    }
}
