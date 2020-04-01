using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Helper;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Controllers
{
    public class ContentController : IContentController
    {
        private readonly IRenderStrategy _renderStrategy;
        private CultureInfo _cultureInfo;
        private IContentHandler _contentHandler;

        public ContentController(IRenderStrategy renderStrategy, IContentHandler contentHandler)
        {
            _renderStrategy = renderStrategy;
            _contentHandler = contentHandler;
        }

        public void SetCulture(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            if (_contentHandler != null)
            {
                _contentHandler.SetDefaultCulture(_cultureInfo);
            }
        }

        public string GetText(string semanticKey, FormElementContentType type, Dictionary<string, object> parameters = null, string defaultResult = "")
        {
            parameters ??= new Dictionary<string, object>();
            var cultureContent = _contentHandler.GetDefaultContent();
            var template = cultureContent.GetContent(semanticKey, type);
            if (template == null)
            {
                return defaultResult;
            }
            return _renderStrategy.Render(template.ToString(), parameters);
        }

        public void Initialize(string body)
        {
            //todo MPS Rewrite to get this from the body supplied
            _cultureInfo = new CultureInfo("nl-NL");
            _contentHandler.SetDefaultCulture(_cultureInfo);
            var parsedContent = YamlContentParser.RenderContentYamlToObject(body);
            _contentHandler.TransLateParsedContentToContent(_cultureInfo, parsedContent);
        }
    }
}
