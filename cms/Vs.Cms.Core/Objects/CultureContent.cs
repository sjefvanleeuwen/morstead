using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Returns the content for the semantic key and all content for all semantic keys that start the same
        /// </summary>
        /// <param name="semanticKey"></param>
        /// <returns></returns>
        public IEnumerable<object> GetCompleteContent(string semanticKey)
        {
            var contents = _semanticContent.Where(s => s.Key.StartsWith(semanticKey));
            if (!contents.Any())
            {
                throw new IndexOutOfRangeException($"There is no content defined for key '{semanticKey}'");
            }
            var result = new List<object>();
            foreach (var content in contents)
            {
                result.AddRange(content.Value.Select(c => c.Value));
            }
            return result;
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