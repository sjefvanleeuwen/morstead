using BlazorMonaco.Bridge;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.YamlEditor.Components.Controllers.ApiCalls;

namespace Vs.YamlEditor.Components.Controllers.Interfaces
{
    public interface IValidationController
    {
        IDictionary<YamlType, bool> Types { get; }
        bool GetEnabledForType(YamlType type);
        Task<IEnumerable<FormattingException>> StartSubmitCountdown(string type, string yaml);
        Task<IEnumerable<FormattingException>> SubmitPage(string type, string yaml);
    }
}