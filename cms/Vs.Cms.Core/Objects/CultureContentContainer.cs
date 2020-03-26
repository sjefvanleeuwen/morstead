using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.Cms.Core.Objects.Interfaces;

namespace Vs.Cms.Core.Objects
{
    public class CultureContentContainer : ICultureContentContainer
    {
        public Dictionary<CultureInfo, ICultureContent> Content { get; }

        public CultureContentContainer()
        {
            Content = new Dictionary<CultureInfo, ICultureContent>();
        }

        public void Add(CultureInfo cultureInfo, ICultureContent cultureContent)
        {
            Content[cultureInfo] = cultureContent;
        }

        public void AddRange(Dictionary<CultureInfo, ICultureContent> cultureContents)
        {
            foreach (var item in cultureContents)
            {
                Add(item.Key, item.Value);
            }
        }
    }
}
