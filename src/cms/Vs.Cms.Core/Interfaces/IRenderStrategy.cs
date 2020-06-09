namespace Vs.Cms.Core.Interfaces
{
    public interface IRenderStrategy
    {
        string Render(string template, dynamic model);
    }
}
