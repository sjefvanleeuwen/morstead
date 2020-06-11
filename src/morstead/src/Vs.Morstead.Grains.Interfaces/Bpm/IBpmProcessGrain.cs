using Orleans;
using System.Threading.Tasks;

namespace Vs.Morstead.Grains.Interfaces.Bpm
{
    public interface IBpmProcessGrain : IGrainWithStringKey
    {
        Task LoadProcess(string bpmn);
        Task StartProcess();
        Task PauseProcess();
        Task StopProcess();
        Task<BpmProcessExecutionTypes> GetProcessStatus();
    }
}
