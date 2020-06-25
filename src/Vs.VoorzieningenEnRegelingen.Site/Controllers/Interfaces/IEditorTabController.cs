using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Site.Model;

namespace Vs.VoorzieningenEnRegelingen.Site.Controllers.Interfaces
{
    public interface IEditorTabController
    {
        IDictionary<int, YamlFileInfo> YamlFileInfos { get; set; }

        int AddTab(YamlFileInfo yamlFileInfo);

        bool RemoveTab(int tabId);
    }
}