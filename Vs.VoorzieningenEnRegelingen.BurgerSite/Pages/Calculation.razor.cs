using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerSite.Shared.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerSite.Pages
{
    public partial class Calculation
    {
        private IEnumerable<TimelineItemDTO> _timelineItems;
        private Dictionary<string, string> _selectOptions;
        private Dictionary<string, string> _jaNeeOptions;
        private IEnumerable<FormElementLabel> _dateLabels;
        private IEnumerable<string> _dateValues;
        private IEnumerable<FormElementLabel> _adresLabels;
        private IEnumerable<string> _adresValues;

        protected override void OnInitialized()
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
        }

        private void InitTimeLineItems()
        {
            _timelineItems = new List<TimelineItemDTO> {
                new TimelineItemDTO {
                    Number = 1,
                    Title = "New Web Design",
                    Content ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                    "Quisque scelerisque diam non nisi semper, et elementum lorem ornare. " +
                    "Maecenas placerat facilisis mollis. Duis sagittis ligula in sodales vehicula...."
                },
                new TimelineItemDTO {
                    Number = 2,
                    Content = "Curabitur purus sem, malesuada eu luctus eget, suscipit sed turpis. " +
                    "Nam pellentesque felis vitae justo accumsan, sed semper nisi sollicitudin..."
                },
                new TimelineItemDTO {
                    Number = 3,
                    Content = "Fusce ullamcorper ligula sit amet quam accumsan aliquet. " +
                    "Sed nulla odio, tincidunt vitae nunc vitae, mollis pharetra velit. " +
                    "Sed nec tempor nibh..."
                }
            };
        }
    }
}
