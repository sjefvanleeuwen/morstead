namespace Vs.VoorzieningenEnRegelingen.Site.Model.Tables.Interfaces
{
    public interface IDisplayItem
    {
        bool Display { get; set; }
        string Name { get; set; }
        string Value { get; set; }
    }
}