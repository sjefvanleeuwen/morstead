using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IFormElementMultipleValueDate : IFormElementData
    {
        IDictionary<string, string> Values { get; set; }
        IDictionary<string, FormElementLabel> Labels { get; set; }

        string GetLabelTitle(string key);
        string GetLabelText(string key);
    }
}