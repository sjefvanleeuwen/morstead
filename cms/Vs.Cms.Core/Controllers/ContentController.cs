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
            var cultureContent = _parsedContent.GetDefaultContent();
            var template = cultureContent.GetContent(semanticKey, type);
            return _renderStrategy.Render(template, parameters);
        }
    }
}
