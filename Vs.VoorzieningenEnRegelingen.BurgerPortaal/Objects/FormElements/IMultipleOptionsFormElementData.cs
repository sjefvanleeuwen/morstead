using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public interface IMultipleOptionsFormElementData : IFormElementData
    {
        Dictionary<string, string> Options { get; set; }
    }
}