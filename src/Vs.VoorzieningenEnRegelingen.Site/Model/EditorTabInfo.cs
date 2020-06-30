using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class EditorTabInfo : YamlFileInfo, IEditorTabInfo
    {
        public int TabId { get; set; }
        public int OrderNr { get; set; }
        public bool IsOpen { get; set; }
        public bool IsActive { get; set; }
        public bool HasChanges => Content != OriginalContent;
        public bool IsSaved { get; set; }
        public string DisplayName =>
            (string.IsNullOrWhiteSpace(Name) ? "<i>Nieuw</i>" : Name) +
            (HasChanges && !IsSaved ? "*" : string.Empty);
        public string OriginalContent { get; set; }
        public string Content { get; set; }
        public string EditorId => "monacoEditor" + TabId;
        public string TabName => "Tab" + TabId;
        public YamlEditor.Components.Shared.YamlEditor YamlEditor { get; set; }
    }
}
