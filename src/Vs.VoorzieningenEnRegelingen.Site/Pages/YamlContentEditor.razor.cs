using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        #region variables

        #region parameters

        [Parameter]
        public INode BaseNode { get; set; }

        #endregion

        #region injectors

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IYamlStorageController YamlStorageController { get; set; }

        #endregion

        #region backing fields 

        private ValidationController _validationController;
        private string _name;
        private string _selectedValue;

        #endregion

        #region references

        private YamlTypeSelector _yamlTypeSelector { get; set; }

        #endregion

        #region properties

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
        
        private string SelectedValue { get => _selectedValue; set { _selectedValue = value; ValidationController.SelectedValue = value; HeaderInfo["YamlType"] = SelectedValue; } }

        private string Name { get => _name; set { _name = value; HeaderInfo["Naam"] = value; } }

        private bool ShowHeaderInfo => HeaderInfo.Any(h => !string.IsNullOrWhiteSpace(h.Value));

        private IDictionary<string, string> HeaderInfo = new Dictionary<string, string> { { "YamlType", string.Empty }, { "Naam", string.Empty } };
        
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

        #endregion

        #endregion

        #region methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                InitYamlFileInfos();
                await JSRuntime.InvokeAsync<object>("splitYamlContentEditor", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
            }
        }

        #region overrides

        protected override void SetMenu()
        {
            var menuItems = new List<IMenuItem>
                {
                    new MenuItem { Link = "/", Name = "Home" },
                    new MenuItem { Name = "Editor", SubMenuItems =
                        new List<MenuItem> {
                            new MenuItem { Link = "#", Name = "Nieuw", HtmlAttributes = new Dictionary<string, object> { { "data-toggle", "modal" }, { "data-target", "#newYamlModal" } } },
                            new MenuItem { IsDivider = true },
                            new MenuItem { Link = "#", Name = "Opslaan", OnClick = TrySave },
                            new MenuItem { Link = "#", Name = "Opslaan als...", HtmlAttributes = new Dictionary<string, object> { { "data-toggle", "modal" }, { "data-target", "#saveAsYamlModal" } } }
                        }
                    },
                    new MenuItem { Link = "/About", Name = "Over"}
                };
            Menu.SetMenuItems(menuItems);
        }

        #endregion

        #region invocables

        [JSInvokable("InvokeLayout")]
        public Task Layout()
        {
            return ValidationController.YamlEditor.Layout();
        }

        #endregion

        #region state methods

        public async void InitYamlFileInfos()
        {
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
                    Type = file.Directory
                });
            }
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async Task<string> WriteFile(YamlFileInfo yamlFileInfo, string content)
        {
            var contentId = await YamlStorageController.WriteYamlFile(yamlFileInfo.Type, yamlFileInfo.Name, content, yamlFileInfo.Id).ConfigureAwait(false);
            return contentId;
        }

        public async Task Load(string id)
        {
            var yamlFileInfo = YamlFileInfos?.FirstOrDefault(y => y.Id == id);
            
            Name = yamlFileInfo.Name;
            SelectedValue = yamlFileInfo.Type;
            var content = await YamlStorageController.GetYamlFileContent(yamlFileInfo.Id).ConfigureAwait(false);
            await ValidationController.YamlEditor.SetValue(content).ConfigureAwait(false);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        #endregion

        #region interactive methods

        private async void OpenNotification(string message)
        {
            await JSRuntime.InvokeAsync<object>("notify", new object[] { message }).ConfigureAwait(false);
        }

        private async Task ValidateNewYaml()
        {
            if (!string.IsNullOrWhiteSpace(_yamlTypeSelector.SelectedValue))
            {
                SelectedValue = _yamlTypeSelector.SelectedValue;
                Name = string.Empty;
                await ValidationController.YamlEditor.SetValue("").ConfigureAwait(false);
                await ToggleModal("newYamlModal").ConfigureAwait(false);
                return;
            }
            OpenNotification("Selecteer een type Yaml.");
        }

        public async void TrySave()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                 await ToggleModal("saveAsYamlModal").ConfigureAwait(false);
            }
            else
            {
                await Save().ConfigureAwait(false);
            }
        }

        public async Task Save()
        {
            if (!NameFilledCheck())
            {
                return;
            }

            var chars = Vs.Core.Extensions.StringExtensions.GetInvalidFileNameCharacters(Name);
            if (chars.Any())
            {
                OpenNotification("De naam bevat illegale tekens voor bestandsnamen");
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedValue))
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

            var yamlFileInfo = YamlFileInfos.FirstOrDefault(y => y.Name == Name && y.Type == SelectedValue);
            if (yamlFileInfo == null)
            {
                yamlFileInfo = new YamlFileInfo();
                YamlFileInfos.Add(yamlFileInfo);
            }
            yamlFileInfo.Name = Name.Trim();
            yamlFileInfo.Type = SelectedValue;

            yamlFileInfo.Id = await WriteFile(yamlFileInfo, content).ConfigureAwait(false);
            OpenNotification("Inhoud succesvol opgeslagen.");
        }

        private async Task SaveAsYaml()
        {
            if (!NameFilledCheck())
            {
                return;
            }
            await Save().ConfigureAwait(false);
            await ToggleModal("saveAsYamlModal").ConfigureAwait(false);
        }

        private async Task ToggleModal(string id)
        {
            await JSRuntime.InvokeAsync<object>("toggleModal", id).ConfigureAwait(false);
        }

        //checks
        private bool NameFilledCheck()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                OpenNotification("Er is geen naam ingevuld voor de Yaml.");
                return false;
            }
            return true;
        }

        #endregion

        #endregion
    }
}