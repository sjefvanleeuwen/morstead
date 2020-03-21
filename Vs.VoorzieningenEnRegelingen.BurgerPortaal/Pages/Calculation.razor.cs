using Microsoft.AspNetCore.Components;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.Core;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class Calculation
    {
        //the formElement we are showing
        private IFormElementBase _formElement;

        private int _displayQuestionNumber => _hasRights ?
            FormTitleHelper.GetQuestionNumber(_sequenceController.Sequence, _sequenceController.LastExecutionResult) :
            0;
        private string _displayQuestion => _hasRights ?
            FormTitleHelper.GetQuestion(_sequenceController.LastExecutionResult) :
            "Geen recht";
        private string _displayQuestionTitle => _hasRights ?
            FormTitleHelper.GetQuestionTitle(_sequenceController.LastExecutionResult) :
            "U heeft geen recht op zorgtoeslag.";
        private string _displayQuestionDescription => _hasRights ?
            FormTitleHelper.GetQuestionDescription(_sequenceController.LastExecutionResult) :
            "Met de door u ingevulde gegevens heeft u geen recht op zorgtoeslag. " +
            "Voor meer informatie over zorgtoeslag in uw situatie, neem contact op met de Belastingdienst.";

        private string _result =>
            _sequenceController.LastExecutionResult.Parameters.Any(p => p.Name == "zorgtoeslag") ?
                " <strong>Uw zorgtoeslag is €" +
                ((double)
                    _sequenceController.LastExecutionResult.Parameters.
                        FirstOrDefault(p => p.Name == "zorgtoeslag").Value)
                        .ToString("#.00").Replace('.', ',') + " per maand.</strong>" :
                null;

        private bool _hasRights = true;

        private bool _showPreviousButton => _sequenceController.CurrentStep <= 1;
        private bool _showNextButton => !_hasRights || _result != null;

        [Inject]
        private ISequenceController _sequenceController { get; set; }

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
            //reset the rights
            _hasRights = true;
            //decrease the request step, can never be lower than 1
            _sequenceController.DecreaseStep();
            _sequenceController.ExecuteStep(GetCurrentParameters());
            Display();
            StateHasChanged();
        }

        private void Display()
        {
            if (RechtHelper.HasRecht(_sequenceController.LastExecutionResult))
            {
                _formElement = FormElementHelper.ParseExecutionResult(_sequenceController.LastExecutionResult);
                if (_formElement.ShowElement)
                {
                    _formElement.Value = 
                        FormElementHelper.GetValue(_sequenceController.Sequence, _sequenceController.LastExecutionResult) ??
                        _formElement.Value;
                    ValidateForm(true); //set the IsValid and ErrorText Property
                }
            }
            else
            {
                _formElement = new FormElementBase();
                _hasRights = false;
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
                    new ClientParameter(_formElement.Data.Name, _formElement.Value, _formElement.Data.InferedType)
                };
            }
            return null;
        }

        private ParametersCollection GetCurrentBooleanParameter()
        {
            var result = new ParametersCollection();
            //get all parameter options
            foreach (var key in (_formElement.Data as IMultipleOptionsFormElementData).Options.Keys)
            {
                result.Add(new ClientParameter(key, key == _formElement.Value ? "ja" : "nee", _formElement.Data.InferedType));
            }

            return result;
        }

        private ParametersCollection GetCurrentNumberParameter()
        {
            return new ParametersCollection
            {
                new ClientParameter(_formElement.Data.Name, _formElement.Value.Replace(',', '.'), _formElement.Data.InferedType)
            };
        }
    }
}