using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Vs.Morstead.Grains.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Morstead.Grains.Rules
{
    public class PersistentRuleWorkerGrain : Grain, IPersistentRuleWorker
    {
        private IPersistentState<ParametersCollection> _parameters;

        public PersistentRuleWorkerGrain(
            [PersistentState("rule-state", "session-store")]
            IPersistentState<ParametersCollection> parameters)
        {
            _parameters = parameters;
        }

        public async Task<ExecutionResult> Execute(string yaml, IParametersCollection parameters)
        {
            await _parameters.ReadStateAsync();
            var state = _parameters.State as IParametersCollection;


            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            var controller = new YamlScriptController();
            controller.QuestionCallback = (sender, args) =>
            {
                executionResult.Questions = args;
            };
            var result = controller.Parse(yaml);
            executionResult = new ExecutionResult(ref parameters);
            try
            {
                controller.ExecuteWorkflow(ref state, ref executionResult);
            }
            catch (UnresolvedException) { }
            _parameters.State = executionResult.Parameters as ParametersCollection;
            await _parameters.WriteStateAsync();
            return executionResult as ExecutionResult;
        }

        public async Task<IParametersCollection> GetState()
        {
            await _parameters.ReadStateAsync();
            return _parameters.State as IParametersCollection;
        }

        public async Task<Model> Parse(string yaml)
        {
            var controller = new YamlScriptController();
            return controller.Parse(yaml).Model;
        }
    }
}
