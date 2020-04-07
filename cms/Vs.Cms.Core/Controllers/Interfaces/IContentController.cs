using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Enums;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    public interface IContentController
    {
        void SetCulture(CultureInfo cultureInfo);
        string GetText(string semanticKey, FormElementContentType type, Dictionary<string, object> parameters = null, string defaultResult = "");
        string GetText(string semanticKey, string type, Dictionary<string, object> parameters = null, string defaultResult = "");
        void Initialize(string body);
    }
}
