using Orleans;
using System.Threading.Tasks;

namespace Vs.Orleans.Primitives.Time.Interfaces
{
    public interface IReminderGrain : IGrainWithStringKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}
