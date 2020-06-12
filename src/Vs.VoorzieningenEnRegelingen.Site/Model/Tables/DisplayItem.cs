using Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables
{
    public class DisplayItem : IDisplayItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Display { get; set; }
    }
}