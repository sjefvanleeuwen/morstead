namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers
{
    public static class FormTitleHelper
    {
        //public static int GetQuestionNumber(ISequenceController sequenceController, IExecutionResult result)
        //{
        //    if (sequenceController.LastExecutionResult.Questions == null)
        //    {
        //        return 0;
        //    }
        //    return sequenceController.Sequence.Steps.Count();
        //}

        //public static string GetQuestion(IExecutionResult result)
        //{
        //    return _questions[result.Stacktrace.Last().Step.Description];
        //}

        //public static string GetQuestionTitle(IExecutionResult result)
        //{
        //    if (result.Questions == null)
        //    {
        //        return "Resultaat";
        //    }
        //    return GetFromContent(result.Questions.Parameters, _questionTitle, false, (bool?)result.Parameters?.GetAll().FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
        //}

        //public static string GetQuestionDescription(IExecutionResult result)
        //{
        //    if (result.Questions == null)
        //    {
        //        return "Uw zorgtoeslag is berekend. Hieronder staat het bedrag in euro's waar u volgens de berekening maandelijks recht op hebt.<br />" +
        //            "Let op: dit is een proefberekening, nadat u uw zorgtoeslag hebt aangevraagd bij de Belastingdienst wordt de definitieve toeslag bekend.";
        //    }
        //    return GetFromContent(result.Questions.Parameters, _questionDescription, false, (bool?)result.Parameters?.GetAll().FirstOrDefault(p => p.Name == "alleenstaande")?.Value);
        //}

        //private static string GetFromContent(IParametersCollection parameters, Dictionary<string, string> dictionary, bool showDefaultText = false, bool? alleenstaande = null)
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

        //private static Dictionary<string, string> _questions = new Dictionary<string, string> {
        //    { "woonland", "Waar bent u woonachtig?" },
        //    { "woonsituatie", "Wat is uw woonsituatie?"},
        //    { "vermogensdrempel", "Is uw vermogen hoger dan de drempelwaarde?"},
        //    { "inkomensdrempel", "Is uw toetsingsinkomen hoger dan de inkomensdrempel?"},
        //    { "toetsingsinkomen", "Wat is uw toetsingsinkomen?"},
        //    { "zorgtoeslag", "Maandelijkse zorgtoeslag"}
        //};

        //private static Dictionary<string, string> _questionTitle = new Dictionary<string, string> {
        //    { "woonland", "Selecteer uw woonland." },
        //    { "alleenstaande", "Wat is uw woonsituatie?"},
        //    { "alleenstaande_hoger_dan_inkomensdrempel", "Inkomensdrempel"},
        //    { "toeslagpartner_hoger_dan_inkomensdrempel", "Inkomensdrempel"},
        //    { "alleenstaande_hoger_dan_vermogensdrempel", "Vermogensdrempel"},
        //    { "toeslagpartner_hoger_dan_vermogensdrempel", "Vermogensdrempel"},
        //    { "toetsingsinkomen_aanvrager", "Toetsingsinkomen" },
        //    { "toetsingsinkomen_gezamenlijk", "Gezamenlijk toetsingsinkomen" }
        //};

        //private static Dictionary<string, string> _questionDescription = new Dictionary<string, string> {
        //    { "woonland", "Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst."},
        //    { "alleenstaande", "Indien u niet zeker weet wat uw woonsituatie is, kijk dan op de website van de Belastingdienst."},
        //    { "alleenstaande_hoger_dan_inkomensdrempel",
        //        "Wanneer u als alleenstaande meer inkomen heeft dan €29.562,00 per jaar, overschrijdt u de inkomensdrempel. " +
        //        "U heeft dan geen recht op zorgtoeslag.<br />" +
        //        "Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst."},
        //    { "toeslagpartner_hoger_dan_inkomensdrempel",
        //        "Wanneer u samen met een toeslagpartner meer inkomen heeft dan €37.885,00 per jaar " +
        //        "overschrijdt u de inkomensdrempel. U heeft dan geen recht op zorgtoeslag.<br />" +
        //        "Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst."},
        //    { "alleenstaande_hoger_dan_vermogensdrempel",
        //        "Wanneer u als alleenstaande meer vermogen heeft dan €114.776,00, overschrijdt u de vermogensdrempel. " +
        //        "U heeft dan geen recht op zorgtoeslag.<br />" +
        //        "Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst."},
        //    { "toeslagpartner_hoger_dan_vermogensdrempel",
        //        "Wanneer u samen met een toeslagpartner meer vermogen heeft dan €145.136,00 " +
        //        "overschrijdt u de vermogensdrempel. U heeft dan geen recht op zorgtoeslag.<br />" +
        //        "Indien u niet zeker weet wat uw vermogen is, kijk dan op de website van de Belastingdienst."},
        //    { "toetsingsinkomen_aanvrager", "Vul uw toetsingsinkomen in. Gebruik een komma als scheidingsteken tussen euro's en centen.<br />" +
        //        "Indien u niet zeker weet wat uw inkomen is, kijk dan op de website van de Belastingdienst." },
        //    { "toetsingsinkomen_gezamenlijk", "Vul de som van uw toetsingsinkomen en het toetsingsinkomen van uw toeslagpartner in. " +
        //        "Gebruik een komma als scheidingsteken tussen euro's en centen.<br />" +
        //        "Indien u niet zeker weet wat uw gezamenlijk inkomen is, kijk dan op de website van de Belastingdienst." }
        //};
    }
}
