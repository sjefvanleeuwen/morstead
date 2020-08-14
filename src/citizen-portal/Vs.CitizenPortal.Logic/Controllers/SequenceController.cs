﻿using System;
using System.Collections.Generic;
using System.Linq;
using Vs.CitizenPortal.Logic.Controllers.Interfaces;
using Vs.CitizenPortal.Logic.Objects.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;

namespace Vs.CitizenPortal.Logic.Controllers
{
    public class SequenceController : ISequenceController
    {
        public ISequence Sequence { get; private set; }
        public int CurrentStep { get; private set; } = 0;
        public int RequestStep { get; private set; } = 0;
        public IExecutionResult LastExecutionResult { get; private set; }
        public IParseResult ParseResult { get; private set; }

        public bool QuestionIsAsked => LastExecutionResult?.QuestionParameters.Any() ?? false;

        public bool HasRights => !LastExecutionResult?.Parameters?.Any(p => p.Name == "recht" && !(bool)p.Value) ?? true;

        private readonly IServiceController _serviceController;
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

        public void FillUnresolvedParameters(ref IParametersCollection parameters, IEnumerable<string> unresolvedParameters)
        {
            var evaluateFormulaWithoutQARequest = GetEvaluateFormulaWithoutQARequest(ref parameters, unresolvedParameters);
            _serviceController.GetExtraParameters(evaluateFormulaWithoutQARequest);
        }

        public IEvaluateFormulaWithoutQARequest GetEvaluateFormulaWithoutQARequest(ref IParametersCollection parameters, IEnumerable<string> unresolvedParameters)
        {
            return new EvaluateFormulaWithoutQARequest
            {
                Config = Sequence.Yaml,
                Parameters = parameters,
                UnresolvedParameters = unresolvedParameters ?? new List<string>()
            };
        }

        public string GetSavedValue()
        {
            //no saved value if there is no question
            if (LastExecutionResult?.Questions == null)
            {
                return null;
            }

            //find the step that is a match for this name
            var step = Sequence.Steps.ToList().FirstOrDefault(s =>
                s.IsMatch(LastExecutionResult.QuestionParameters.FirstOrDefault()));
            if (step == null)
            {
                return null;
            }

            var value = GetValueFromSavedParameter(step);
            if (value != null &&
                LastExecutionResult.InferedType == TypeInference.InferenceResult.TypeEnum.Double)
            {
                value = value?.Replace('.', ',');
            }

            return value;
        }

        private string GetValueFromSavedParameter(ISequenceStep step)
        {
            //find the corresponding saved Parameter for this step
            var parameters = Sequence.Parameters.GetAll().Where(p => step.IsMatch(p));
            if (parameters == null || !parameters.Any())
            {
                return null;
            }
            if (parameters.Count() == 1)
            {
                return parameters.Single().ValueAsString;
            }

            if (parameters.First().Type == TypeInference.InferenceResult.TypeEnum.Boolean)
            {
                return parameters.FirstOrDefault(p => (bool)p.Value).Name;
            }

            return parameters.First().ValueAsString;
        }
    }
}
