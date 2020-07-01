using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Controllers
{
    public class EditorTabController : IEditorTabController
    {
        public IDictionary<int, IEditorTabInfo> EditorTabInfos { get; set; } = new Dictionary<int, IEditorTabInfo>();

        /// <summary>
        /// Throws an error if it doesnt exist
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public IEditorTabInfo GetTabByTabId(int tabId)
        {
            if (!EditorTabInfos.ContainsKey(tabId))
            {
                throw new IndexOutOfRangeException("The id of the tab provided is unknown.");
            }

            return EditorTabInfos[tabId];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns>null if not found</returns>
        public IEditorTabInfo GetTabByContentId(string contentId)
        {
            foreach (var editorTabInfo in EditorTabInfos)
            {
                if (editorTabInfo.Value.ContentId == contentId)
                {
                    return EditorTabInfos[editorTabInfo.Key];
                }
            }

            return null;
        }

        public void AddTab(IEditorTabInfo editorTabInfo)
        {
            if (editorTabInfo == null)
            {
                throw new ArgumentNullException("Can't add no info to a tab.");
            }
            editorTabInfo.TabId = editorTabInfo.TabId > 0 ? editorTabInfo.TabId  :
                (EditorTabInfos.Keys.Any() ? EditorTabInfos.Keys.Max() : 0) + 1;
            EditorTabInfos[editorTabInfo.TabId] = editorTabInfo;
            Activate(editorTabInfo.TabId);
        }

        public void Activate(int tabId)
        {
            EditorTabInfos.Values.ToList().ForEach(e => e.IsActive = false);
            if (tabId > 0)
            {
                EditorTabInfos[tabId].IsActive = true;
            }
        }

        public bool RemoveTab(int tabId)
        {
            return EditorTabInfos.Remove(tabId);
        }

        public int GetNextOrderNr()
        {
            return (EditorTabInfos?.Max(e => e.Value?.OrderNr) ?? 0) + 1;
        }
    }
}
