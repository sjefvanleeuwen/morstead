using System.Collections.Generic;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Rules.Core.Interfaces;

namespace Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces
{
    public interface IOptionsFormElementData : IFormElementData
    {
        Dictionary<string, string> Options { get; set; }

        void DefineOptions(IExecutionResult result, IContentController contentController);
    }
}