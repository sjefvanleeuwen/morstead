﻿using Microsoft.AspNetCore.Components;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.CitizenPortal.DataModel.Enums;
using Vs.Core.Formats.Yaml.Helper;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Areas.Pages
{
    public partial class ContentTemplate
    {
        [Inject]
        private IYamlScriptController YamlScriptController { get; set; }

        private const string None = "none";
        private const string Block = "block";

        readonly ITextFormElementData YamlLogic = new TextFormElementData
        {
            Label = "Regels Yaml Url",
            Name = "Rule",
            Value = "https://raw.githubusercontent.com/sjefvanleeuwen/morstead/master/src/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5.yaml"
        };

        private string _urlDisplay = None;

        private string UrlYamlContent => "<code><pre>" + _urlYamlContentNonFormatted + "</pre></code>";

        private string _urlYamlContentNonFormatted = string.Empty;

        private void SubmitUrl()
        {
            var yaml = YamlParser.ParseHelper(YamlLogic.Value);
            _urlYamlContentNonFormatted = GetYamlContentTemplate(yaml);
            _urlDisplay = Block;
        }

        private void HideUrlContentResult()
        {
            _urlDisplay = None;
        }

        readonly ITextFormElementData YamlLogicText = new TextFormElementData
        {
            Label = "Regels Yaml Text",
            Name = "Rule Als Text",
            Value = "Vul hier de yaml in",
            Size = ElementSize.ExtraLarge
        };

        private string _textDisplay = None;

        private string UrlTextContent => "<code><pre>" + _textYamlContentNonFormatted + "</pre></code>";

        private string _textYamlContentNonFormatted = string.Empty;
        private void SubmitText()
        {
            _textYamlContentNonFormatted = GetYamlContentTemplate(YamlLogicText.Value);
            _textDisplay = Block;
        }

        private void HideTextContentResult()
        {
            _textDisplay = None;
        }

        private string GetYamlContentTemplate(string body)
        {
            var result = YamlScriptController.Parse(body);
            if (result.IsError)
            {
                return "Er zit een fout in de Yaml";
            }
            return YamlScriptController.CreateYamlContentTemplate();
        }
    }
}
