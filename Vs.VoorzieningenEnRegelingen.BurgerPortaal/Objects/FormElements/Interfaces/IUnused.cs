using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IUnused
    {
        IEnumerable<FormElementLabel> Labels { get; set; }
    }
}