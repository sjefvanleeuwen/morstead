using System.Collections.Generic;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.Interface;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces
{
    public interface IOptionsFormElementData : IFormElementSingleValueDate
    {
        Dictionary<string, string> Options { get; set; }

        void DefineOptions(IExecutionResult result, IContentController contentController);
    }
}