using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Interfaces;
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
            if (_parsedContent == null)
            {
                throw new ArgumentNullException("The ContentController contains no parsed data");
            }
            var cultureContent = _parsedContent.GetDefaultContent();
            var template = cultureContent.GetContent(semanticKey, type);
            return _renderStrategy.Render(template.ToString(), parameters);
        }

        public string GetText(string semanticKey, FormElementContentType type, string option, Dictionary<string, object> parameters = null)
        {
            if (_parsedContent == null)
            {
                throw new ArgumentNullException("The ContentController contains no parsed data");
            }
            var cultureContent = _parsedContent.GetDefaultContent();
            var templates = cultureContent.GetContent(semanticKey, type) as Dictionary<string, string>;
            if (!templates.ContainsKey(option))
            {
                throw new IndexOutOfRangeException($"The option '{option}' is not known in the content for key '{semanticKey}' - '{type}'");
            }
            var template = templates[option];
            return _renderStrategy.Render(template.ToString(), parameters);
        }
    }
}
