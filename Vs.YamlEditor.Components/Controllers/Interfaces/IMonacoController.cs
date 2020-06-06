using BlazorMonaco;
using BlazorMonaco.Bridge;
using System.Threading.Tasks;

namespace Vs.YamlEditor.Components.Controllers.Interfaces
{
    public interface IMonacoController
    {
        string Language { get; set; }
        MonacoEditor MonacoEditor { get; set; }

        Task ResetDeltaDecorations();
        Task SetDeltaDecorations(Range range, ModelDecorationOptions options);
        Task SetDeltaDecorations(ModelDeltaDecoration[] deltaDecorations);
    }
}