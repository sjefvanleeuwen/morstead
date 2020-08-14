using System;
using System.Collections.Generic;
using System.Linq;
using Vs.CitizenPortal.Logic.Objects.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.CitizenPortal.Logic.Objects
{
    public class Sequence : ISequence
    {
        public string Yaml { get; set; }
        public IParametersCollection Parameters { get; private set; }
        public IEnumerable<ISequenceStep> Steps { get; private set; }

        public Sequence()
        {
            Parameters = new ParametersCollection();
            Steps = new List<ISequenceStep>();
        }

        public IParametersCollection GetParametersToSend(int step)
        {
            var result = new ParametersCollection();
            //for each item up till the current step get the parameter that corresponds with the items parameterName and key
            //for instance step 3; give paramaters for step 1 and 2 (item 0 and 1)...
            //...i.e. the first 2 items; number to take = 3 - 1
            var steps = Steps.ToList().GetRange(0, Math.Max(0, Math.Min(Parameters.Count(), step - 1)));
            steps.ForEach(s =>
            {
                foreach (var p in Parameters)
                {
                    if (s.IsMatch(p))
                    {
                        result.Add(p);
                    }
                }
            });
            return result;
        }

        public void AddStep(int requestStep, IExecutionResult result)
        {
            //remove all steps known for this stepnumber and beyond
            //for instance the result for step 3 (requestStep) is added, throw away everything after for step 1 and 2
            //number to take = 3 - 1
            var steps = Steps.ToList().GetRange(0, Math.Max(0, requestStep - 1));
            //add the new item requested item from the result question
            Steps = steps;
            if (result.Questions == null)
            {
                Steps = steps;
                return;
            }
            steps.Add(new SequenceStep
            {
                Key = result.Stacktrace.Last().Step.Key,
                SemanticKey = result.Stacktrace.Last().Step.SemanticKey,
                ParameterName = result.QuestionFirstParameter?.Type != TypeInference.InferenceResult.TypeEnum.Boolean ?
                    result.QuestionFirstParameter.Name :
                    null,
                ValidParameterNames = result.QuestionFirstParameter?.Type == TypeInference.InferenceResult.TypeEnum.Boolean ?
                    result.QuestionParameters.Select(p => p.Name) :
                    null
            });
            Steps = steps;
        }

        public void UpdateParametersCollection(IParametersCollection parameters)
        {
            foreach (var p in parameters.GetAll())
            {
                if (p is IClientParameter && !((IClientParameter)p).IsCalculated)
                {
                    Parameters.UpSert(p);
                }
            };
        }
    }
}
