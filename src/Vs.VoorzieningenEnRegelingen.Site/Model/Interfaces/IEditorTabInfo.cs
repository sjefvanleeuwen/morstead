namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IEditorTabInfo : IYamlFileInfo
    {
        int TabId { get; set; }
        bool IsOpen { get; set; }
        bool IsActive { get; set; }
        string DisplayName { get; }
        string Content { get; set; }
        string EditorId { get; }
        string TabName { get; }
        YamlEditor.Components.Shared.YamlEditor YamlEditor { get; set; }
    }
}