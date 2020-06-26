using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces;
using Vs.ProfessionalPortal.Morstead.Client.Models;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables;
using Vs.YamlEditor.Components.Controllers.Interfaces;
using Vs.YamlEditor.Components.Shared;

namespace Vs.VoorzieningenEnRegelingen.Site.Pages
{
    public partial class YamlContentEditor
    {
        #region variables

        #region injectors

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IEditorTabController EditorTabController { get; set; }

        [Inject]
        protected IYamlStorageController YamlStorageController { get; set; }

        [Inject]
        protected IValidationController ValidationController { get; set; }

        #endregion

        #region references

        private YamlTypeSelector _yamlTypeSelector { get; set; }

        #endregion

        #region properties

        public int ActiveTab { get; set; }

        public string SaveAsName { get; set; }

        private IList<IYamlFileInfo> SavedYamls { get; set; } = new List<IYamlFileInfo>();

        private Grid YamFileInfoGrid
        {
            get
            {
                var rows = new List<Row>();
                foreach (var savedYamlInfo in SavedYamls)
                {
                    rows.Add(new Row()
                    {
                        DisplayItems = new List<DisplayItem>
                        {
                            { new DisplayItem { Name = "Id", Value = savedYamlInfo.ContentId.ToString(), Display = false } },
                            { new DisplayItem { Name = "Name", Value = savedYamlInfo.Name, Display = true } },
                            { new DisplayItem { Name = "Type", Value = savedYamlInfo.Type, Display = true } }
                        },
                        TableActions = new List<TableAction> {
                            { new TableAction { Action = new Action (async () => await Load(savedYamlInfo.ContentId).ConfigureAwait(false)), IconName = "fa-edit" } }
                        }
                    });
                }
                return new Grid { Rows = rows };
            }
        }

        #endregion

        #endregion

        #region methods

        #region overrides

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                InitEditorTabInfos();
                await JSRuntime.InvokeAsync<object>("splitYamlContentEditor", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
            }
        }

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
            if (ActiveTab == 0)
            {
                return null;
            }

