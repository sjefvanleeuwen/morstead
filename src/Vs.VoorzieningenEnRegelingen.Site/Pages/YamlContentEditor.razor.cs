using BlazorMonaco.Bridge;
using BlazorMonacoYaml;
using BlazorMonacoYaml.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.Definitions.Models;
using Vs.Definitions.Repositories;
using Vs.VoorzieningenEnRegelingen.Site.ApiCalls;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model.Tables;
using Vs.VoorzieningenEnRegelingen.Site.Shared;

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
        protected IFileRepository FileRepository { get; set; }

        [Inject]
        protected IYamlValidationController YamlValidationController { get; set; }

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
                InitSavedFiles();
                await JSRuntime.InvokeAsync<object>("splitYamlContentEditor", new object[] { DotNetObjectReference.Create(this), "InvokeLayout" }).ConfigureAwait(false);
            }
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
        }


        protected override void SetMenu()
        {
            var editorMenuItems =
                new List<MenuItem> {
                    new MenuItem { Link = "#", Name = "Nieuw", HtmlAttributes = new Dictionary<string, object> { { "data-toggle", "modal" }, { "data-target", "#newYamlModal" } } }
                };

            if (ActiveTab > 0)
            {
                editorMenuItems.Add(new MenuItem { IsDivider = true });
                editorMenuItems.Add(new MenuItem { Link = "#", Name = "Opslaan", OnClick = TrySave });
                editorMenuItems.Add(new MenuItem { Link = "#", Name = "Opslaan als...", HtmlAttributes = new Dictionary<string, object> { { "data-toggle", "modal" }, { "data-target", "#saveAsYamlModal" } } });
            }

            var menuItems = new List<IMenuItem>
                {
                    new MenuItem { Link = "/", Name = "Home" },
                    new MenuItem { Name = "Editor", SubMenuItems =
                        editorMenuItems
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
                return default;
            }

            return EditorTabController.GetTabByTabId(ActiveTab).MonacoEditorYaml.Layout();
        }

        #endregion

        #region methods called from razor

        private async void CloseDiff(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            editorTabInfo.CompareInfo = null;
            await editorTabInfo.MonacoEditorYaml.SetValue(editorTabInfo.Content).ConfigureAwait(false);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async void CloseTab(int tabId)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            editorTabInfo.IsVisible = false;

            CloseDiff(tabId);
            if (tabId == ActiveTab)
            {
                //set the next one visible
                var newActiveTab = GetTabToRight(tabId);
                if (newActiveTab == null)
                {
                    newActiveTab = GetTabToLeft(tabId);
                }
                EditorTabController.Activate(newActiveTab?.TabId ?? 0);
            }

            //possibly reset the Menu
            if (ActiveTab == 0)
            {
                SetMenu();
            }

            //draw all tabs & menu again
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
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

        private async void RegisterContentModification(int tabId, bool actionIsOnDiffEditor = false)
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(tabId);
            editorTabInfo.IsSaved = false;
            string yaml;
            //get yaml now so it doesn't have to be retrieved multiple times
            if (!actionIsOnDiffEditor)
            {
                yaml = await editorTabInfo.MonacoEditorYaml.GetValue().ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(yaml))
                {
                    await editorTabInfo.MonacoEditorYaml.ResetDeltaDecorations().ConfigureAwait(false);
                    YamlValidationController.CancelSubmitCountdown();
                }
                else
                {
                    StartValidationSubmitCountdown(editorTabInfo, yaml);
                }
            }
            else
            {
                yaml = await editorTabInfo.MonacoDiffEditorYaml.GetModifiedValue().ConfigureAwait(false);
            }

            TrackContentChanged(editorTabInfo, yaml);
        }

        private void SwitchToTab(int tabId)
        {
            //remove all errors
            EditorTabController.GetTabByTabId(ActiveTab).RemoveExceptions();
            EditorTabController.Activate(tabId);
        }

        private void TabIsInitialised()
        {
            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
            if (!editorTabInfo.IsCompareMode && !string.IsNullOrWhiteSpace(editorTabInfo.Content))
            {
                StartValidationSubmitCountdown(editorTabInfo, editorTabInfo.Content, 0);
            }
            //call layout just in cases
            Layout();
        }

        #endregion

        #region methods called from cs file

        private async Task AddFileListToSavedYamls(IEnumerable<FileInformation> fileList)
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

        private async Task AddNewTab()
        {
            var currentTab = ActiveTab;

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
                MonacoEditorYaml = null
            };

            EditorTabController.AddTab(editorTabInfo);

            if (currentTab == 0)
            {
                SetMenu();
            }

            //create the YamlFileEditor in the interface
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            await ToggleModal("newYamlModal").ConfigureAwait(false);
        }

        private async Task Compare(string contentId)
        {
            var compareInfo = SavedYamls.FirstOrDefault(s => s.ContentId == contentId);
            if (compareInfo == null)
            {
                return;
            }

            var editorTabInfo = EditorTabController.GetTabByTabId(ActiveTab);
            editorTabInfo.CompareInfo = compareInfo;

            editorTabInfo.Content = await editorTabInfo.MonacoEditorYaml.GetValue().ConfigureAwait(false);
            editorTabInfo.CompareContent = await FileRepository.GetFileContent(contentId).ConfigureAwait(false);

            //set the value if it is already initiated; otherwise the content is not drawn again (no update detected in Blazor)
            if (editorTabInfo.MonacoDiffEditorYaml != null)
            {
                await editorTabInfo.MonacoDiffEditorYaml.SetOriginalValue(editorTabInfo.CompareContent).ConfigureAwait(false);
            }

            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

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

        private async void InitSavedFiles()
        {
            var fileList = await FileRepository.GetAllFiles().ConfigureAwait(false);
            await AddFileListToSavedYamls(fileList).ConfigureAwait(false);
        }

        private async Task Load(string contentId)
        {
            var currentTab = ActiveTab;

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
            editorTabInfo.OriginalContent = await FileRepository.GetFileContent(editorTabInfo.ContentId).ConfigureAwait(false);
            editorTabInfo.Content = editorTabInfo.OriginalContent;

            //remove all errors if there were set
            editorTabInfo.RemoveExceptions();

            if (editorTabInfo.MonacoEditorYaml != null)
            {
                await editorTabInfo.MonacoEditorYaml.ResetDeltaDecorations().ConfigureAwait(false);
                await editorTabInfo.MonacoEditorYaml.SetValue(editorTabInfo.Content).ConfigureAwait(false);
            }

            //add the tab if it didnt already exist
            EditorTabController.AddTab(editorTabInfo);

            //possibly reset the Menu
            if (currentTab == 0)
            {
                SetMenu();
            }

            //create the YamlFileEditor in the interface
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

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

        private async void OpenNotification(string message)
        {
            await JSRuntime.InvokeAsync<object>("notify", new object[] { message }).ConfigureAwait(false);
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

            var content = await editorTabInfo.MonacoEditorYaml.GetValue().ConfigureAwait(false);
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

        private static void SetErrors(ref IEditorTabInfo editorTabInfo)
        {
            SetDeltaDecorationsFromExceptions(editorTabInfo.MonacoEditorYaml, editorTabInfo.Exceptions).ConfigureAwait(false);
        }

        public static async Task SetDeltaDecorationsFromExceptions(MonacoEditorYaml monacoEditorYaml, IEnumerable<FormattingException> formattingExceptions)
        {
            if (formattingExceptions == null)
            {
                return;
            }
            await monacoEditorYaml.ResetDeltaDecorations().ConfigureAwait(false);

            var deltaDecorations = new List<ModelDeltaDecoration>();
            foreach (var exception in formattingExceptions)
            {
                var message = exception.Message;
                var range = new BlazorMonaco.Bridge.Range()
                {
                    StartLineNumber = exception.DebugInfo.Start.Line,
                    StartColumn = exception.DebugInfo.Start.Col,
                    EndLineNumber = exception.DebugInfo.End.Line,
                    EndColumn = exception.DebugInfo.End.Col
                };

                deltaDecorations.Add(await DeltaDecorationHelper.BuildDeltaDecoration(monacoEditorYaml.MonacoEditor, range, message).ConfigureAwait(false));
            }

            await monacoEditorYaml.SetDeltaDecoration(deltaDecorations).ConfigureAwait(false);
        }

        private async void StartValidationSubmitCountdown(IEditorTabInfo editorTabInfo, string yaml, int? overrideTimeOut = null)
        {
            var type = editorTabInfo.Type;
            var formattingExceptions = await YamlValidationController.StartSubmitCountdown(type, yaml, overrideTimeOut).ConfigureAwait(false);
            editorTabInfo.Exceptions = formattingExceptions;
            SetErrors(ref editorTabInfo);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }

        private async Task ToggleModal(string modalId)
        {
            await JSRuntime.InvokeAsync<object>("toggleModal", modalId).ConfigureAwait(false);
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

        private async Task<string> WriteFile(IYamlFileInfo yamlFileInfo, string content)
        {
            var contentId = await FileRepository.WriteFile(yamlFileInfo.Type, yamlFileInfo.Name, content, yamlFileInfo.ContentId).ConfigureAwait(false);
            return contentId;
        }

        #endregion

        #endregion
    }
}