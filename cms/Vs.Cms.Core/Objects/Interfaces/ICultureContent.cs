using System.Collections.Generic;
using Vs.Cms.Core.Enums;

namespace Vs.Cms.Core.Objects.Interfaces
{
    public interface ICultureContent
    {
        void AddContent(string semanticKey, IDictionary<string, object> content);
        void AddContent(string semanticKey, string type, object contentItem);
        IEnumerable<object> GetCompleteContent(string semanticKey);
        object GetContent(string semanticKey, FormElementContentType type);
        object GetContent(string semanticKey, string type);
    }
}