namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IYamlFileInfo
    {
        string Id { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string Content { get; set; }
    }
}