using System;
using System.Collections.Generic;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Objects
{
    public class CultureContent : ICultureContent
    {
        private Dictionary<string, Dictionary<FormElementContentType, object>> _semanticContent = new Dictionary<string, Dictionary<FormElementContentType, object>>();

        public void AddContent(string semanticKey, Dictionary<FormElementContentType, object> content)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                _semanticContent[semanticKey] = new Dictionary<FormElementContentType, object>();
            }
            _semanticContent[semanticKey] = content;
        }

        public void AddContent(string semanticKey, FormElementContentType type, object contentItem)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                _semanticContent[semanticKey] = new Dictionary<FormElementContentType, object>();
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