            return EditorTabController.GetTabByTabId(ActiveTab).YamlEditor.Layout();
        }

        #endregion

        #region state methods

        public async void InitEditorTabInfos()
        {
            var fileList = await YamlStorageController.GetYamlFiles().ConfigureAwait(false);
            await AddFileListToEditorTabInfos(fileList).ConfigureAwait(false);
        }

        private async Task AddFileListToEditorTabInfos(IEnumerable<FileInformation> fileList)
        {
            foreach (var file in fileList)
            {
                SavedYamls.Add(new YamlFileInfo
                {
                    ContentId = file.Id,
                    Name = file.FileName,
                    Type = file.Directory
                });
            }
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async Task<string> WriteFile(IYamlFileInfo yamlFileInfo, string content)
        {
            var contentId = await YamlStorageController.WriteYamlFile(yamlFileInfo.Type, yamlFileInfo.Name, content, yamlFileInfo.ContentId).ConfigureAwait(false);
            return contentId;
        }

        public async Task Load(string contentId)
        {
            //get how it is saved
            var savedYamlInfo = SavedYamls?.FirstOrDefault(y => y.ContentId == contentId);
            //get the correct EditorTabInfo if it already exists
            var editorTabInfo = EditorTabController.GetTabByContentId(contentId) ?? new EditorTabInfo();
            //assign the correct values from the load
            editorTabInfo.ContentId = contentId;
            editorTabInfo.Name = savedYamlInfo.Name;
            editorTabInfo.Type = savedYamlInfo.Type;
            editorTabInfo.Content = await YamlStorageController.GetYamlFileContent(editorTabInfo.ContentId).ConfigureAwait(false);
            //add the tab if it didnt already exist
            EditorTabController.AddTab(editorTabInfo);
            ActiveTab = editorTabInfo.TabId;
            //create the YamlFileEditor in the interface
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            //TODO Fix This
            //Thread.Sleep(5000);
            //await editorTabInfo.YamlEditor.SetValue(editorTabInfo.Content).ConfigureAwait(false);
            //await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        #endregion

        #region interactive methods

        private async void StartSubmitCountdown(int tabId)
        {
            var info = EditorTabController.GetTabByTabId(tabId);
            var type = info.Type;
            var yaml = await info.YamlEditor.GetValue().ConfigureAwait(false);
            await ValidationController.StartSubmitCountdown(type, yaml).ConfigureAwait(false);
        }

        private async void OpenNotification(string message)
        {
            await JSRuntime.InvokeAsync<object>("notify", new object[] { message }).ConfigureAwait(false);
        }

        private async Task AddNewYaml()
        {
            if (string.IsNullOrWhiteSpace(_yamlTypeSelector.SelectedValue))
            {
                OpenNotification("Selecteer een type Yaml.");
                return;
            }

            var editorTabInfo = new EditorTabInfo
            {
                Content = string.Empty,
                ContentId = null,
                IsOpen = true,
                Name = string.Empty,
                Type = _yamlTypeSelector.SelectedValue,
                YamlEditor = null 
            };

            EditorTabController.AddTab(editorTabInfo);
            ActiveTab = editorTabInfo.TabId;
            //create the YamlFileEditor in the interface
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            await ToggleModal("newYamlModal").ConfigureAwait(false);
        }

        public async void TrySave()
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
            if (string.IsNullOrWhiteSpace(editorTabInfo.Name))
            {
                await ToggleModal("saveAsYamlModal").ConfigureAwait(false);
            }
            else
            {
                await Save().ConfigureAwait(false);
            }
        }

        public async Task SaveAsYaml()
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
            if (!NameFilledCheck(editorTabInfo))
            {
                return;
            }
            if (NameIsValid(SaveAsName))
            {
                editorTabInfo.Name = SaveAsName.Trim();
                SaveAsName = string.Empty;
                await Save().ConfigureAwait(false);
                await ToggleModal("saveAsYamlModal").ConfigureAwait(false);
            }
        }

        private async Task Save()
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
            if (!NameFilledCheck(editorTabInfo))
            {
                return;
            }

            if (!NameIsValid(editorTabInfo.Name))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(editorTabInfo.Type))
            {
                OpenNotification("Geen type document geselecteerd");
                return;
            }

            var content = await editorTabInfo.YamlEditor.GetValue().ConfigureAwait(false);
            if (string.IsNullOrEmpty(content))
            {
                OpenNotification("Er is geen inhoud in het bestand");
                return;
            }

            var yamlFileInfo = SavedYamls.FirstOrDefault(y => y.Name == editorTabInfo.Name && y.Type == editorTabInfo.Type);
            if (yamlFileInfo == null)
            {
                yamlFileInfo = new YamlFileInfo();
                SavedYamls.Add(yamlFileInfo);
            }
            yamlFileInfo.Name = editorTabInfo.Name;
            yamlFileInfo.Type = editorTabInfo.Type;

            yamlFileInfo.ContentId = await WriteFile(yamlFileInfo, content).ConfigureAwait(false);
            OpenNotification("Inhoud succesvol opgeslagen.");
        }

        private async Task ToggleModal(string modalId)
        {
            await JSRuntime.InvokeAsync<object>("toggleModal", modalId).ConfigureAwait(false);
        }

        //checks
        private bool NameFilledCheck(IEditorTabInfo editorTabInfo)
        {
            if (string.IsNullOrWhiteSpace(editorTabInfo.Name))
            {
                OpenNotification("Er is geen naam ingevuld voor de Yaml.");
                return false;
            }
            return true;
        }

        private bool NameIsValid(string name)
        {
            var chars = Vs.Core.Extensions.StringExtensions.GetInvalidFileNameCharacters(name);
            if (chars.Any())
            {
                OpenNotification("De naam bevat illegale tekens voor bestandsnamen");
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}