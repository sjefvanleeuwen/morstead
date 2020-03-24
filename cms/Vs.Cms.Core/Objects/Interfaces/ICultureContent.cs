using System.Collections.Generic;
using Vs.Cms.Core.Enums;

namespace Vs.Cms.Core.Objects.Interfaces
{
    public interface ICultureContent
    {
        void AddContent(string semanticKey, Dictionary<FormElementContentType, object> content);
        void AddContent(string semanticKey, FormElementContentType type, object contentItem);
        object GetContent(string semanticKey, FormElementContentType type);
    }
}