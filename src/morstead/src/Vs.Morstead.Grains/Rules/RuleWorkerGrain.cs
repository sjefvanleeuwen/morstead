using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using Vs.Morstead.Grains.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Morstead.Grains.Rules
{
    [StatelessWorker]
    [Reentrant]
    public class RuleWorkerGrain : Grain, IRuleWorker
    {
        public async Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters)
        {
            IExecutionResult executionResult = null;
            var controller = new YamlScriptController();
            controller.QuestionCallback = (sender, args) =>
            {
                executionResult.Questions = args;
            };
            var result = controller.Parse(yaml);
            executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException) { }

            return executionResult;
        }

        public async Task<Model> Parse(string yaml)
        {
            var controller = new YamlScriptController();
            var model = controller.Parse(yaml);
            return model.Model;
        }
    }
}
