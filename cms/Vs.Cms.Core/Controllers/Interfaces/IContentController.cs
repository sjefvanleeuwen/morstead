using System.Globalization;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    public interface IContentController
    {
        void SetCulture(CultureInfo cultureInfo);
        string GetText(string semanticKey, FormElementContentType type, string defaultResult = null);
        string GetText(string semanticKey, string type, string defaultResult = null);
        void Initialize(string body);
        void SetParameters(string semanticKey, IParametersCollection parameters);
    }
}
