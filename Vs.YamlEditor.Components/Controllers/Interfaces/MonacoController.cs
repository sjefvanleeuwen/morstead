using BlazorMonaco;
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

        public async void SetDeltaDecorations(BlazorMonaco.Bridge.Range range, object options)
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }
            //TODO to enable after BlazorMonaco Update
            //await MonacoEditor.SetDeltaDecoration(range, options);
        }

        public async void ResetDeltaDecorations()
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("MonacoEditor has not been set");
            }
            //TODO to enable after BlazorMonaco Update
            //await MonacoEditor.ResetDeltaDecorations();
        }

        public async void SetHoverText(BlazorMonaco.Bridge.Range range, string title, string content)
        {
            if (MonacoEditor == null)
            {
                throw new NullReferenceException("Language has not been set.");
            }
            await _jsRuntime.InvokeVoidAsync("setHoverText", Language, range, title, content);
        }

        public async void ResetHoverTexts()
        {
            await _jsRuntime.InvokeVoidAsync("resetHoverTexts");
        }
    }
}
