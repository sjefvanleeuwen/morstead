using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class Calculation
    {
        //the formElement we are showing
        private FormElement _formElement = new FormElement();

        private IEnumerable<object> _errors = new List<object>();

        [Inject]
        private ISequenceController _sequenceController { get; set; }

        protected override void OnInitialized()
        {
            InitTestData();
            //get the first step
            GetNextStep();
        }

        private void GetNextStep()
        {
            if (FormIsValid())
            {
                //increase the request step
                _sequenceController.RequestStep++;
                _sequenceController.ExecuteStep(GetCurrentParameter());
            }
        }

        private void GetPreviousStep()
        {
            //decrease the request step, can never be lower than 1
            _sequenceController.RequestStep = Math.Max(1, _sequenceController.RequestStep - 1);
            _sequenceController.ExecuteStep(GetCurrentParameter());
        }

        private bool FormIsValid()
        {
            //always return true if the sequence is before the first step
            if (_sequenceController.CurrentStep == 0)
            {
                return true;
            }
            ValidateForm();
            return !_errors.Any();
        }

        private void ValidateForm()
        {
        }

        /// <summary>
        /// Only return the current value if it is a valid value
        /// </summary>
        /// <returns></returns>
        private Parameter GetCurrentParameter()
        {
            ValidateForm();
            if (_formElement.IsValid)
            {
                return new Parameter
                {
                    Name = _formElement.Name,
                    Value = _formElement.Value,
                    //Key = 0
                };
            }
            return null;
        }

        #region test display variables

        private IEnumerable<TimelineItemDTO> _timelineItems;
        private Dictionary<string, string> _selectOptions;
        private Dictionary<string, string> _countryOptions;
        private Dictionary<string, string> _woonsituatieOptions;
        private Dictionary<string, string> _jaNeeOptions;
        private IEnumerable<FormElementLabel> _dateLabels;
        private IEnumerable<string> _dateValues;
        private IEnumerable<FormElementLabel> _adresLabels;
        private IEnumerable<string> _adresValues;

        private void InitTestData()
        {
            InitTimeLineItems();
            InitSelectOptions();
            InitJaNeeOptions();
            InitDate();
            InitAdres();
        }

        private void InitAdres()
        {
            _adresLabels = new List<FormElementLabel> {
                new FormElementLabel { Text = "Postcode", Title = "AdresPostcode" },
                new FormElementLabel { Text = "Huisnummer", Title = "AdresHuisnummer" },
                new FormElementLabel { Text = "Toevoeging", Title = "AdresToevoeging" }
            };
            _adresValues = new List<string> { "1234 AB", "12", "a" };
        }

        private void InitDate()
        {
            _dateLabels = new List<FormElementLabel> {
                new FormElementLabel { Text = "Dag", Title = "DateDag" },
                new FormElementLabel { Text = "Maand", Title = "DateMaand" },
                new FormElementLabel { Text = "Jaar", Title = "DateJaar" }
            };
            _dateValues = new List<string> { "18", "2", "2020" };
        }

        private void InitJaNeeOptions()
        {
            _jaNeeOptions = new Dictionary<string, string>
            {
                {"true", "Ja" },
                {"false", "Nee" }
            };
        }

        private void InitSelectOptions()
        {
            _selectOptions = new Dictionary<string, string>
            {
                {"1", "een" },
                {"2", "twee" },
                {"3", "drie" },
                {"99", "negenennegentig" }
            };
            _countryOptions = new Dictionary<string, string>
            {
                {"België", "België"},
                {"Bosnië-Herzegovina", "Bosnië-Herzegovina"},
                {"Bulgarije", "Bulgarije"},
                {"Cyprus", "Cyprus"},
                {"Denemarken", "Denemarken"},
                {"Duitsland", "Duitsland"},
                {"Estland", "Estland"},
                {"Finland", "Finland"},
                {"Frankrijk", "Frankrijk"},
                {"Griekenland", "Griekenland"},
                {"Hongarije", "Hongarije"},
                {"Ierland", "Ierland"},
                {"IJsland", "IJsland"},
                {"Italië", "Italië"},
                {"Kaapverdië", "Kaapverdië"},
                {"Kroatië", "Kroatië"},
                {"Letland", "Letland"},
                {"Liechtenstein", "Liechtenstein"},
                {"Litouwen", "Litouwen"},
                {"Luxemburg", "Luxemburg"},
                {"Macedonië", "Macedonië"},
                {"Malta", "Malta"},
                {"Marokko", "Marokko"},
                {"Montenegro", "Montenegro"},
                {"Nederland", "Nederland"},
                {"Noorwegen", "Noorwegen"}
            };
            _woonsituatieOptions = new Dictionary<string, string>
            {
                {"alleenstaand", "Alleenstaand"},
                {"samenwonend_met_toeslagpartner", "Samenwonend met toeslagpartner"},
            };
        }

        private void InitTimeLineItems()
        {
            _timelineItems = new List<TimelineItemDTO> {
                new TimelineItemDTO {
                    Number = 1,
                    Title = "Woonland",
                    Content ="Selecteer het land waar u woonachtig bent. " +
                    "Selecteer \"Anders\" Wanneer het land niet in de lijst staat."
                },
                new TimelineItemDTO {
                    Number = 2,
                    Title = "Woonsituatie",
                    Content = "Selecteer uw huidige woonsituatie."
                }//,
                //new TimelineItemDTO {
                //    Number = 3,
                //    Content = "Fusce ullamcorper ligula sit amet quam accumsan aliquet. " +
                //    "Sed nulla odio, tincidunt vitae nunc vitae, mollis pharetra velit. " +
                //    "Sed nec tempor nibh..."
                //}
            };
        }

        #endregion
    }
}
