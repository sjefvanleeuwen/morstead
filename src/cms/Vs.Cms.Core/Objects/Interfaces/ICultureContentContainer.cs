using System.Collections.Generic;
using System.Globalization;

namespace Vs.Cms.Core.Objects.Interfaces
{
    public interface ICultureContentContainer
    {
        public Dictionary<CultureInfo, ICultureContent> Content { get; }

        void Add(CultureInfo cultureInfo, ICultureContent cultureContent);

        void AddRange(Dictionary<CultureInfo, ICultureContent> cultureContents);
    }
}
