using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Enums;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    public interface IContentController
    {
        void SetCulture(CultureInfo cultureInfo);
        string GetText(string semanticKey, FormElementContentType type, Dictionary<string, object> parameters = null);
        string GetText(string semanticKey, FormElementContentType type, string option, Dictionary<string, object> parameters = null);
        void Initialize(string body);
    }
}
