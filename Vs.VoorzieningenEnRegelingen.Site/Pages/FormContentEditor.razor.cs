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
                await JSRuntime.InvokeAsync<object>("split", new object[] { });
                await JSRuntime.InvokeAsync<object>("initializeTree", new object[] { });
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
    }
}
