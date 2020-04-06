using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.Cms.Core.Enums;
using Vs.Cms.Core.Objects.Interfaces;
using Vs.Core.Extensions;

namespace Vs.Cms.Core.Objects
{
    public class ContentHandler : IContentHandler
    {
        private CultureInfo _defaultCulture;

        private readonly ICultureContentContainer _cultureContentContainer;

        public ContentHandler(ICultureContentContainer cultureContentContainer)
        {
            _cultureContentContainer = cultureContentContainer;
        }

        public void AddCultureContents(Dictionary<CultureInfo, ICultureContent> cultureContents)
        {
            _cultureContentContainer.AddRange(cultureContents);
            _defaultCulture = null;
        }

        public CultureInfo GetDefaultCulture()
        {
            if (_defaultCulture != null && IsValidCulture(_defaultCulture))
            {
                return _defaultCulture;
            }
            var dutch = new CultureInfo("nl-NL");
            if (IsValidCulture(dutch))
            {
                _defaultCulture = dutch;
                return dutch;
            }
            var english = new CultureInfo("en-GB");
            if (IsValidCulture(english))
            {
                _defaultCulture = english;
                return english;
            }
            var american = new CultureInfo("en-US");
            if (IsValidCulture(english))
            {
                _defaultCulture = american;
                return american;
            }
            if (!_cultureContentContainer.Content.Any())
            {
                throw new IndexOutOfRangeException("There is no content defined.");
            }
            _defaultCulture = _cultureContentContainer.Content.Keys.First();
            return _cultureContentContainer.Content.Keys.First();
        }

        private bool IsValidCulture(CultureInfo cultureInfo)
        {
            return _cultureContentContainer.Content.Any() &&
                _cultureContentContainer.Content.ContainsKey(cultureInfo);
        }

        public void SetDefaultCulture(CultureInfo cultureInfo)
        {
            _defaultCulture = cultureInfo;
        }

        public ICultureContent GetContentByCulture(CultureInfo cultureInfo)
        {
            if (!_cultureContentContainer.Content.ContainsKey(cultureInfo))
            {
                throw new IndexOutOfRangeException($"There is no content defined for the culture '{cultureInfo.Name}'");
            }
            return _cultureContentContainer.Content[cultureInfo];
        }

        public ICultureContent GetDefaultContent()
        {
            var cultureInfo = GetDefaultCulture();
            return _cultureContentContainer.Content[cultureInfo];
        }

        public void TranslateParsedContentToContent(CultureInfo cultureInfo, IDictionary<string, object> parsedContent)
        {
            const string Content = "Content";
            const string Key = "key";

            if (!parsedContent.ContainsKey(Content))
            {
                throw new ArgumentException($"The ParsedContent does not have a top element '{Content}'.");
            }
            if (!(parsedContent[Content] is IEnumerable<object>))
            {
                throw new ArgumentException($"The ParsedContent element '{Content}' is not of the correct type.");
            }

            var cultureContent = new CultureContent();
            foreach (var item in parsedContent[Content] as IEnumerable<object>)
            {
                var subItems = item as IDictionary<string, object>;
                if (subItems == null)
                {
                    throw new ArgumentException($"An item in the ParsedContent element '{Content}' is not of the correct type.");
                }
                if (!subItems.ContainsKey(Key))
                {
                    throw new ArgumentException($"An item in the ParsedContent is missing the value '{Key}'.");
                }

                var keys = GetAllKeys(subItems[Key].ToString());
                var content = GetContent(subItems);
                foreach (var key in keys)
                {
                    cultureContent.AddContent(key, content);
                }
            }
            _cultureContentContainer.Add(cultureInfo, cultureContent);
        }

        private static Dictionary<FormElementContentType, object> GetContent(IDictionary<string, object> subItems)
        {
            var content = new Dictionary<FormElementContentType, object>();
            foreach (var formElementContentType in Enum.GetValues(typeof(FormElementContentType)).Cast<FormElementContentType>())
            {
                var label = formElementContentType.GetDescription();
                if (label != "key" && subItems.ContainsKey(label))
                {
                    content.Add(formElementContentType, subItems[label]);
                }
            }
            return content;
        }

        private IEnumerable<string> GetAllKeys(string keyString)
        {
            return keyString.Split(",").Select(s => s.Trim());
        }
    }
}
