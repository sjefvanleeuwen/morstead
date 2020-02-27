using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects
{
    public class Sequence : ISequence
    {
        public string Yaml { get; private set; }
        public ParametersCollection Parameters { get; private set; }
        public IEnumerable<IStep> Steps { get; private set; }

        public Sequence(string yaml)
        {
            Yaml = yaml;
            Parameters = new ParametersCollection();
            Steps = new List<IStep>();
        }

        public ParametersCollection GetParametersToSend(int step)
        {
            var result = new ParametersCollection();
            //for each item up till the current step get the parameter that corresponds with the items parameterName and key
            //for instance step 3; give paramaters for step 1 and 2 (item 0 and 1)...
            //...i.e. the first 2 items; number to take = 3 - 1
            var steps = Steps.ToList().GetRange(0, Math.Max(0, Math.Min(Parameters.Count(), step - 1)));
            steps.ForEach(s =>
            {
                var parameter = Parameters.FirstOrDefault(p => s.IsMatch(p));
                if (parameter != null)
                {
                    result.Add(parameter);
                }
            });
            return result;
        }

        public void AddStep(int requestStep, ExecutionResult result)
        {
            //remove all steps known for this stepnumber and beyond
            //for instance the result for step 3 (requestStep) is added, throw away everything after for step 1 and 2
            //number to take = 3 - 1
            var steps = Steps.ToList().GetRange(0, Math.Max(0, requestStep - 1));
            //add the new item requested item from the result question
            steps.Add(new Step
            {
                Key = result.Stacktrace.Last().Step.Key,
                ParameterName = result.Questions.Parameters.FirstOrDefault()?.Type != TypeInference.InferenceResult.TypeEnum.Boolean ?
                    result.Questions.Parameters.First().Name :
                    null,
                ValidParameterNames = result.Questions.Parameters.FirstOrDefault()?.Type == TypeInference.InferenceResult.TypeEnum.Boolean ?
                    result.Questions.Parameters.Select(p => p.Name) :
                    null
            });
            Steps = steps;
        }

        public void UpdateParametersCollection(ParametersCollection parameters)
        {
            parameters.ForEach(p =>
            {
                if (p is ClientParameter && !((ClientParameter)p).IsCalculated)
                {
                    IEnumerable<string> correspondingParameterNames = new List<string>
                    {
                        p.Name
                    };
                    var stepFound = Steps.FirstOrDefault(s => s.IsMatch(p));
                    if (stepFound != null)
                    {
                        correspondingParameterNames = stepFound.ValidParameterNames;
                    }
                    Parameters.UpSert(p, correspondingParameterNames);
                }
            });
        }
    }
}
