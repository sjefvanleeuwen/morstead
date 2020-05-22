using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.Rules.Grains.Interfaces;

namespace Vs.Rules.Grains.Rules
{
    public class PersistentRuleWorkerGrain : Grain, IPersistentRuleWorker
    {
        private readonly IPersistentState<ExecutionResult> _profile;

        public PersistentRuleWorkerGrain(
            [PersistentState("rule-state", "session-store")]
            IPersistentState<Model> model)
        {
            //_profile = profile;
        }

        public async Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters)
        {
            await _profile.ReadStateAsync();

            IExecutionResult executionResult = _profile.State;
            var controller = new YamlScriptController();
            controller.QuestionCallback = (sender, args) =>
            {
                executionResult.Questions = args;
            };
            var result = controller.Parse(yaml);
            executionResult = new ExecutionResult(ref parameters);
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException) { }
            _profile.State = executionResult as ExecutionResult;
            await _profile.WriteStateAsync();
            return executionResult;
        }

        public Task<ExecutionResult> GetState() => Task.FromResult(_profile.State);

        public async Task<Model> Parse(string yaml)
        {
            var controller = new YamlScriptController();
            return controller.Parse(yaml).Model;
        }
    }
}
