namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IEditorTabInfo : IYamlFileInfo
    {
        int TabId { get; set; }
        int OrderNr { get; set; }
        bool IsVisible { get; set; }
        bool IsActive { get; set; }
        bool HasChanges { get; }
        bool HasErrors { get; set; }
        bool IsSaved { get; set; }
        string DisplayName { get; }
        string OriginalContent { get; set; }
        string Content { get; set; }
        string EditorId { get; }
        string TabName { get; }
        YamlEditor.Components.Shared.YamlEditor YamlEditor { get; set; }
    }
}