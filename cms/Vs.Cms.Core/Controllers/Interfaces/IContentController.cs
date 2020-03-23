using Vs.Cms.Core.Constants;

namespace Vs.Cms.Core.Controllers.Interfaces
{
    interface IContentController
    {
        string GetText(FormElementContentType question, string semanticKey);
    }
}
