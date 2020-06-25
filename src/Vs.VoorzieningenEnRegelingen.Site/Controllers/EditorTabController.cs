using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Site.Model;

namespace Vs.VoorzieningenEnRegelingen.Site.Controllers
{
    public class EditorTabController : IEditorTabController
    {
        public IDictionary<int, YamlFileInfo> YamlFileInfos { get; set; } = new Dictionary<int, YamlFileInfo>();

        public int AddTab(YamlFileInfo yamlFileInfo)
        {
            var tabId = (YamlFileInfos.Keys?.Max() ?? 0) + 1;
            YamlFileInfos[tabId] = yamlFileInfo;
            return tabId;
        }

        public bool RemoveTab(int tabId)
        {
            return YamlFileInfos.Remove(tabId);
        }
    }
}
