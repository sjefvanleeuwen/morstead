using Orleans;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces
{
    public interface IReminderGrain : IGrainWithStringKey, IRemindable
    {
        Task Start();
        Task Stop();
    }
}
