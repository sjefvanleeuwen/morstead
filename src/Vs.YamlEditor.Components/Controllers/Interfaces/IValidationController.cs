using BlazorMonaco.Bridge;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;
using Vs.YamlEditor.Components.Controllers.ApiCalls;

namespace Vs.YamlEditor.Components.Controllers.Interfaces
{
    public interface IValidationController
    {
        string SelectedValue { get; set; }
        IDictionary<YamlType, bool> Types { get; }
        Shared.YamlEditor YamlEditor { get; set; }

        Task<ModelDeltaDecoration> BuildDeltaDecoration(Range range, string message);
        bool GetEnabledForType(YamlType type);
        Task SetDeltaDecorationsFromExceptions(IEnumerable<FormattingException> formattingExceptions);
        void StartSubmitCountdown();
        void SubmitPage();
    }
}