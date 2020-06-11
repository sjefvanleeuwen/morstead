using Microsoft.AspNetCore.Components;
using System;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class Dialog
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string CloseText { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public bool ShowOkButton { get; set; }
        [Parameter]
        public string OkText { get; set; }
        [Parameter]
        public Action OkAction { get; set; }

        private bool isOpen = false;

        public void OpenDialog()
        {
            isOpen = true;
        }
    }
}
