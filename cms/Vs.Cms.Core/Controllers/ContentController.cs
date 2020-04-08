using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Helper;
using Vs.Cms.Core.Interfaces;
using Vs.Cms.Core.Objects.Interfaces;
using Vs.Core.Extensions;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

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

        public string GetText(string semanticKey, FormElementContentType type)
        {
            return GetText(semanticKey, type, null, string.Empty);
        }
        public string GetText(string semanticKey, FormElementContentType type, IParametersCollection parameters)
        {
            return GetText(semanticKey, type, parameters, string.Empty);
        }
        public string GetText(string semanticKey, FormElementContentType type, IParametersCollection parameters, string defaultResult)
        {
            return GetText(semanticKey, type.GetDescription(), parameters, defaultResult);
        }
        public string GetText(string semanticKey, string type)
        {
            return GetText(semanticKey, type, null, string.Empty);
        }
        public string GetText(string semanticKey, string type, IParametersCollection parameters)
        {
            return GetText(semanticKey, type, parameters, string.Empty);
        }
        public string GetText(string semanticKey, string type, IParametersCollection parameters, string defaultResult)
        {
            var cultureContent = _contentHandler.GetDefaultContent();
            var template = cultureContent.GetContent(semanticKey, type);
            if (template == null)
            {
                return defaultResult;
            }
            return _renderStrategy.Render(template.ToString(), ConvertParametersCollectionToDictionary(parameters));
        }

        private IDictionary<string, object> ConvertParametersCollectionToDictionary(IParametersCollection parameters)
        {
            var result = new Dictionary<string, object>();
            if (parameters == null)
            {
                return result;
            }
            foreach (var parameter in parameters)
            {
                if (result.ContainsKey(parameter.Name))
                {
                    //always take the last supplied value in the chain
                    result[parameter.Name] = parameter.ValueAsString;
                    continue;
                }
                result.Add(parameter.Name, parameter.ValueAsString);
                
            }
            return result;
        }

        public void Initialize(string body)
        {
            //todo MPS Rewrite to get this from the body supplied
            _cultureInfo = new CultureInfo("nl-NL");
            _contentHandler.SetDefaultCulture(_cultureInfo);
            var parsedContent = YamlContentParser.RenderContentYamlToObject(body);
            _contentHandler.TranslateParsedContentToContent(_cultureInfo, parsedContent);
        }
    }
}
