namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface IMenuItem
    {
        bool IsDivider { get; set; }
        string Link { get; set; }
        string Name { get; set; }
        System.Collections.Generic.IEnumerable<IMenuItem> SubMenuItems { get; set; }
    }
}