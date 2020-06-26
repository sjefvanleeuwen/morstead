namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IEditorTabInfo : IYamlFileInfo
    {
        int TabId { get; set; }
        bool IsOpen { get; set; }
        string Content { get; set; }
        string EditorId { get; }
        YamlEditor.Components.Shared.YamlEditor YamlEditor { get; set; }
    }
}