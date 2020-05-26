using Orleans;

namespace Vs.Orleans.Primitives.Time.Interfaces
{
    public interface IReminderGrain : IGrainWithStringKey, IRemindable
    {
    }
}
