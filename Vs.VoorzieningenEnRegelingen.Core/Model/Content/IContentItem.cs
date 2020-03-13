using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Vs.VoorzieningenEnRegelingen.Core.Model.Content
{
    public interface IContentItem
    {
        IEnumerable<KeyValuePair<CultureInfo, string>> Body { get; set; }
    }
}
