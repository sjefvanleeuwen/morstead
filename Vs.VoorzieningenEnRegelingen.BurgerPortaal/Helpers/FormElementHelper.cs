using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class FormElementHelper
    {
        public static IFormElement ParseExecutionResult(IExecutionResult result)
        {
            var formElement = result.Questions == null ? 
                new FormElement() :
                GetFormElementFormInferedType(GetInferedType(result.Questions));

            if (result.Questions == null)
            {
                return formElement;
            }

            formElement.InferedType = GetInferedType(result.Questions);

            formElement.Name = result.Questions.Parameters.GetAll().First().Name;
            formElement.Label = GetFromLookupTable(result.Questions.Parameters, _labels, false, (bool?)result.Parameters?.GetAll().FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
            formElement.Options = DefineOptions(result);
            formElement.TagText = GetFromLookupTable(result.Questions.Parameters, _tagText, false, (bool?)result.Parameters?.GetAll().FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
            formElement.HintText = GetFromLookupTable(result.Questions.Parameters, _hintText, false, (bool?)result.Parameters?.GetAll().FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
            return formElement;
        }

        private static IFormElement GetFormElementFormInferedType(TypeInference.InferenceResult.TypeEnum typeEnum)
        {
            switch (typeEnum)
            {
                case TypeInference.InferenceResult.TypeEnum.Double:
                    return new Number() as IFormElement;
                case TypeInference.InferenceResult.TypeEnum.Boolean:
                    return new Radio() as IFormElement;
                case TypeInference.InferenceResult.TypeEnum.List:
                    return new Select() as IFormElement;
                case TypeInference.InferenceResult.TypeEnum.TimeSpan:
                case TypeInference.InferenceResult.TypeEnum.DateTime:
                case TypeInference.InferenceResult.TypeEnum.String:
                case TypeInference.InferenceResult.TypeEnum.Period:
                default:
                    return new FormElement() as IFormElement;
            }
        }

        private static TypeInference.InferenceResult.TypeEnum GetInferedType(IQuestionArgs questions)
        {
            return questions.Parameters.GetAll().FirstOrDefault().Type;
        }

        private static string GetFromLookupTable(IParametersCollection parameters, Dictionary<string, string> dictionary, bool showDefaultText = false, bool? alleenstaande = null)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            var key = parameters.GetAll().FirstOrDefault(p => dictionary.Keys.Contains(ModifyName(p.Name, alleenstaande)))?.Name ?? string.Empty;
            if (key != null)
            {
                key = ModifyName(key, alleenstaande);
            }
            if (dictionary.Keys.Contains(key))
            {
                return dictionary[key];
            }

            return showDefaultText ? $"Unknown for {parameters.GetAll().First().Name}" : string.Empty;
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

        private static Dictionary<string, string> DefineOptions(IExecutionResult result)
        {
            switch (GetInferedType(result.Questions))
            {
                case TypeInference.InferenceResult.TypeEnum.Boolean:
                    return BooleanToOptions(result);
                case TypeInference.InferenceResult.TypeEnum.List:
                    return ListToOptions(result.Questions);
                default:
                    return null;
            }
        }

        private static Dictionary<string, string> BooleanToOptions(IExecutionResult result)
        {
            var options = new Dictionary<string, string>();
            foreach (var p in result.Questions.Parameters.GetAll()) {
                options.Add(p.Name, GetParameterDisplayName(p.Name, result.Parameters));
            }
            return options;
        }

        private static string GetParameterDisplayName(string name, IParametersCollection parameters)
        {
            switch (name)
            {
                case "hoger_dan_de_vermogensdrempel":
                    if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
                        return "Ja, mijn vermogen is <strong>hoger</strong> dan €114.776,00";
                    else return "Ja, het gezamenlijk vermogen is <strong>hoger</strong> dan €145.136,00";
                case "lager_dan_de_vermogensdrempel":
                    if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
                        return "Nee, mijn vermogen is <strong>lager</strong> dan €114.776,00";
                    else return "Nee, het gezamenlijk vermogen is <strong>lager</strong> dan €145.136,00";
                case "hoger_dan_de_inkomensdrempel":
                    if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
                        return "Ja, mijn inkomen is <strong>hoger</strong> dan €29.562,00";
                    else return "Ja, het gezamenlijk inkomen is <strong>hoger</strong> dan €37.885,00";
                case "lager_dan_de_inkomensdrempel":
                    if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
                        return "Nee, mijn inkomen is <strong>lager</strong> dan €29.562,00";
                    else return "Nee, het gezamenlijk inkomen is <strong>lager</strong> dan €37.885,00";
            }

            return name.Substring(0, 1).ToUpper() + name.Substring(1).Replace('_', ' ');
        }

        private static Dictionary<string, string> ListToOptions(IQuestionArgs questions)
        {
            var result = new Dictionary<string, string>();
            (questions.Parameters.GetAll().First().Value as List<object>)?.ForEach(v => result.Add(v.ToString(), v.ToString()));
            return result;
        }

        private static Dictionary<string, string> _labels = new Dictionary<string, string>
        {
            //{ "woonland", "Woonland" },
            //{ "alleenstaande", "Woonsituatie"},
            //{ "lager_dan_de_inkomensdrempel", "Inkomensdrempel"},
            //{ "lager_dan_de_vermogensdrempel", "Vermogensdrempel"}
            //{ "toetsingsinkomen_aanvrager", "" },
            //{ "toetsingsinkomen_gezamenlijk", "" }
        };

        private static Dictionary<string, string> _tagText = new Dictionary<string, string>
        {
        };

        private static Dictionary<string, string> _hintText = new Dictionary<string, string> {
            { "woonland", "Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat." },
            { "alleenstaande", "Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft."},
            { "alleenstaande_hoger_dan_de_vermogensdrempel", "De huidige vermogensdrempel voor alleenstaanden is €114.776,00."},
            { "toeslagpartner_hoger_dan_de_vermogensdrempel", "De huidige vermogensdrempel voor aanvragers met toeslagpartners is €145.136,00"},
            { "alleenstaande_hoger_dan_de_inkomensdrempel", "De huidige inkomensdrempel voor alleenstaanden is €29.562,00 per jaar."},
            { "toeslagpartner_hoger_dan_de_inkomensdrempel", "De huidige inkomensdrempel voor aanvragers met toeslagpartners is €37.885,00 per jaar"},
            { "toetsingsinkomen_aanvrager", "Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen." },
            { "toetsingsinkomen_gezamenlijk", "Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen." }
        };

        internal static string GetValue(ISequence sequence, IExecutionResult result)
        {
            var value = GetSavedValue(sequence, result);
            if (value != null && GetInferedType(result.Questions) == TypeInference.InferenceResult.TypeEnum.Double)
            {
                value = value?.Replace('.', ',');
            }

            return value;
        }

        private static string GetSavedValue(ISequence sequence, IExecutionResult result)
        {
            //no saved value if there is no question
            if (result.Questions == null)
            {
                return null;
            }
            //find the step that is a match for this name
            var step = sequence.Steps.FirstOrDefault(s => s.IsMatch(result.Questions.Parameters.GetAll().FirstOrDefault()));
            if (step == null)
            {
                return null;
            }

            //find the corresponding saved Parameter for this step
            var parameters = sequence.Parameters.GetAll().Where(p => step.IsMatch(p));
            if (parameters == null || !parameters.Any())
            {
                return null;
            }
            if (parameters.Count() == 1)
            {
                return parameters.Single().ValueAsString;
            }

            if (parameters.First().Type == TypeInference.InferenceResult.TypeEnum.Boolean)
            {
                return parameters.FirstOrDefault(p => (bool)p.Value).Name;
            }

            return parameters.First().ValueAsString;
        }

        private static string GetDefaultListValue(IExecutionResult result)
        {
            var options = DefineOptions(result);
            if (options.Keys.Contains(string.Empty))
            {
                return string.Empty;
            }

            return options.Keys?.FirstOrDefault() ?? string.Empty;
        }
    }
}
