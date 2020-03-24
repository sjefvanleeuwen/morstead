using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Controllers
{
    public class ContentController : IContentController
    {
        private readonly IRenderStrategy _renderStrategy;
        private CultureInfo _cultureInfo;
        private IParsedContent _parsedContent;

        public ContentController(IRenderStrategy renderStrategy)
        {
            _renderStrategy = renderStrategy;
        }

        public void SetParsedContent(IParsedContent parsedContent)
        {
            _parsedContent = parsedContent;
            if (_cultureInfo != null)
            {
                _parsedContent.SetDefaultCulture(_cultureInfo);
            }
        }

        public void SetCulture(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            if (_parsedContent != null)
            {
                _parsedContent.SetDefaultCulture(_cultureInfo);
            }
        }

        public string GetText(string semanticKey, FormElementContentType type, Dictionary<string, object> parameters = null)
        {
            parameters ??= new Dictionary<string, object>();
            if (_parsedContent == null)
            {
                throw new ArgumentNullException("The ContentController contains no parsed data");
            }
            var cultureContent = _parsedContent.GetDefaultContent();
            var template = cultureContent.GetContent(semanticKey, type);
            if (template == null)
            {
                return string.Empty;
            }
            return _renderStrategy.Render(template.ToString(), parameters);
        }

        public string GetText(string semanticKey, FormElementContentType type, string option, Dictionary<string, object> parameters = null)
        {
            parameters ??= new Dictionary<string, object>();
            if (_parsedContent == null)
            {
                throw new ArgumentNullException("The ContentController contains no parsed data");
            }
            var cultureContent = _parsedContent.GetDefaultContent();
            var templates = cultureContent.GetContent(semanticKey, type) as Dictionary<string, string>;
            if (!templates?.Any() ?? true)
            {
                //no options defined, return the original value
                return option;
            }
            if (!templates.ContainsKey(option))
            {
                throw new IndexOutOfRangeException($"The option '{option}' is not known in the content for key '{semanticKey}' - '{type}'");
            }
            var template = templates[option];
            return _renderStrategy.Render(template.ToString(), parameters);
        }

        public void Initialize(string body)
        {
            //todo MPS Rewrite to get this from the body supplied
            _cultureInfo = new CultureInfo("nl-NL");
            _parsedContent = new ParsedContent();
            _parsedContent.SetDefaultCulture(_cultureInfo);
            _parsedContent.SetCultureContents(GetCultureContents(_cultureInfo));
        }

        private Dictionary<CultureInfo, ICultureContent> GetCultureContents(CultureInfo cultureInfo)
        {
            var cultureContent = new CultureContent();
            cultureContent.AddContent("woonland", FormElementContentType.Question, "Waar bent u woonachtig?");
            cultureContent.AddContent("woonland", FormElementContentType.Title, "Selecteer uw woonland.");
            cultureContent.AddContent("woonland", FormElementContentType.Description, "Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.");
            cultureContent.AddContent("woonland", FormElementContentType.Label, "");
            cultureContent.AddContent("woonland", FormElementContentType.Hint, "Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat.");
            //cultureContent.AddContent("woonland", FormElementContentType.Options, new Dictionary<string, string> { { "alleenstaande", "Alleenstaande" }, { "alleenstaande", "Alleenstaande" } });

            var result = new Dictionary<CultureInfo, ICultureContent> { { _cultureInfo, cultureContent } };
            return result;
        }
    }
}
