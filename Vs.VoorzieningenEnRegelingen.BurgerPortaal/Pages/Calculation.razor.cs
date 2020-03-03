using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class Calculation
    {
        //the formElement we are showing
        private IFormElement _formElement;

        private int _displayQuestionNumber => _hasRights ?
            FormTitleHelper.GetQuestionNumber(_sequenceController.Sequence) :
            -1;
        private string _displayQuestion => _hasRights ?
            FormTitleHelper.GetQuestion(_sequenceController.LastExecutionResult) :
            "Geen recht";
        private string _displayQuestionTitle => _hasRights ?
            FormTitleHelper.GetQuestionTitle(_sequenceController.LastExecutionResult) :
            "U heeft geen recht op zorgtoeslag.";
        private string _displayQuestionDescription => _hasRights ?
            FormTitleHelper.GetQuestionDescription(_sequenceController.LastExecutionResult) :
            "Met de door u ingevulde waarden is bepaald dat hiervoor geen recht verleend kan worden op zorgtoeslag.";

        private bool _hasRights = true;

        [Inject]
        private ISequenceController _sequenceController { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _sequenceController.Sequence.Yaml = _testYaml;
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
        }

        private void GetPreviousStep()
        {
            //reset the rights
            _hasRights = true;
            //decrease the request step, can never be lower than 1
            _sequenceController.DecreaseStep();
            _sequenceController.ExecuteStep(GetCurrentParameters());
            Display();
        }

        private void Display()
        {

            if (RechtHelper.HasRecht(_sequenceController.LastExecutionResult))
            {
                _formElement = FormElementHelper.ParseExecutionResult(_sequenceController.LastExecutionResult);
                StateHasChanged();
                _formElement.Value = FormElementHelper.GetValue(_sequenceController.Sequence, _sequenceController.LastExecutionResult) ?? string.Empty;
                StateHasChanged();
                ValidateForm(true); //set the IsValid and ErrorText Property
            }
            else
            {
                _formElement = null;
                _hasRights = false;
                StateHasChanged();
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
            StateHasChanged();
            return _formElement.IsValid;
        }

        /// <summary>
        /// Will validate the form
        /// </summary>
        /// <param name="unobtrusive">Will not set a visible error message of IsValid tag to false if true (to use when first displaying generating with an empty value)</param>
        /// <returns>Whether or not the form is valid.</returns>
        private bool ValidateForm(bool unobtrusive = false)
        {
            return _formElement?.Validate(unobtrusive) ?? true;
        }

        /// <summary>
        /// Only return the current value if it is a valid value
        /// </summary>
        /// <returns></returns>
        private ParametersCollection GetCurrentParameters()
        {
            ValidateForm();
            if (_formElement?.IsValid ?? false)
            {
                if (_formElement.InferedType == TypeInference.InferenceResult.TypeEnum.Boolean)
                {
                    return GetCurrentBooleanParameter();
                }
                if (_formElement.InferedType == TypeInference.InferenceResult.TypeEnum.Double)
                {
                    return GetCurrentNumberParameter();
                }
                return new ParametersCollection { 
                    new ClientParameter(_formElement.Name, _formElement.Value, _formElement.InferedType) 
                };
                //Key = 0
            }
            return null;
        }

        private ParametersCollection GetCurrentBooleanParameter()
        {
            var result = new ParametersCollection();
            //get all parameter options
            foreach (var key in _formElement.Options.Keys)
            {
                result.Add(new ClientParameter(key, key == _formElement.Value ? "ja" : "nee", _formElement.InferedType));
            }

            return result;
        }

        private ParametersCollection GetCurrentNumberParameter()
        {
            return new ParametersCollection
            {
                new ClientParameter(_formElement.Name, _formElement.Value.Replace(',', '.'), _formElement.InferedType)
            };
        }

        private string _testYaml => @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: 1
   omschrijving: Waar bent u woonachtig?
   formule: woonlandfactor
   recht: woonlandfactor > 0
 - stap: 2
   omschrijving: Wat is uw woonsituatie?
   formule: standaardpremie
 - stap: 3
   situatie: alleenstaande
   omschrijving: Is uw vermogen hoger dan de drempelwaarde van 114.776,00 euro?
   formule: vermogensdrempel
   recht: vermogensdrempel = 1
 - stap: 3
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Is uw gezamenlijk vermogen hoger dan de drempelwaarde van 145.136,00 euro?
   formule: vermogensdrempel
   recht: vermogensdrempel = 1
 - stap: 4
   situatie: alleenstaande
   omschrijving: Is uw toetsingsinkomen hoger dan de inkomensdrempel van 29.562,00 euro per jaar?
   formule: inkomensdrempel
   recht: inkomensdrempel = 1
 - stap: 4
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Is uw gezamenlijk toetsingsinkomen hoger dan de inkomensdrempel van 37.885,00 euro per jaar?
   formule: inkomensdrempel
   recht: inkomensdrempel = 1
 - stap: 5
   situatie: alleenstaande
   omschrijving: Wat is uw toetsingsinkomen?
   formule: toetsingsinkomen
   recht: toetsingsinkomen_aanvrager < 29562
 - stap: 5
   situatie: aanvrager_met_toeslagpartner
   omschrijving: Wat is uw gezamenlijk toetsingsinkomen?
   formule: toetsingsinkomen_gezamenlijk
   recht: toetsingsinkomen_gezamenlijk < 37885
 - stap: 6
   omschrijving: Maandelijkste zorgtoeslag.
   formule: zorgtoeslag
formules:
 - woonlandfactor:
     formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218
 - vermogensdrempel:
   - situatie: lager_dan_de_vermogensdrempel
     formule: 1
   - situatie: hoger_dan_de_vermogensdrempel
     formule: 0 
 - inkomensdrempel:
   - situatie: lager_dan_de_inkomensdrempel
     formule: 1
   - situatie: hoger_dan_de_inkomensdrempel
     formule: 0
 - toetsingsinkomen:
   - situatie: alleenstaande
     formule: toetsingsinkomen_aanvrager
   - situatie: aanvrager_met_toeslagpartner
     formule: toetsingsinkomen_gezamenlijk
 - drempelinkomen:
     formule: 20941
 - normpremie:
   - situatie: alleenstaande     
     formule: min(percentage(2.005) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 1189)
   - situatie: aanvrager_met_toeslagpartner
     formule: min(percentage(4.315) * drempelinkomen + max(percentage(13.520) * (toetsingsinkomen - drempelinkomen),0), 2314)
 - zorgtoeslag:
     formule: round((standaardpremie - normpremie) * woonlandfactor / 12,2)
tabellen:
  - naam: woonlandfactoren
    woonland, factor:
      - [ Nederland,           1.0    ]
      - [ België,              0.7392 ]
      - [ Bosnië-Herzegovina,  0.0672 ]
      - [ Bulgarije,           0.0735 ]
      - [ Cyprus,              0.1363 ]
      - [ Denemarken,          0.9951 ]
      - [ Duitsland,           0.8701 ]
      - [ Estland,             0.2262 ]
      - [ Finland,             0.7161 ]
      - [ Frankrijk,           0.8316 ]
      - [ Griekenland,         0.2490 ]
      - [ Hongarije,           0.1381 ]
      - [ Ierland,             0.8667 ]
      - [ IJsland,             0.9802 ]
      - [ Italië,              0.5470 ]
      - [ Kaapverdië,          0.0177 ]
      - [ Kroatië,             0.1674 ]
      - [ Letland,             0.0672 ]
      - [ Liechtenstein,       0.9720 ]
      - [ Litouwen,            0.2399 ]
      - [ Luxemburg,           0.7358 ]
      - [ Macedonië,           0.0565 ]
      - [ Malta,               0.3574 ]
      - [ Marokko,             0.0193 ]
      - [ Montenegro,          0.0821 ]
      - [ Noorwegen,           1.3729 ]
      - [ Oostenrijk,          0.6632 ]
      - [ Polen,               0.1691 ]
      - [ Portugal,            0.2616 ]
      - [ Roemenië,            0.0814 ]
      - [ Servië,              0.0714 ]
      - [ Slovenië,            0.3377 ]
      - [ Slowakije,           0.2405 ]
      - [ Spanje,              0.4001 ]
      - [ Tsjechië,            0.2412 ]
      - [ Tunesië,             0.0292 ]
      - [ Turkije,             0.0874 ]
      - [ Verenigd Koninkrijk, 0.7741 ]
      - [ Zweden,              0.8213 ]
      - [ Zwitserland,         0.8000 ]
      - [ Anders,              0      ]
";

    }
}
