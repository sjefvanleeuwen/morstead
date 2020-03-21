using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements
{
    public interface IUnused
    {
        IEnumerable<FormElementLabel> Labels { get; set; }
    }
}