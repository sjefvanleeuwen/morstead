using BlazorMonaco;
using BlazorMonaco.Bridge;
using Microsoft.JSInterop;
using System;
using Vs.YamlEditor.Components.Controllers.Interfaces;

namespace Vs.YamlEditor.Components.Controllers
{
    public class MonacoController : IMonacoController
    {
        private readonly IJSRuntime _jsRuntime;

        public string Language { get; set; }
        public MonacoEditor MonacoEditor { get; set; }

        public MonacoController(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async void SetDeltaDecorations(BlazorMonaco.Bridge.Range range, DecorationOptions options)
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }

            await MonacoEditor.SetDeltaDecoration(range, options);
        }

        public async void ResetDeltaDecorations()
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }
            await MonacoEditor.ResetDeltaDecorations();
        }
    }
}
