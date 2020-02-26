using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core;

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
            var steps = Steps.ToList().GetRange(0, Math.Min(Parameters.Count(), step - 1));
            steps.ForEach(s =>
            {
                result.Add(Parameters.FirstOrDefault(p => p.Name == s.ParameterName /*&& p.Key == i.Key*/));
            });
            return result;
        }

        public void AddStep(int requestStep, ExecutionResult result)
        {
            //remove all steps known for this stepnumber and beyond
            //for instance the result for step 3 (requestStep) is added, throw away everything after for step 1 and 2
            //number to take = 3 - 1
            var items = Steps.ToList().GetRange(0, Math.Max(0, requestStep - 1));
            //add the new item requested item from the result question
            items.Add(new Step
            {
                Key = 0/*result.Parameters.FirstOrDefault(p => p.Name == newParameterName).Key*/,
                ParameterName = "temp"/*result.Questions.Parameters.FirstOrDefault()*/
            });
            Steps = items;
        }

        public void UpdateParametersCollection(ParametersCollection parameters)
        {
            parameters.ForEach(p =>
            {
                Parameters.UpSert(p);
            });
        }
    }
}
