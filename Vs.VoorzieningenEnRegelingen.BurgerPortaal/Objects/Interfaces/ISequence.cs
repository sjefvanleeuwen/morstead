using System.Collections.Generic;
using Vs.Rules.Core.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interfaces
{
    public interface ISequence
    {
        string Yaml { get; set; }
        IParametersCollection Parameters { get; }
        IEnumerable<ISequenceStep> Steps { get; }

        IParametersCollection GetParametersToSend(int step);
        void AddStep(int requestStep, IExecutionResult result);
        void UpdateParametersCollection(IParametersCollection parameters);
    }
}