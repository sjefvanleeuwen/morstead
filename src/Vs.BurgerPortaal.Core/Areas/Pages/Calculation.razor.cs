using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements;
using Vs.CitizenPortal.DataModel.Model;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.CitizenPortal.DataModel.Model.Interfaces;
using Vs.CitizenPortal.Logic.Controllers.Interfaces;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Core.Enums;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;
using Vs.Core.Layers.Helpers;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData;

namespace Vs.BurgerPortaal.Core.Areas.Pages
{
    public partial class Calculation
    {
        [Inject]
        private ISequenceController SequenceController { get; set; }
        [Inject]
        private IContentController ContentController { get; set; }
        [Inject]
        private IYamlSourceController YamlSourceController { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool ShowDisclaimer = true;

        //the formElement we are showing
        private IFormElementBase _formElement;

        private string ContentYaml => YamlSourceController.GetYaml(YamlType.Uxcontent);
        private int DisplayQuestionNumber => SequenceController.LastExecutionResult.Questions == null ? 0 : SequenceController.Sequence.Steps.Count();
        private bool HasRights => SequenceController.HasRights;
        private IStep LastStep => SequenceController.LastExecutionResult.Step;
        private string PageSubTitle => ContentController.GetText("berekening.header", "ondertitel");
        private string PageTitle => ContentController.GetText("berekening.header", "titel");
        private double Progress => CalculateProgress();
        private bool QuestionAsked => SequenceController.QuestionIsAsked;
        private string RuleYaml => YamlSourceController.GetYaml(YamlType.Rules);
        private string SemanticKey => SequenceController.HasRights ? LastStep.SemanticKey : LastStep.Break.SemanticKey;
        private bool ShowForm => _formElement.GetType().IsSubclassOf(typeof(FormElementBase));
        private bool ShowPreviousButton => SequenceController.CurrentStep > 1;
        private bool ShowNextButton => HasRights && QuestionAsked;
        private string TextDescription => ContentController.GetText(SemanticKey, FormElementContentType.Description);
        private string TextSummary => ContentController.GetText(SemanticKey, FormElementContentType.Question);
        private string TextTitle => ContentController.GetText(SemanticKey, FormElementContentType.Title);
        private Uri Uri => NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

        protected override void OnInitialized()
        {
            InitialiseYamls();
            SequenceController.Sequence.Yaml = RuleYaml;
            ContentController.Initialize(ContentYaml);
            base.OnInitialized();
            //get the first step
            GetNextStep();
        }

        private void InitialiseYamls()
        {
            YamlSourceHelper.SetAllYamlFromUri(YamlSourceController, Uri);
            //this type of setting will override whatever is provided via url, so it will only set if there is nothing set yet.
            YamlSourceHelper.SetDefaultYaml(YamlSourceController,
                null,
                YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"),
                YamlTestFileLoader.Load(@"Zorgtoeslag5Content.yaml")
            //,YamlTestFileLoader.Load(@"Zorgtoeslag5Routing.yaml")
            );
        }

        private void GetNextStep()
        {
            if (FormIsValid())
            {
                //increase the requ6est step
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
            ContentController.Parameters = parameters;
            Display();
        }

        private void Display()
        {
            _formElement = new FormElementBase();
            if (HasRights)
            {
                _formElement = GetFormElement(SequenceController.LastExecutionResult) ?? _formElement;
                _formElement.FillDataFromResult(SequenceController.LastExecutionResult, ContentController);
                if (_formElement.ShowElement)
                {
                    _formElement.Data.Value = SequenceController.GetSavedValue() ?? _formElement.Data.Value;
                    ValidateForm(true); //set the IsValid and ErrorText Property unobtrusive
                }
            }
        }

        private IFormElementBase GetFormElement(IExecutionResult result)
        {
            switch (result.InferedType)
            {
                case TypeInference.InferenceResult.TypeEnum.Double:
                    return new Number();
                case TypeInference.InferenceResult.TypeEnum.Boolean:
                    return new Radio();
                case TypeInference.InferenceResult.TypeEnum.List:
                    return new Select();
                case TypeInference.InferenceResult.TypeEnum.String:
                    return new Text();
                case TypeInference.InferenceResult.TypeEnum.DateTime:
                case TypeInference.InferenceResult.TypeEnum.Period:
                case TypeInference.InferenceResult.TypeEnum.Step:
                case TypeInference.InferenceResult.TypeEnum.TimeSpan:
                case TypeInference.InferenceResult.TypeEnum.Unknown:
                default:
                    return null;
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

        private double CalculateProgress()
        {
            if (!ShowPreviousButton)
            {
                return 0;
            }
            if (!ShowNextButton)
            {
                return 1;
            }
            var contentNodes =
                SequenceController.LastExecutionResult.ContentNodes.Where(n =>
                    !n.Name.ToLower().Contains(".keuze") &&
                    !n.Name.ToLower().StartsWith("formule") &&
                    !n.Name.ToLower().EndsWith(".geen_recht") &&
                    n.Name.ToLower() != "end")
                .Select(n =>
                    n.Name.ToLower().Contains(".situatie") ?
                        n.Name.ToLower().Substring(0, n.Name.IndexOf(".situatie")) :
                        n.Name.ToLower()).Distinct().ToList();
            for (var i = 0; i < contentNodes.Count(); i++)
            {
                if (SemanticKey.StartsWith(contentNodes.ElementAt(i)))
                {
                    return (i + 1d - 1) / (contentNodes.Count() - 1);
                }
            }

            return 0;
        }

        private void HideDisclaimer()
        {
            ShowDisclaimer = false;
        }
    }
}