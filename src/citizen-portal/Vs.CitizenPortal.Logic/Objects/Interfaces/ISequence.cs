using System.Collections.Generic;
using Vs.Rules.Core.Interfaces;

namespace Vs.CitizenPortal.Logic.Objects.Interfaces
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