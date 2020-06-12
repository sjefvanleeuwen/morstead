using Orleans;
using Orleans.Runtime;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vs.Morstead.Bpm.Model;
using Vs.Morstead.Bpm.Model.Tasks;
using Vs.Morstead.Grains.Interfaces.Bpm;

namespace Vs.Morstead.Grains.Bpm
{
    public class BpmProcessGrain : Grain, IBpmProcessGrain
    {
        private BpmnProcess _process;

        public BpmProcessGrain([PersistentState("bpm-process", "bpm-process-store")]
         IPersistentState<BpmProcessState> _process)
        {
            Process = _process;
        }

        public IPersistentState<BpmProcessState> Process { get; }

        public async Task PauseProcess()
        {
            if (Process.State.Status != BpmProcessExecutionTypes.Started)
                throw new Exception($"Current bpm process has the status {Process.State.Status}. Only processes that are in state {BpmProcessExecutionTypes.Started} can be paused.");
            Process.State.Status = BpmProcessExecutionTypes.Paused;
            await Process.WriteStateAsync();
        }

        public async Task LoadProcess(string bpmn)
        {
            if (Process.State.Status != BpmProcessExecutionTypes.Uninitialized)
                throw new Exception($"Current bpm process has the status {Process.State.Status}. Only processes that are in state {BpmProcessExecutionTypes.Uninitialized} can be loaded.");
            Process.State.Bpmn = bpmn;
            Process.State.Status = BpmProcessExecutionTypes.Initialized;
            await Process.WriteStateAsync();
        }

        public async Task StopProcess()
        {
            if (Process.State.Status != BpmProcessExecutionTypes.Started &&
                Process.State.Status != BpmProcessExecutionTypes.Paused)
                throw new Exception($"Current bpm process has the status {Process.State.Status}. Only processes that are in states {BpmProcessExecutionTypes.Started} or  {BpmProcessExecutionTypes.Paused} can be stopped.");
            Process.State.Status = BpmProcessExecutionTypes.Stopped;
            await Process.WriteStateAsync();
        }

        public async Task StartProcess()
        {
            if (Process.State.Status != BpmProcessExecutionTypes.Initialized &&
                Process.State.Status != BpmProcessExecutionTypes.Stopped &&
                Process.State.Status != BpmProcessExecutionTypes.Paused)
                throw new Exception($"Current bpm process has the status {Process.State.Status}. Only processes that are in states {BpmProcessExecutionTypes.Initialized} or {BpmProcessExecutionTypes.Stopped} or {BpmProcessExecutionTypes.Paused} can be started.");

            _process = new BpmnProcess(Process.State.Bpmn);
            var sequenceFlow = _process.SequenceFlow.Next();
            Process.State.Status = BpmProcessExecutionTypes.Started;
            await Process.WriteStateAsync();
            if (sequenceFlow.ExecutionListeners != null);
            ExecuteDelegate(sequenceFlow.ExecutionListeners[0]);
        }

        public Task<BpmProcessExecutionTypes> GetProcessStatus()
        {
            return Task.FromResult(Process.State.Status);
        }

        private void ExecuteDelegate(ExecutionListener listener)
        {
            var l = typeof(IBpmProcessGrain).Assembly.DefinedTypes.First(p => p.Name == listener.DelegateInterface).AsType();
            var m = l.GetMethod(listener.DelegateMethod);
            var p = m.GetParameters();
            var o = new System.Collections.Generic.List<object>();
            foreach (var item in m.GetParameters())
            {
                if (!listener.OutputParameters.ContainsKey(item.Name.ToLower()))
                {
                    throw new Exception($"Execution Listener for delegate {listener.DelegateInterface} method {listener.DelegateMethod} does not contain the expected parameter {item.Name}.");
                }
                if (item.ParameterType.IsArray)
                {

                    o.Add(new string[] { listener.OutputParameters[item.Name.ToLower()] });
                }
                else
                {
                    o.Add(listener.OutputParameters[item.Name.ToLower()]);
                }
            }
            var grain = GrainFactory.GetGrain(l, 0);
            m.Invoke(grain, o.ToArray());
        }
    }
}
