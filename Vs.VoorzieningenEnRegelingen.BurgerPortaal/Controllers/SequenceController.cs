    using System;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers
{
    public class SequenceController : ISequenceController
    {
        public ISequence Sequence { get; private set; }
        public int CurrentStep { get; private set; } = 0;
        public int RequestStep { get; private set; } = 0;
        public ParseResult ParseResult { get; private set; }
        public ExecutionResult LastExecutionResult { get; private set; }

        private IServiceController _serviceController;

        public SequenceController(IServiceController serviceController, ISequence sequence)
        {
            _serviceController = serviceController;
            Sequence = sequence;
        }

        public ExecuteRequest GetExecuteRequest(ParametersCollection parameters = null)
        {
            return new ExecuteRequest
            {
                Config = Sequence.Yaml,
                Parameters = parameters
            };
        }

        public ParseRequest GetParseRequest()
        {
            if (ParseResult == null)
            {
                ParseResult = _serviceController.Parse(GetParseRequest());
            }

            return new ParseRequest
            {
                Config = Sequence.Yaml
            };
        }

        public void IncreaseStep()
        {
            RequestStep++;
        }

        public void DecreaseStep()
        {
            RequestStep = Math.Max(1, RequestStep - 1);
        }

        public void ExecuteStep(ParametersCollection currentParameters)
        {
            SaveCurrentParameters(currentParameters);
            //var requestParameters = GetRequestParameters();
            var requestParameters = Sequence.GetParametersToSend(RequestStep);
            var request = GetExecuteRequest(requestParameters);
            LastExecutionResult = _serviceController.Execute(request);
            //only save non-calculated parameters
            Sequence.UpdateParametersCollection(LastExecutionResult.Parameters);

            Sequence.AddStep(RequestStep, LastExecutionResult);
            CurrentStep = RequestStep;
        }

        private void SaveCurrentParameters(ParametersCollection currentParameters)
        {
            if (currentParameters != null)
            {
                Sequence.UpdateParametersCollection(currentParameters);
            }
        }
    }
}
