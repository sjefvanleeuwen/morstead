using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interface
{
    public interface IUnused
    {
        IEnumerable<FormElementLabel> Labels { get; set; }
    }
}