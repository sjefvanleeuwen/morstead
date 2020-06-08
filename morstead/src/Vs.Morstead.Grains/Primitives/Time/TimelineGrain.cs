using Itenso.TimePeriod;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Primitives.Time
{
    public class TimelineGrain : ITimelineGrain
    {
        public Task AddMoment(ITimelineParticipant grain, ITimeLineMoment moment)
        {
            throw new System.NotImplementedException();
        }
    }
}
