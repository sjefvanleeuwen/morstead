using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFormElementMultipleValue
    {
        Dictionary<string, string> Values { get; set; }
    }
}