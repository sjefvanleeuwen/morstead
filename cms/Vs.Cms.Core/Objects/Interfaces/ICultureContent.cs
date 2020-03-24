using System.Collections.Generic;
using Vs.Cms.Core.Enums;

namespace Vs.Cms.Core.Objects.Interfaces
{
    public interface ICultureContent
    {
        void AddContent(string semanticKey, Dictionary<FormElementContentType, string> content);
        void AddContent(string semanticKey, FormElementContentType type, string contentItem);
        string GetContent(string semanticKey, FormElementContentType type);
    }
}