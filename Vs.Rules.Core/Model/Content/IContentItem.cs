using System.Collections.Generic;
using System.Globalization;

namespace Vs.Rules.Core.Model.Content
{
    public interface IContentItem
    {
        IEnumerable<KeyValuePair<CultureInfo, string>> Body { get; set; }
    }
}
