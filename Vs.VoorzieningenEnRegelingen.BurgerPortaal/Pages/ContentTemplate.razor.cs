using System.Threading.Tasks;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages
{
    public partial class ContentTemplate
    {
        ITextFormElementData YamlLogic = new TextFormElementData
        {
            Size = FormElementSize.ExtraLarge,
            Label = "Regels Yaml Url",
            Name = "Rule",
            TagText = "Optioneel",
            Value = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/YamlZorgtoeslag5.yaml"
        };

        private string _urlDisplay = "none";

        private string _urlYamlContent => _urlYamlContentNonFormatted.Replace("\n", "<br />");

        private string _urlYamlContentNonFormatted = @"<code><pre>Content:
 - key: berekening.header
   titel: Zorgtoeslag
   ondertitel: Proefberekening
 - key: stap.woonland
   vraag: Waar bent u woonachtig?
   titel: Selecteer uw woonland.
   tekst: Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.
   hint: Selecteer ""Anders"" wanneer uw woonland niet in de lijst staat.
 - key: stap.woonsituatie
   vraag: Wat is uw woonsituatie?
   titel: Wat is uw woonsituatie?
   tekst: Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst.
 - key: stap.woonsituatie.keuze.alleenstaande
   hint: Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft.
   tekst: Alleenstaande
 - key: stap.woonsituatie.keuze.aanvrager_met_toeslagpartner
   tekst: Aanvrager met toeslagpartner
 - key: stap.vermogensdrempel.situatie.alleenstaande
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
 - key: stap.vermogensdrempel.situatie.alleenstaande.keuze.hoger_dan_vermogensdrempel
   tekst: Ja, mijn vermogen is **hoger** dan €114.776,00
   hint: De huidige vermogensdrempel voor alleenstaanden is €114.776,00.
 - key: stap.vermogensdrempel.situatie.alleenstaande.keuze.lager_dan_vermogensdrempel
   tekst: Nee, mijn vermogen is **lager** dan €114.776,00
 - key: stap.vermogensdrempel.situatie.aanvrager_met_toeslagpartner
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u samen met een toeslagpartner meer vermogen heeft dan €145.136,00 overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.</pre></code>";

        private async Task SubmitUrl()
        {
            //await OpenPage(pageBase + rules + content);
            _urlDisplay = "block";
        }

        private async Task HideUrlContentResult()
        {
            //await OpenPage(pageBase + rules + content);
            _urlDisplay = "none";
        }

        ITextFormElementData YamlLogicText = new TextFormElementData
        {
            Size = FormElementSize.ExtraLarge,
            Label = "Regels Yaml Text",
            Name = "Rule Als Text",
            TagText = "Optioneel",
            Value = "Vul hier de Yaml"
        };

        private string _textDisplay = "none";

        private string _urlTextContent => _urlTextContentNonFormatted.Replace("\n", "<br />").Replace(" ", "&nsbp;");

        private string _urlTextContentNonFormatted = @"<code><pre>Content:
 - key: berekening.header
   titel: Zorgtoeslag
   ondertitel: Proefberekening
 - key: stap.woonland
   vraag: Waar bent u woonachtig?
   titel: Selecteer uw woonland.
   tekst: Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.
   hint: Selecteer ""Anders"" wanneer uw woonland niet in de lijst staat.
 - key: stap.woonsituatie
   vraag: Wat is uw woonsituatie?
   titel: Wat is uw woonsituatie?
   tekst: Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst.
 - key: stap.woonsituatie.keuze.alleenstaande
   hint: Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft.
   tekst: Alleenstaande
 - key: stap.woonsituatie.keuze.aanvrager_met_toeslagpartner
   tekst: Aanvrager met toeslagpartner
 - key: stap.vermogensdrempel.situatie.alleenstaande
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.
 - key: stap.vermogensdrempel.situatie.alleenstaande.keuze.hoger_dan_vermogensdrempel
   tekst: Ja, mijn vermogen is **hoger** dan €114.776,00
   hint: De huidige vermogensdrempel voor alleenstaanden is €114.776,00.
 - key: stap.vermogensdrempel.situatie.alleenstaande.keuze.lager_dan_vermogensdrempel
   tekst: Nee, mijn vermogen is **lager** dan €114.776,00
 - key: stap.vermogensdrempel.situatie.aanvrager_met_toeslagpartner
   vraag: Is uw vermogen hoger dan de drempelwaarde?
   titel: Vermogensdrempel
   tekst: Wanneer u samen met een toeslagpartner meer vermogen heeft dan €145.136,00 overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.
     <br />Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst.</pre></code>";
        private async Task SubmitText()
        {
            _textDisplay = "block";
            //await OpenPage(pageBase + rules + content);
        }

        private async Task HideTextContentResult()
        {
            _textDisplay = "none";
            //await OpenPage(pageBase + rules + content);
        }

        private async Task GetYamlContentTemplate()
        {
            //CreateYamlContentTemplate()
        }
    }
}
