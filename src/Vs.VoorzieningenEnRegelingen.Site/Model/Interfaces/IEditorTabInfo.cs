namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IEditorTabInfo : IYamlFileInfo
    {
        int TabId { get; set; }
        int OrderNr { get; set; }
        bool IsOpen { get; set; }
        bool IsActive { get; set; }
        public bool HasChanges { get; }
        public bool IsSaved { get; set; }
        string DisplayName { get; }
        string OriginalContent { get; set; }
        string Content { get; set; }
        string EditorId { get; }
        string TabName { get; }
        YamlEditor.Components.Shared.YamlEditor YamlEditor { get; set; }
    }
}