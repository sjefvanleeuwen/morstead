using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    public interface IContentController
    {
        void SetParsedContent(IParsedContent parsedContent);
        void SetCulture(CultureInfo cultureInfo);
        string GetText(string semanticKey, FormElementContentType type, Dictionary<string, object> parameters = null);
        string GetText(string semanticKey, FormElementContentType type, string option, Dictionary<string, object> parameters = null);
        void Initialize(string body);
    }
}
