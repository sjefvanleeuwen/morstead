using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class Calculation
    {
        [Inject]
        private ISequenceController SequenceController { get; set; }
        [Inject]
        private IContentController ContentController { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private System.Uri Uri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        private string RuleYaml => QueryHelpers.ParseQuery(Uri.Query).TryGetValue("rules", out var param) ? param.First() : null;
        private string ContentYaml => QueryHelpers.ParseQuery(Uri.Query).TryGetValue("content", out var param) ? param.First() : null;

        //the formElement we are showing
        private IFormElementBase _formElement;

        private IStep LastStep => SequenceController.LastExecutionResult.Step;
        private string SemanticKey => SequenceController.HasRights ? LastStep.SemanticKey : LastStep.Break.SemanticKey;
        private int DisplayQuestionNumber => SequenceController.LastExecutionResult.Questions == null ? 0 : SequenceController.Sequence.Steps.Count();
        private string PageTitle => ContentController.GetText("berekening.header", "titel");
        private string PageSubTitle => ContentController.GetText("berekening.header", "ondertitel");
        private string TextSummary => ContentController.GetText(SemanticKey, FormElementContentType.Question);
        private string TextTitle => ContentController.GetText(SemanticKey, FormElementContentType.Title);
        private string TextDescription => ContentController.GetText(SemanticKey, FormElementContentType.Description);
        private bool HasRights => SequenceController.HasRights;
        private bool QuestionAsked => SequenceController.QuestionIsAsked;
        private bool ShowPreviousButton => SequenceController.CurrentStep > 1;
        private bool ShowNextButton => HasRights && QuestionAsked;

        protected override void OnInitialized()
        {
            SequenceController.Sequence.Yaml = RuleYaml ?? YamlZorgtoeslag5.Body;
            ContentController.Initialize(ContentYaml ?? YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            base.OnInitialized();
            //get the first step
            GetNextStep();
        }

        private void GetNextStep()
        {
            if (FormIsValid())
            {
                //increase the request step
                SequenceController.IncreaseStep();
                ProcessStep();
            }
            StateHasChanged();
        }

        private void GetPreviousStep()
        {
            //decrease the request step, can never be lower than 1
            SequenceController.DecreaseStep();
            ProcessStep();
            StateHasChanged();
        }

        private void ProcessStep()
        {
            SequenceController.ExecuteStep(GetCurrentParameters());
            var unresolvedParameters = ContentController.GetUnresolvedParameters(SemanticKey, SequenceController.LastExecutionResult.Parameters);
            var parameters = SequenceController.LastExecutionResult.Parameters;
            if (unresolvedParameters != null && unresolvedParameters.Any())
            {
                SequenceController.FillUnresolvedParameters(ref parameters, unresolvedParameters);
            }
            ContentController.SetParameters(parameters);
            Display();
        }

        private void Display()
        {
            _formElement = new FormElementBase();
            if (HasRights)
            {
                _formElement = _formElement.GetFormElement(SequenceController.LastExecutionResult);
                _formElement.FillDataFromResult(SequenceController.LastExecutionResult, ContentController);
                if (_formElement.ShowElement)
                {
                    _formElement.Data.Value = SequenceController.GetSavedValue() ?? _formElement.Data.Value;
                    ValidateForm(true); //set the IsValid and ErrorText Property unobtrusive
                }
            }
        }

        private bool FormIsValid()
        {
            //always return true if the sequence is before the first step
            if (SequenceController.CurrentStep == 0)
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
            _formElement?.Data?.CustomValidate(unobtrusive);
            return _formElement?.Data?.IsValid ?? true;
        }

        /// <summary>
        /// Only return the current value if it is a valid value
        /// Also a name must be set (i.e. a form element should be present)
        /// </summary>
        /// <returns></returns>
        private ParametersCollection GetCurrentParameters()
        {
            if (_formElement == null || !_formElement.HasInput)
            {
                return null;
            }
            ValidateForm();
            if (_formElement.Data?.IsValid ?? false)
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
                    new ClientParameter(_formElement.Data.Name, _formElement.Data.Value, _formElement.Data.InferedType, SemanticKey)
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
                result.Add(new ClientParameter(key, key == _formElement.Data.Value ? "ja" : "nee", _formElement.Data.InferedType, SemanticKey));
            }

            return result;
        }

        private ParametersCollection GetCurrentNumberParameter()
        {
            return new ParametersCollection
            {
                new ClientParameter(_formElement.Data.Name, _formElement.Data.Value.Replace(',', '.'), _formElement.Data.InferedType, SemanticKey)
            };
        }
    }
}