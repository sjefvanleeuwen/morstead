using BlazorMonaco;
using BlazorMonaco.Bridge;

namespace Vs.YamlEditor.Components.Controllers.Interfaces
{
    public interface IMonacoController
    {
        string Language { get; set; }
        MonacoEditor MonacoEditor { get; set; }

        void ResetDeltaDecorations();
        void SetDeltaDecorations(Range range, ModelDecorationOptions options);
        void SetDeltaDecorations(ModelDeltaDecoration[] deltaDecorations);
    }
}