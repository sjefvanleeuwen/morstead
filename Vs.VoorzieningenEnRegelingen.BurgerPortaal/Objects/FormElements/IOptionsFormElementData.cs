using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public interface IOptionsFormElementData : IFormElementData
    {
        Dictionary<string, string> Options { get; set; }
    }
}