using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class FormTitleHelper
    {
        public static int GetQuestionNumber(ISequence sequence)
        {
            return sequence.Steps.Count();
        }

        public static string GetQuestion(IExecutionResult result)
        {
            return result.Stacktrace.Last().Step.Description;
        }

        public static string GetQuestionTitle(IExecutionResult result)
        {
            return GetFromLookupTable(result.Questions.Parameters, _questionTitle);
        }

        public static string GetQuestionDescription(IExecutionResult result)
        {
            return GetFromLookupTable(result.Questions.Parameters, _questionDescription);
        }

        private static string GetFromLookupTable(ParametersCollection parameters, Dictionary<string, string> dictionary, bool showDefaultText = false)
        {
            var key = parameters.FirstOrDefault(p => dictionary.Any() && dictionary.Keys.Contains(p.Name))?.Name;
            return key != null ? dictionary[key] : null ?? (showDefaultText ? $"Unknown for {parameters[0].Name}" : string.Empty);
        }

        private static Dictionary<string, string> _questionTitle = new Dictionary<string, string> {
            { "woonland", "Selecteer uw woonland." },
            { "alleenstaande", "Wat is uw woonsituatie?."},
            { "alleenstaand_lager_dan_de_inkomensdrempel", "Inkomensdrempel"},
            { "toeslagpartner_lager_dan_de_inkomensdrempel", "Inkomensdrempel"},
            { "alleenstaand_lager_dan_de_vermogensdrempel", "Vermogensdrempel"},
            { "toeslagpartner_lager_dan_de_vermogensdrempel", "Vermogensdrempel"},
            { "toetsingsinkomen_aanvrager", "Toetsingsinkomen" },
            { "toetsingsinkomen_gezamenlijk", "Gezamenlijk toetsingsinkomen" }
        };

        private static Dictionary<string, string> _questionDescription = new Dictionary<string, string> { 
            { "woonland", "Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de belastingdienst. " +
                "Staat uw woonland niet in deze lijst, vul dan \"Anders\" in." },
            { "alleenstaande", "Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de belastingdienst."},
            { "lager_dan_de_inkomensdrempel", "Afhankelijk van us situatie; wanneer u als alleenstaande meer verdient " +
                "dan 29.562,00 euro per jaar, of als u samen met een toeslagpartner meer verdient dan 37.885,00 euro per jaar, " +
                "dan overschreidt u de inkomensdrempel voor zorgtoeslag. U heeft dan geen recht op zorgtoeslag. " +
                "Wij willen u alleen naar het daadwerkelijke bedrag vragen wanneer dit voor de berekening noodzakelijk is. " +
                "Daarom vragen wij u eerst of u deze drempelwaarde overschreidt." +
                "Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de belastingdienst."},
            { "lager_dan_de_vermogensdrempel", "Afhankelijk van us situatie; wanneer u als alleenstaande meer vermogen heeft dan " +
                "dan 114.776,00 euro, of als u samen met een toeslagpartner meer vermogen heeft dan 145.136,00 euro, " +
                "dan overschreidt u de vermogensdrempel voor zorgtoeslag. U heeft dan geen recht op zorgtoeslag. " +
                "Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de belastingdienst."},
            { "toetsingsinkomen_aanvrager", "Vul een getal in. Gebruik geen punt (\".\"), en slechts een komma (\",\") als scheidngsteken tussen euro's en centen." },
            { "toetsingsinkomen_gezamenlijk", "Vul een getal in. Gebruik geen punt (\".\"), en slechts een komma (\",\") als scheidngsteken tussen euro's en centen. " +
                "Vul de som van uw toetsingsinkomen en het toetsingsinkomen van uw toeslagpartner in." }
        };
    }
}
