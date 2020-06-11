﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Shared.Components;
using Vs.YamlEditor.Components.Controllers;
using Vs.YamlEditor.Components.Shared;
using YamlDotNet.Core;

namespace Vs.VoorzieningenEnRegelingen.Site.Pages
{
    public partial class YamlContentEditor
    {
        [Parameter]
        public INode BaseNode { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

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

        private IEnumerable<YamlType> DisabledTypes => ValidationController.DisabledTypes?.Keys ?? new List<YamlType>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<object>("split", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
            }
        }

        [JSInvokable("InvokeLayout")]
        public Task Layout()
        {
            return ValidationController.YamlEditor.Layout();
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

        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                OpenNotification("Geen naam ingevuld");
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
        }

        private void OpenNotification(string message)
        {
            JSRuntime.InvokeAsync<object>("notify", new object[] { message });
        }
    }
}