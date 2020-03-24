using System.Collections.Generic;
using System.Globalization;

namespace Vs.Cms.Core.Objects.Interfaces
{
    public interface IParsedContent
    {
        void SetCultureContents(Dictionary<CultureInfo, ICultureContent> cultureContents);

        void SetDefaultCulture(CultureInfo cultureInfo);

        CultureInfo GetDefaultCulture();

        ICultureContent GetContentByCulture(CultureInfo cultureinfo);

        ICultureContent GetDefaultContent();
    }
}
