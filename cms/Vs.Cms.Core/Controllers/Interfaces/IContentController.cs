using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.Core.Interfaces;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    public interface IContentController
    {
        IParametersCollection _parameters { get; set; }
        void SetCulture(CultureInfo cultureInfo);
        string GetText(string semanticKey, FormElementContentType type, string defaultResult = null);
        string GetText(string semanticKey, string type, string defaultResult = null);
        void Initialize(string body);
        IEnumerable<string> GetUnresolvedParameters(string semanticKey, IParametersCollection parameters);
        void SetParameters(IParametersCollection parameters);
    }
}
