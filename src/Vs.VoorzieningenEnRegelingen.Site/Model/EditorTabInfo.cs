using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class EditorTabInfo : YamlFileInfo, IEditorTabInfo
    {
        public int TabId { get; set; }
        public bool IsOpen { get; set; }
        public string Content { get; set; }
        public string EditorId => "monacoEditor" + TabId;
        public YamlEditor.Components.Shared.YamlEditor YamlEditor { get; set; }
    }
}
