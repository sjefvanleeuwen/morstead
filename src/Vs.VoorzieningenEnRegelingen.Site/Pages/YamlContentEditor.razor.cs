using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces;
using Vs.ProfessionalPortal.Morstead.Client.Models;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;
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
                await JSRuntime.InvokeAsync<object>("splitYamlContentEditor", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
            }
        }

        [JSInvokable("InvokeLayout")]
        public Task Layout()
        {
            return ValidationController.YamlEditor.Layout();
        }
        
        public async void InitYamlFileInfos()
        {
            //await YamlStorageController.WriteYamlFile("Content", "Naam 1", "Content 1").ConfigureAwait(false);
            //await YamlStorageController.WriteYamlFile("Layer", "Naam 2", "Layer 1").ConfigureAwait(false);
            //await YamlStorageController.WriteYamlFile("Rule", "The One Rule", "The Rule to rule all Rules").ConfigureAwait(false);
            //var contentId = await YamlStorageController.WriteYamlFile("Rule", "The Second Rule", "A second rule to rule them all and in the darkness bind them.").ConfigureAwait(false);
            //await YamlStorageController.WriteYamlFile("Rule", "The Second Rule", "A second rule to rule them all and in the darkness bind them. revised", contentId).ConfigureAwait(false);
            var fileList = await YamlStorageController.GetYamlFiles().ConfigureAwait(false);
            await AddFileListToYamlFileInfos(fileList).ConfigureAwait(false);
        }

        private async Task AddFileListToYamlFileInfos(IEnumerable<FileInformation> fileList)
        {
            foreach (var file in fileList)
            {
                YamlFileInfos.Add(new YamlFileInfo
                {
                    Id = file.Id,
                    Name = file.FileName,
                    Type = file.Directory,
                    Content = file.Content
                });
            }
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
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
            var content = await ValidationController.YamlEditor.GetValue().ConfigureAwait(false);
            if (string.IsNullOrEmpty(content))
            {
                OpenNotification("Er is geen inhoud in het bestand");
                return;
            }
            var yamlFileInfo = YamlFileInfos.FirstOrDefault(y => y.Name == Name && y.Type == YamlSaveTypeSelector.SelectedValue);
            if (yamlFileInfo == null)
            {
                yamlFileInfo = new YamlFileInfo();
                YamlFileInfos.Add(yamlFileInfo);
            }
            yamlFileInfo.Name = Name.Trim();
            yamlFileInfo.Content = content;
            yamlFileInfo.Type = YamlSaveTypeSelector.SelectedValue;

            yamlFileInfo.Id = await WriteFile(yamlFileInfo).ConfigureAwait(false);
        }

        private async Task<string> WriteFile(YamlFileInfo yamlFileInfo)
        {
            var contentId = await YamlStorageController.WriteYamlFile(yamlFileInfo.Type, yamlFileInfo.Name, yamlFileInfo.Content, yamlFileInfo.Id).ConfigureAwait(false);
            return contentId;
        }

        public async Task Load(string id)
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