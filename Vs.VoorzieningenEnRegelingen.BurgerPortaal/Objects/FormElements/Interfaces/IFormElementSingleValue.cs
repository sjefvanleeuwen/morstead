using Microsoft.AspNetCore.Components;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFormElementSingleValue : IFormElementData
    {
        EventCallback<string> ValueChanged { get; set; }
    }
}
