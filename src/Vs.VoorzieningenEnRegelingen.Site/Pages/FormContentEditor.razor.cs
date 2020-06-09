using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.YamlEditor.Components.Controllers;

namespace Vs.VoorzieningenEnRegelingen.Site.Pages
{
    public partial class FormContentEditor
    {
        private ValidationController _validationController;
        private ValidationController ValidationController
        {
            get
            {
                if (_validationController == null)
                {
                    _validationController = new ValidationController();
                }
                return _validationController;
            }
        }

        [Parameter]
        public INode BaseNode { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               //await JSRuntime.InvokeAsync<object>("initBlazorMonacoYamlCallbacks", new object[] { DotNetObjectReference.Create(this) }).ConfigureAwait(false);
                await JSRuntime.InvokeAsync<object>("split",new object[] { DotNetObjectReference.Create(this), "layoutYamlEditor" }).ConfigureAwait(false);
            }
        }

        private string GetStyleForType(string type)
        {
            if (ValidationController.GetEnabledForType(type))
            {
                return string.Empty;
            }

            return "disabled";
        }

        [JSInvokable("layoutYamlEditor")]
        public Task Layout()
        {
            return ValidationController.YamlEditor.Layout();
        }
    }
}
