using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class DialogData
    {
        public string Title { get; set; }
        public string CloseText { get; set; }
        public MarkupString Content { get; set; }
        public bool ShowOkButton { get; set; }
        public string OkText { get; set; }
        public Action OkAction { get; set; } = () => { };
    }
}
