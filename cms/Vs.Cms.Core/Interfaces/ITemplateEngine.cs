namespace Vs.Cms.Core.Interfaces
{
    public interface ITemplateEngine
    {
        string Render(string template, dynamic model);
    }
}