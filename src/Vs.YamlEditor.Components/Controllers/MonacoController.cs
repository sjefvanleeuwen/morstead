using BlazorMonaco;
using BlazorMonaco.Bridge;
using System;
using System.Threading.Tasks;
using Vs.YamlEditor.Components.Controllers.Interfaces;

namespace Vs.YamlEditor.Components.Controllers
{
    public class MonacoController : IMonacoController
    {
        public string Language { get; set; }
        public MonacoEditor MonacoEditor { get; set; }
        public string[] DeltaDecorationIds { get; set; }

        public async Task SetDeltaDecorations(BlazorMonaco.Bridge.Range range, ModelDecorationOptions options)
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }

            DeltaDecorationIds = await MonacoEditor.DeltaDecorations(DeltaDecorationIds ?? new string[] { }, new ModelDeltaDecoration[] { new ModelDeltaDecoration { Range = range, Options = options } });
        }

        public async Task SetDeltaDecorations(ModelDeltaDecoration[] deltaDecorations)
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }
            
            DeltaDecorationIds = await MonacoEditor.DeltaDecorations(DeltaDecorationIds ?? new string[] { }, deltaDecorations);
        }

        public async Task ResetDeltaDecorations()
        {
            DeltaDecorationIds = null;
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }
            await MonacoEditor.ResetDeltaDecorations();
        }
    }
}
