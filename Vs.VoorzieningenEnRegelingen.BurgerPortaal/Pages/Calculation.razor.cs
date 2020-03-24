using Microsoft.AspNetCore.Components;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interface;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class Calculation
    {
        [Inject]
        private ISequenceController _sequenceController { get; set; }
        [Inject]
        private IContentController _contentController { get; set; }

        //the formElement we are showing
        private IFormElementBase _formElement;
        private string _semanticKey => _sequenceController.LastExecutionResult.SemanticKey;
        private int _displayQuestionNumber
        {
            get
            {
                if (_sequenceController.LastExecutionResult.Questions == null)
                {
                    return 0;
                }
                return _sequenceController.Sequence.Steps.Count();
            }
        }

        private string _textSummary => _contentController.GetText(_semanticKey, FormElementContentType.Question).ToString();
        //FormTitleHelper.GetQuestion(_sequenceController.LastExecutionResult) :
        //"Geen recht";
        private string _textTitle => _contentController.GetText(_semanticKey, FormElementContentType.Title).ToString();
        //FormTitleHelper.GetQuestionTitle(_sequenceController.LastExecutionResult) :
        //"U heeft geen recht op zorgtoeslag.";
        private string _textDescription => _contentController.GetText(_semanticKey, FormElementContentType.Description).ToString();
        //FormTitleHelper.GetQuestionDescription(_sequenceController.LastExecutionResult) :
        //"Met de door u ingevulde gegevens heeft u geen recht op zorgtoeslag. " +
        //"Voor meer informatie over zorgtoeslag in uw situatie, neem contact op met de Belastingdienst.";

        //_sequenceController.LastExecutionResult.Parameters.Any(p => p.Name == "zorgtoeslag") ?
        //    " <strong>Uw zorgtoeslag is €" +
        //    ((double)
        //        _sequenceController.LastExecutionResult.Parameters.
        //            FirstOrDefault(p => p.Name == "zorgtoeslag").Value)
        //            .ToString("#.00").Replace('.', ',') + " per maand.</strong>" :
        private bool _hasRights => _sequenceController.HasRights;
        private bool _questionAsked => _sequenceController.QuestionIsAsked;
        private bool _showPreviousButton => _sequenceController.CurrentStep > 1;
        private bool _showNextButton => _hasRights && _questionAsked;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _sequenceController.Sequence.Yaml = YamlZorgtoeslag4.Body;
            //get the first step
            GetNextStep();
        }

        private void GetNextStep()
        {
            if (FormIsValid())
            {
                //increase the request step
                _sequenceController.IncreaseStep();
                _sequenceController.ExecuteStep(GetCurrentParameters());
                Display();
            }
            StateHasChanged();
        }

        private void GetPreviousStep()
        {
            //decrease the request step, can never be lower than 1
            _sequenceController.DecreaseStep();
            _sequenceController.ExecuteStep(GetCurrentParameters());
            Display();
            StateHasChanged();
        }

        private void Display()
        {
            if (_hasRights)
            {
                _formElement = new FormElementBase().GetFormElement(_sequenceController.LastExecutionResult);
                _formElement.FillDataFromResult(_sequenceController.LastExecutionResult);
                if (_formElement.ShowElement)
                {
                    _formElement.Data.Value = _sequenceController.GetSavedValue() ?? _formElement.Data.Value;
                    //FormElementHelper.GetValue(_sequenceController.Sequence, _sequenceController.LastExecutionResult) ??
                    //_formElement.Data.Value;
                    ValidateForm(true); //set the IsValid and ErrorText Property
                }
            }
            else
            {
                _formElement = new FormElementBase();
            }
        }

        private bool FormIsValid()
        {
            //always return true if the sequence is before the first step
            if (_sequenceController.CurrentStep == 0)
            {
                return true;
            }
            ValidateForm();
            return _formElement.Data.IsValid;
        }

        /// <summary>
        /// Will validate the form
        /// </summary>
        /// <param name="unobtrusive">Will not set a visible error message of IsValid tag to false if true (to use when first displaying generating with an empty value)</param>
        /// <returns>Whether or not the form is valid.</returns>
        private bool ValidateForm(bool unobtrusive = false)
        {
            _formElement?.Data?.Validate(unobtrusive);
            return _formElement?.Data?.IsValid ?? true;
        }

        /// <summary>
        /// Only return the current value if it is a valid value
        /// </summary>
        /// <returns></returns>
        private ParametersCollection GetCurrentParameters()
        {
            ValidateForm();
            if (_formElement?.Data?.IsValid ?? false)
            {
                if (_formElement.Data.InferedType == TypeInference.InferenceResult.TypeEnum.Boolean)
                {
                    return GetCurrentBooleanParameter();
                }
                if (_formElement.Data.InferedType == TypeInference.InferenceResult.TypeEnum.Double)
                {
                    return GetCurrentNumberParameter();
                }
                return new ParametersCollection {
                    new ClientParameter(_formElement.Data.Name, _formElement.Data.Value, _formElement.Data.InferedType)
                };
            }
            return null;
        }

        private ParametersCollection GetCurrentBooleanParameter()
        {
            var result = new ParametersCollection();
            //get all parameter options
            foreach (var key in (_formElement.Data as IOptionsFormElementData).Options.Keys)
            {
                result.Add(new ClientParameter(key, key == _formElement.Data.Value ? "ja" : "nee", _formElement.Data.InferedType));
            }

            return result;
        }

        private ParametersCollection GetCurrentNumberParameter()
        {
            return new ParametersCollection
            {
                new ClientParameter(_formElement.Data.Name, _formElement.Data.Value.Replace(',', '.'), _formElement.Data.InferedType)
            };
        }
    }
}