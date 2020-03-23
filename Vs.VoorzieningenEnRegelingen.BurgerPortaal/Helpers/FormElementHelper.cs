namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class FormElementHelper
    {
        //public static IFormElementBase ParseExecutionResult(IExecutionResult result)
        //{
        //    var formElement = result.Questions == null ?
        //        new FormElementBase() :
        //        GetFormElementFormInferedType(result.GetInferedType());

        //    if (result.Questions != null)
        //    {
        //        formElement.FillDataFromResult(result);
        //    }

        //    return formElement;
        //}



        //internal static string GetFromContent(IParametersCollection parameters, Dictionary<string, string> dictionary, bool showDefaultText = false, bool? alleenstaande = null)
        //{
        //    if (dictionary == null)
        //    {
        //        throw new ArgumentNullException(nameof(dictionary));
        //    }
        //    var key = parameters.GetAll().FirstOrDefault(p => dictionary.Keys.Contains(ModifyName(p.Name, alleenstaande)))?.Name ?? string.Empty;
        //    if (key != null)
        //    {
        //        key = ModifyName(key, alleenstaande);
        //    }
        //    if (dictionary.Keys.Contains(key))
        //    {
        //        return dictionary[key];
        //    }

        //    return showDefaultText ? $"Unknown for {parameters.GetAll().First().Name}" : string.Empty;
        //}

        //private static string ModifyName(string key, bool? alleenstaande)
        //{
        //    if (alleenstaande == null)
        //    {
        //        return key;
        //    }
        //    if (key == "hoger_dan_vermogensdrempel")
        //    {
        //        return (alleenstaande ?? false) ? "alleenstaande_hoger_dan_vermogensdrempel" : "toeslagpartner_hoger_dan_vermogensdrempel";
        //    }
        //    if (key == "hoger_dan_inkomensdrempel")
        //    {
        //        return (alleenstaande ?? false) ? "alleenstaande_hoger_dan_inkomensdrempel" : "toeslagpartner_hoger_dan_inkomensdrempel";
        //    }
        //    if (key == "toetsingsinkomen")
        //    {
        //        return (alleenstaande ?? false) ? "toetsingsinkomen_aanvrager" : "toetsingsinkomen_gezamenlijk";
        //    }

        //    return key;
        //}

        //internal static bool StepHasQuestion(ISequenceController sequenceController)
        //{
        //    return sequenceController.LastExecutionResult.Questions?.Parameters?.Any() ?? false;
        //}

        //internal static string GetParameterDisplayName(string name, IParametersCollection parameters)
        //{
        //    switch (name)
        //    {
        //        case "hoger_dan_vermogensdrempel":
        //            if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
        //                return "Ja, mijn vermogen is <strong>hoger</strong> dan €114.776,00";
        //            else return "Ja, het gezamenlijk vermogen is <strong>hoger</strong> dan €145.136,00";
        //        case "lager_dan_vermogensdrempel":
        //            if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
        //                return "Nee, mijn vermogen is <strong>lager</strong> dan €114.776,00";
        //            else return "Nee, het gezamenlijk vermogen is <strong>lager</strong> dan €145.136,00";
        //        case "hoger_dan_inkomensdrempel":
        //            if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
        //                return "Ja, mijn inkomen is <strong>hoger</strong> dan €29.562,00";
        //            else return "Ja, het gezamenlijk inkomen is <strong>hoger</strong> dan €37.885,00";
        //        case "lager_dan_inkomensdrempel":
        //            if (parameters.GetAll().Any(p => p.Name == "alleenstaande" && (bool)p.Value))
        //                return "Nee, mijn inkomen is <strong>lager</strong> dan €29.562,00";
        //            else return "Nee, het gezamenlijk inkomen is <strong>lager</strong> dan €37.885,00";
        //    }

        //    return name.Substring(0, 1).ToUpper() + name.Substring(1).Replace('_', ' ');
        //}

        //internal static Dictionary<string, string> Labels = new Dictionary<string, string>
        //{
        //    //{ "woonland", "Woonland" },
        //    //{ "alleenstaande", "Woonsituatie"},
        //    //{ "lager_dan_inkomensdrempel", "Inkomensdrempel"},
        //    //{ "lager_dan_vermogensdrempel", "Vermogensdrempel"}
        //    //{ "toetsingsinkomen_aanvrager", "" },
        //    //{ "toetsingsinkomen_gezamenlijk", "" }
        //};

        //internal static Dictionary<string, string> TagText = new Dictionary<string, string>
        //{
        //};

        //internal static Dictionary<string, string> HintText = new Dictionary<string, string> {
        //    { "woonland", "Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat." },
        //    { "alleenstaande", "Geef aan of u alleenstaande bent of dat u een toeslagpartner heeft."},
        //    { "alleenstaande_hoger_dan_vermogensdrempel", "De huidige vermogensdrempel voor alleenstaanden is €114.776,00."},
        //    { "toeslagpartner_hoger_dan_vermogensdrempel", "De huidige vermogensdrempel voor aanvragers met toeslagpartners is €145.136,00"},
        //    { "alleenstaande_hoger_dan_inkomensdrempel", "De huidige inkomensdrempel voor alleenstaanden is €29.562,00 per jaar."},
        //    { "toeslagpartner_hoger_dan_inkomensdrempel", "De huidige inkomensdrempel voor aanvragers met toeslagpartners is €37.885,00 per jaar"},
        //    { "toetsingsinkomen_aanvrager", "Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen." },
        //    { "toetsingsinkomen_gezamenlijk", "Vul een getal in. Gebruik een komma (,) in plaats van een punt (.) als scheidingsteken tussen euro's en centen." }
        //};
    }
}
