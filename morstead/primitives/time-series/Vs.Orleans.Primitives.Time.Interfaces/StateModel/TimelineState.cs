using Itenso.TimePeriod;

namespace Vs.Orleans.Primitives.Time.Interfaces.StateModel
{
    public class TimelineState
    {
        public TimeLine<TimePeriodCollection> TimeLine { get; set; }
    }
}
