using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    public interface IContentController
    {
        void SetCulture(CultureInfo cultureInfo);
        string GetText(string semanticKey, FormElementContentType type);
        string GetText(string semanticKey, FormElementContentType type, IParametersCollection parameters);
        string GetText(string semanticKey, FormElementContentType type, IParametersCollection parameters, string defaultResult);
        string GetText(string semanticKey, string type);
        string GetText(string semanticKey, string type, IParametersCollection parameters);
        string GetText(string semanticKey, string type, IParametersCollection parameters, string defaultResult);
        void Initialize(string body);
    }
}
