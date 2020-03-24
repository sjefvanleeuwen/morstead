using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Objects
{
    public class ParsedContent : IParsedContent
    {
        private CultureInfo _defaultCulture;

        private Dictionary<CultureInfo, ICultureContent> _cultureContents = new Dictionary<CultureInfo, ICultureContent>();

        public void SetCultureContents(Dictionary<CultureInfo, ICultureContent> cultureContents)
        {
            _cultureContents = cultureContents;
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
            if (!_cultureContents.Any())
            {
                throw new IndexOutOfRangeException("There is no content defined.");
            }
            _defaultCulture = _cultureContents.Keys.First();
            return _cultureContents.Keys.First();
        }

        private bool IsValidCulture(CultureInfo cultureInfo)
        {
            return _cultureContents.ContainsKey(cultureInfo);
        }

        public void SetDefaultCulture(CultureInfo cultureInfo)
        {
            _defaultCulture = cultureInfo;
        }

        public ICultureContent GetContentByCulture(CultureInfo cultureInfo)
        {
            if (!_cultureContents.ContainsKey(cultureInfo))
            {
                throw new IndexOutOfRangeException($"There is no content defined for the culture {cultureInfo.Name}");
            }
            return _cultureContents[cultureInfo];
        }

        public ICultureContent GetDefaultContent()
        {
            var cultureInfo = GetDefaultCulture();
            return _cultureContents[cultureInfo];
        }
    }
}
