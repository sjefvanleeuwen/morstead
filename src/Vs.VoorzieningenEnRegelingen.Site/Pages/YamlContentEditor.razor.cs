using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.ProfessionalPortal.Morstead.Client.Controllers.Interfaces;
using Vs.ProfessionalPortal.Morstead.Client.Models;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables;
using Vs.YamlEditor.Components.Controllers.Interfaces;
using Vs.YamlEditor.Components.Helpers;
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

        public bool ShowTabs => EditorTabController.EditorTabInfos.Values.Any(e => e.IsVisible);

        public IEnumerable<int> keysVisibleTabs => EditorTabController.EditorTabInfos
                                                    .Where(e => e.Value.IsVisible)
                                                    .OrderBy(e => e.Value.OrderNr)
                                                    .Select(e => e.Key);

        public IEnumerable<int> keysAllTabs => EditorTabController.EditorTabInfos
                                                    .OrderBy(e => e.Value.OrderNr)
                                                    .Select(e => e.Key);

        public int ActiveTab => EditorTabController.EditorTabInfos.Values?.FirstOrDefault(e => e.IsActive)?.TabId ?? 0;

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
                            { new TableAction { Action = new Action (async () => await Load(savedYamlInfo.ContentId).ConfigureAwait(false)), IconName = "fa-edit" } },
                            { new TableAction { Action = new Action (async () => await Compare(savedYamlInfo.ContentId).ConfigureAwait(false)), IconName = /*"fa-exchange"*/ "fa-check" } }
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

        #region interactive methods

        private void SwitchToTab(int tabId)
        {
            EditorTabController.Activate(tabId);
            Layout();
        }

        private void CloseTab(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            editorTabInfo.IsVisible = false;
            //set the next one visible
            var newActiveTab = GetTabToRight(tabId);
            if (newActiveTab == null)
            {
                newActiveTab = GetTabToLeft(tabId);
            }
            EditorTabController.Activate(newActiveTab?.TabId ?? 0);

            //draw all tabs again
            StateHasChanged();
        }

        private void MoveTabLeft(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            var currentOrderNr = editorTabInfo.OrderNr;

            var tabToLeft = GetTabToLeft(tabId);
            if (tabToLeft == null)
            {
                return;
            }
            //switch order numbers;
            var leftOrderNr = tabToLeft.OrderNr;
            tabToLeft.OrderNr = currentOrderNr;
            editorTabInfo.OrderNr = leftOrderNr;

            //draw all tabs again
            StateHasChanged();
        }

        private void MoveTabRight(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            var currentOrderNr = editorTabInfo.OrderNr;

            var tabToRight = GetTabToRight(tabId);
            if (tabToRight == null)
            {
                return;
            }
            //switch order numbers;
            var rightOrderNr = tabToRight.OrderNr;
            tabToRight.OrderNr = currentOrderNr;
            editorTabInfo.OrderNr = rightOrderNr;

            //draw all tabs again
            StateHasChanged();
        }

        private async void ContentModification(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            editorTabInfo.IsSaved = false;
            //get yaml now so it doesn't have to be retrieved multiple times
            var yaml = await editorTabInfo.YamlEditor.GetValue().ConfigureAwait(false);

            StartValidationSubmitCountdown(editorTabInfo, yaml);
            TrackContentChanged(editorTabInfo, yaml);
        }

        private async void TrackContentChanged(IEditorTabInfo editorTabInfo, string yaml)
        {
            var hasChangesBefore = editorTabInfo.HasChanges;
            editorTabInfo.Content = yaml;
            var hasChangesAfter = editorTabInfo.HasChanges;
            if (hasChangesAfter != hasChangesBefore)
            {
                //update the tab name
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            }
        }

        private async void StartValidationSubmitCountdown(IEditorTabInfo editorTabInfo, string yaml)
        {
            var type = editorTabInfo.Type;
            var formattingExceptions = await ValidationController.StartSubmitCountdown(type, yaml).ConfigureAwait(false);
            await DeltaDecorationHelper.SetDeltaDecorationsFromExceptions(editorTabInfo.YamlEditor, formattingExceptions).ConfigureAwait(false);
            editorTabInfo.HasErrors = formattingExceptions?.Any() ?? false;
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async void OpenNotification(string message)
        {
            await JSRuntime.InvokeAsync<object>("notify", new object[] { message }).ConfigureAwait(false);
        }

        private async Task AddNewTab()
        {
            if (string.IsNullOrWhiteSpace(_yamlTypeSelector.SelectedValue))
            {
                OpenNotification("Selecteer een type Yaml.");
                return;
            }

            var editorTabInfo = new EditorTabInfo
            {
                OrderNr = EditorTabController.GetNextOrderNr(),
                Content = string.Empty,
                ContentId = null,
                IsVisible = true,
                Name = string.Empty,
                Type = _yamlTypeSelector.SelectedValue,
                YamlEditor = null 
            };

            EditorTabController.AddTab(editorTabInfo);
            //create the YamlFileEditor in the interface
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            await ToggleModal("newYamlModal").ConfigureAwait(false);
        }

        private async void TrySave()
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

        private async Task SaveAsYaml()
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
            if (!NameFilledCheck(SaveAsName))
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

        #endregion

        #region private methods

        private IEditorTabInfo GetTabToLeft(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            var currentOrderNr = editorTabInfo.OrderNr;

            var openTabsToLeft = EditorTabController.EditorTabInfos?.Where(e => e.Value.IsVisible && e.Value.OrderNr < currentOrderNr);
            if (!openTabsToLeft.Any())
            {
                return null;
            }

            var leftOrderNr = openTabsToLeft.Max(e => e.Value.OrderNr);

            //switch order numbers;
            return EditorTabController.EditorTabInfos.Where(e => e.Value.OrderNr == leftOrderNr).First().Value;
        }

        private IEditorTabInfo GetTabToRight(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            var currentOrderNr = editorTabInfo.OrderNr;

            var openTabsToRight = EditorTabController.EditorTabInfos?.Where(e => e.Value.IsVisible && e.Value.OrderNr > currentOrderNr);
            if (!openTabsToRight.Any())
            {
                return null;
            }

            var rightOrderNr = openTabsToRight.Min(e => e.Value.OrderNr);

            //switch order numbers;
            return EditorTabController.EditorTabInfos.Where(e => e.Value.OrderNr == rightOrderNr).First().Value;
        }

        private async void InitEditorTabInfos()
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
            editorTabInfo.OrderNr = editorTabInfo.IsVisible ? editorTabInfo.OrderNr : EditorTabController.GetNextOrderNr();
            editorTabInfo.IsVisible = true;
            editorTabInfo.ContentId = contentId;
            editorTabInfo.Name = savedYamlInfo.Name;
            editorTabInfo.Type = savedYamlInfo.Type;
            editorTabInfo.OriginalContent = await YamlStorageController.GetYamlFileContent(editorTabInfo.ContentId).ConfigureAwait(false);
            editorTabInfo.Content = editorTabInfo.OriginalContent;
            //add the tab if it didnt already exist
            EditorTabController.AddTab(editorTabInfo);
            //create the YamlFileEditor in the interface
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        public async Task Compare(string contentId)
        {
            //TODO add the compare content to the second split screen
            var compareContent = await YamlStorageController.GetYamlFileContent(contentId).ConfigureAwait(false);
        }

        private async Task Save()
        {
            if (!NameFilledCheck())
            {
                return;
            }

            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
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

            editorTabInfo.ContentId = yamlFileInfo.ContentId;
            editorTabInfo.IsSaved = true;
            editorTabInfo.OriginalContent = editorTabInfo.Content;
            //update the tab name
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false); 
        }

        private async Task ToggleModal(string modalId)
        {
            await JSRuntime.InvokeAsync<object>("toggleModal", modalId).ConfigureAwait(false);
        }

        //checks
        private bool NameFilledCheck(string name = null)
        {
            var nameToCheck = name ?? EditorTabController.GetTabByTabId(ActiveTab).Name;
            if (string.IsNullOrWhiteSpace(nameToCheck))
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