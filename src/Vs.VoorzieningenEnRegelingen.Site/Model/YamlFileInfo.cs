using Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class YamlFileInfo : IYamlFileInfo
    {
        public string ContentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
