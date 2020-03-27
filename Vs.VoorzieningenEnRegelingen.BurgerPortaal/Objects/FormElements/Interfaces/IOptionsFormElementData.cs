using System.Collections.Generic;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IOptionsFormElementData : IFormElementSingleValue
    {
        Dictionary<string, string> Options { get; set; }

        void DefineOptions(IExecutionResult result, IContentController contentController);
    }
}