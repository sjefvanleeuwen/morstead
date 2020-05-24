using Itenso.TimePeriod;
using System.Threading.Tasks;
using Vs.Orleans.Primitives.Time.Interfaces;

namespace Vs.Orleans.Primitives.Time
{
    public class TimelineGrain : ITimelineGrain
    {
        public Task AddMoment(ITimelineParticipant grain, ITimeLineMoment moment)
        {
            throw new System.NotImplementedException();
        }
    }
}
