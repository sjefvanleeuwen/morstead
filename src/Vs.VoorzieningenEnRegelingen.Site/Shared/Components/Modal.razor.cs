using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class Modal
    {
        [Parameter] public string Cancel { get; set; }
        [Parameter] public string Ok { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCancel { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnOk { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public void Close()
        {
            JSRuntime.InvokeAsync<object>("closeModal", new object[] { Id });
        }
    }
}
