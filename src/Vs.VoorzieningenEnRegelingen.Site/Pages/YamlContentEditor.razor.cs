using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.YamlEditor.Components.Controllers;

namespace Vs.VoorzieningenEnRegelingen.Site.Pages
{
    public partial class YamlContentEditor
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
                await JSRuntime.InvokeAsync<object>("split", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
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

        [JSInvokable("InvokeLayout")]
        public Task Layout()
        {
            return ValidationController.YamlEditor.Layout();
        }

        private string Name { get; set; }

        private IList<YamlFileInfo> YamlFileInfos { get; set; } = new List<YamlFileInfo>();

        public async Task Save()
        {
            var yamlFileInfo = YamlFileInfos.FirstOrDefault(y => y.Name == Name);
            if (yamlFileInfo == null) {
                yamlFileInfo = new YamlFileInfo();
                YamlFileInfos.Add(yamlFileInfo);
            }
            yamlFileInfo.Name = Name.Trim();
            yamlFileInfo.Content = await ValidationController.YamlEditor.GetValue().ConfigureAwait(false);
        }
    }
}