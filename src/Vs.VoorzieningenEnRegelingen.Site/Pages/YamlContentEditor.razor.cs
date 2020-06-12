using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables;
using Vs.YamlEditor.Components.Controllers;
using Vs.YamlEditor.Components.Shared;

namespace Vs.VoorzieningenEnRegelingen.Site.Pages
{
    public partial class YamlContentEditor
    {
        [Parameter]
        public INode BaseNode { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IYamlStorageController YamlStorageController { get; set; }

        private ValidationController _validationController;
        private YamlTypeSelector _yamlValidateTypeSelector;

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

        private YamlTypeSelector YamlValidateTypeSelector
        {
            get => _yamlValidateTypeSelector;
            set
            {
                ValidationController.YamlTypeSelector = value;
                _yamlValidateTypeSelector = value;
            }
        }

        private YamlTypeSelector YamlSaveTypeSelector { get; set; }

        private string Name { get; set; }

        private IList<YamlFileInfo> YamlFileInfos { get; set; } = new List<YamlFileInfo>();

        private Grid YamFileInfoGrid
        {
            get
            {
                var rows = new List<Row>();
                foreach (var yamlFileInfo in YamlFileInfos)
                {
                    rows.Add(new Row()
                    {
                        DisplayItems = new List<DisplayItem>
                        {
                            { new DisplayItem { Name = "Id", Value = yamlFileInfo.Id.ToString(), Display = false } },
                            { new DisplayItem { Name = "Name", Value = yamlFileInfo.Name, Display = true } },
                            { new DisplayItem { Name = "Type", Value = yamlFileInfo.Type, Display = true } }
                        },
                        TableActions = new List<TableAction> {
                            { new TableAction { Action = new Action (async () => await Load(yamlFileInfo.Id).ConfigureAwait(false)), IconName = "fa-edit" } }
                        }
                    });
                }
                return new Grid { Rows = rows };
            }
        }

        private IEnumerable<YamlType> DisabledTypes => ValidationController.DisabledTypes?.Keys ?? new List<YamlType>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                InitYamlFileInfos();
                await JSRuntime.InvokeAsync<object>("split", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
            }
        }

        [JSInvokable("InvokeLayout")]
        public Task Layout()
        {
            return ValidationController.YamlEditor.Layout();
        }
        
        public async void InitYamlFileInfos()
        {
            var fileList = await YamlStorageController.GetYamlFiles().ConfigureAwait(false);
        }

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                OpenNotification("Geen naam ingevuld");
                return;
            }
             
            var chars = Vs.Core.Extensions.StringExtensions.GetInvalidFileNameCharacters(Name);
            if (chars.Any())
            {
                OpenNotification("De naam bevat illegale tekens voor bestandsnamen");
                return;
            }
            if (string.IsNullOrWhiteSpace(YamlSaveTypeSelector.SelectedValue))
            {
                OpenNotification("Geen type document geselecteerd");
                return;
            }
            var yamlFileInfo = YamlFileInfos.FirstOrDefault(y => y.Name == Name && y.Type == YamlSaveTypeSelector.SelectedValue);
            if (yamlFileInfo == null)
            {
                yamlFileInfo = new YamlFileInfo();
                YamlFileInfos.Add(yamlFileInfo);
            }
            yamlFileInfo.Id = Guid.NewGuid();
            yamlFileInfo.Name = Name.Trim();
            yamlFileInfo.Content = await ValidationController.YamlEditor.GetValue().ConfigureAwait(false);
            yamlFileInfo.Type = YamlSaveTypeSelector.SelectedValue;

            WriteFile(yamlFileInfo);
        }

        private async void WriteFile(YamlFileInfo yamlFileInfo)
        {
            YamlStorageController.WriteYamlFile(yamlFileInfo.Type, yamlFileInfo.Name, yamlFileInfo.Content);
        }

        public async Task Load(Guid id)
        {
            var yamlFileInfo = YamlFileInfos?.FirstOrDefault(y => y.Id == id);
            Name = yamlFileInfo.Name;
            YamlSaveTypeSelector.SelectedValue = yamlFileInfo.Type;
            await ValidationController.YamlEditor.SetValue(yamlFileInfo?.Content ?? string.Empty).ConfigureAwait(false);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async void OpenNotification(string message)
        {
            await JSRuntime.InvokeAsync<object>("notify", new object[] { message }).ConfigureAwait(false);
        }
    }
}