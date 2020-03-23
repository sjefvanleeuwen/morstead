using System;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers
{
    public class SequenceController : ISequenceController
    {
        public ISequence Sequence { get; private set; }
        public int CurrentStep { get; private set; } = 0;
        public int RequestStep { get; private set; } = 0;
        public IExecutionResult LastExecutionResult { get; private set; }
        public IParseResult ParseResult { get; private set; }

        private IServiceController _serviceController;
        private IParseRequest _parseRequest;
        private IParseResult _parseResult;


        public SequenceController(IServiceController serviceController, ISequence sequence)
        {
            _serviceController = serviceController;
            Sequence = sequence;
        }

        public IExecuteRequest GetExecuteRequest(IParametersCollection parameters = null)
        {
            return new ExecuteRequest
            {
                Config = Sequence.Yaml,
                Parameters = parameters
            };
        }

        public IParseResult GetParseResult()
        {
            if (_parseResult == null)
            {
                _parseResult = _serviceController.Parse(GetParseRequest());
            }

            return _parseResult;
        }

        public IParseRequest GetParseRequest()
        {
            if (_parseRequest == null)
            {
                _parseRequest = new ParseRequest
                {
                    Config = Sequence.Yaml
                };
            }
            return _parseRequest;
        }

        public void IncreaseStep()
        {
            RequestStep++;
        }

        public void DecreaseStep()
        {
            RequestStep = Math.Max(1, RequestStep - 1);
        }

        public void ExecuteStep(IParametersCollection currentParameters)
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

        private void SaveCurrentParameters(IParametersCollection currentParameters)
        {
            if (currentParameters != null)
            {
                Sequence.UpdateParametersCollection(currentParameters);
            }
        }
    }
}
