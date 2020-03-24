using System;
using System.Collections.Generic;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Objects
{
    public class CultureContent : ICultureContent
    {
        private Dictionary<string, Dictionary<FormElementContentType, string>> _semanticContent = new Dictionary<string, Dictionary<FormElementContentType, string>>();

        public void AddContent(string semanticKey, Dictionary<FormElementContentType, string> content)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                _semanticContent[semanticKey] = new Dictionary<FormElementContentType, string>();
            }
            _semanticContent[semanticKey] = content;
        }

        public void AddContent(string semanticKey, FormElementContentType type, string contentItem)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                _semanticContent[semanticKey] = new Dictionary<FormElementContentType, string>();
            }
            if (!_semanticContent[semanticKey].ContainsKey(type))
            {
                _semanticContent[semanticKey].Add(type, contentItem);
                return;
            }
            _semanticContent[semanticKey][type] = contentItem;
        }

        public string GetContent(string semanticKey, FormElementContentType type)
        {
            if (!_semanticContent.ContainsKey(semanticKey))
            {
                throw new IndexOutOfRangeException($"There is no content defined for key {semanticKey}");
            }
            if (!_semanticContent[semanticKey].ContainsKey(type))
            {
                throw new IndexOutOfRangeException($"There is no content defined for key {semanticKey} - {type}");
            }
            return _semanticContent[semanticKey][type];
        }
    }
}