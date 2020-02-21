using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerRepository.Objects.Interfaces
{
    public interface IResult
    {
        string Description { get; set; }
        string ResultTemplate { get; set; }
        Dictionary<string, string> TemplateVariables { set; }      
    }
}
