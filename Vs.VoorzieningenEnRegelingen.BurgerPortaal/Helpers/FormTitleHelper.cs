using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class FormTitleHelper
    {
        public static int GetQuestionNumber(ISequence sequence, IExecutionResult result)
        {
            if (result.Questions == null)
            {
                return 0;
            }
            return sequence.Steps.Count();
        }

        public static string GetQuestion(IExecutionResult result)
        {
            return result.Stacktrace.Last().Step.Description;
        }

        public static string GetQuestionTitle(IExecutionResult result)
        {
            if (result.Questions == null)
            {
                return "Resultaat";
            }
            return GetFromLookupTable(result.Questions.Parameters, _questionTitle, false, (bool?)result.Parameters?.FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
        }

        public static string GetQuestionDescription(IExecutionResult result)
        {
            if (result.Questions == null)
            {
                return "Uw zorgtoeslag is berekend. Hieronder staat het bedrag in euro's waar u volgens de berekening maandelijks recht op hebt.<br />" +
                    "Let op: dit is een proefberekening, nadat u uw zorgtoeslag hebt aangevraagd bij de Belastingdienst wordt de definitieve toeslag bekend.";
            }
            return GetFromLookupTable(result.Questions.Parameters, _questionDescription, false, (bool?)result.Parameters?.FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
        }

        private static string GetFromLookupTable(IParametersCollection parameters, Dictionary<string, string> dictionary, bool showDefaultText = false, bool? alleenstaande = null)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            var key = parameters.FirstOrDefault(p => dictionary.Keys.Contains(ModifyName(p.Name, alleenstaande)))?.Name ?? string.Empty;
            if (key != null)
            {
                key = ModifyName(key, alleenstaande);
    }
            if (dictionary.Keys.Contains(key))
            {
                return dictionary[key];
            }

            return showDefaultText? $"Unknown for {parameters[0].Name}" : string.Empty;
        }

        private static string ModifyName(string key, bool? alleenstaande)
{
    if (alleenstaande == null)
    {
        return key;
    }
    if (key == "hoger_dan_de_vermogensdrempel")
    {
        return (alleenstaande ?? false) ? "alleenstaande_hoger_dan_de_vermogensdrempel" : "toeslagpartner_hoger_dan_de_vermogensdrempel";
    }
    if (key == "hoger_dan_de_inkomensdrempel")
    {
        return (alleenstaande ?? false) ? "alleenstaande_hoger_dan_de_inkomensdrempel" : "toeslagpartner_hoger_dan_de_inkomensdrempel";
    }

    return key;
}

private static Dictionary<string, string> _questionTitle = new Dictionary<string, string> {
            { "woonland", "Selecteer uw woonland." },
            { "alleenstaande", "Wat is uw woonsituatie?"},
            { "alleenstaande_hoger_dan_de_inkomensdrempel", "Inkomensdrempel"},
            { "toeslagpartner_hoger_dan_de_inkomensdrempel", "Inkomensdrempel"},
            { "alleenstaande_hoger_dan_de_vermogensdrempel", "Vermogensdrempel"},
            { "toeslagpartner_hoger_dan_de_vermogensdrempel", "Vermogensdrempel"},
            { "toetsingsinkomen_aanvrager", "Toetsingsinkomen" },
            { "toetsingsinkomen_gezamenlijk", "Gezamenlijk toetsingsinkomen" }
        };

private static Dictionary<string, string> _questionDescription = new Dictionary<string, string> {
            { "woonland", "Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst."},
            { "alleenstaande", "Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst."},
            { "alleenstaande_hoger_dan_de_inkomensdrempel",
                "Wanneer u als alleenstaande meer inkomen heeft dan €29.562,00 per jaar, overschrijdt u de inkomensdrempel. " +
                "U heeft dan geen recht op zorgtoeslag.<br />" +
                "Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst."},
            { "toeslagpartner_hoger_dan_de_inkomensdrempel",
                "Wanneer u samen met een toeslagpartner meer inkomen heeft dan €37.885,00 per jaar " +
                "overschrijdt u de inkomensdrempel. U heeft dan geen recht op zorgtoeslag.<br />" +
                "Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst."},
            { "alleenstaande_hoger_dan_de_vermogensdrempel",
                "Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. " +
                "U heeft dan geen recht op zorgtoeslag.<br />" +
                "Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst."},
            { "toeslagpartner_hoger_dan_de_vermogensdrempel",
                "Wanneer u samen met een toeslagpartner meer vermogen heeft dan €145.136,00 " +
                "overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.<br />" +
                "Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst."},
            { "toetsingsinkomen_aanvrager", "Vul uw toetsingsinkomen in. Gebruik een komma als scheidingsteken tussen euro's en centen.<br />" +
                "Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst." },
            { "toetsingsinkomen_gezamenlijk", "Vul de som van uw toetsingsinkomen en het toetsingsinkomen van uw toeslagpartner in. " +
                "Gebruik een komma als scheidingsteken tussen euro's en centen.<br />" +
                "Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst." }
        };
    }
}
