using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IOptionsFormElementData : IFormElementData
    {
        Dictionary<string, string> Options { get; set; }

        void DefineOptions(IExecutionResult result);
    }
}