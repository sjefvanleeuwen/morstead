using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces
{
    public interface IEditorTabController
    {
        IDictionary<int, IEditorTabInfo> EditorTabInfos { get; set; }

        IEditorTabInfo GetTabByTabId(int tabId);

        IEditorTabInfo GetTabByContentId(string contentId);

        void AddTab(IEditorTabInfo editorTabInfo);

        bool RemoveTab(int tabId);
    }
}