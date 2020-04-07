using System;
using System.Collections.Generic;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects.Interfaces;
using Vs.Core.Extensions;

namespace Vs.Cms.Core.Objects
{
    public class CultureContent : ICultureContent
    {
        private IDictionary<string, IDictionary<string, object>> _semanticContent = new Dictionary<string, IDictionary<string, object>>();

        public void AddContent(string semanticKey, IDictionary<string, object> content)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                _semanticContent[semanticKey] = new Dictionary<string, object>();
            }
            _semanticContent[semanticKey] = content;
        }

        public void AddContent(string semanticKey, string type, object contentItem)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                _semanticContent[semanticKey] = new Dictionary<string, object>();
            }
            if (!_semanticContent[semanticKey].ContainsKey(type))
            {
                _semanticContent[semanticKey].Add(type, contentItem);
                return;
            }
            _semanticContent[semanticKey][type] = contentItem;
        }

        public object GetContent(string semanticKey, FormElementContentType type)
        {
            return GetContent(semanticKey, type.GetDescription());
        }

        public object GetContent(string semanticKey, string type)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                throw new IndexOutOfRangeException($"There is no content defined for key '{semanticKey}'");
            }
            if (!_semanticContent[semanticKey].ContainsKey(type))
            {
                return null;
            }
            return _semanticContent[semanticKey][type];
        }

    }
}