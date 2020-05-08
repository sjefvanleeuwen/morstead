using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using Vs.Rules.Core;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Grains.Interfaces;

namespace Vs.Rules.Grains
{
    [StatelessWorker]
    public class RuleWorkerGrain : Grain, IRuleWorker
    {
        public async Task<IExecutionResult> Execute(string yaml, IParametersCollection parameters)
        {
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) => { };
            var result = controller.Parse(yaml);
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException) { }

            return executionResult;
        }
    }
}
