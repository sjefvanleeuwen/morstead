using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects
{
    public interface ISequence
    {
        string Yaml { get; set; }
        ParametersCollection Parameters { get; }
        IEnumerable<IStep> Steps { get; }

        ParametersCollection GetParametersToSend(int step);
        void AddStep(int requestStep, ExecutionResult result);
        void UpdateParametersCollection(ParametersCollection parameters);
    }
}