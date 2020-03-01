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
            { "alleenstaande", "Wat is uw woonsituatie?."}
        };

        private static Dictionary<string, string> _questionDescription = new Dictionary<string, string> { 
            { "woonland", "Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de belastingdienst. Staat uw woonland niet in deze lijst, vul dan \"Anders\" in." },
            { "alleenstaande", "Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de beslatingdienst."}
        };
    }
}
