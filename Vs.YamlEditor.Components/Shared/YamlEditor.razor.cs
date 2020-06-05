using BlazorMonaco;
using BlazorMonaco.Bridge;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.YamlEditor.Components.Controllers.ApiCalls;
using Vs.YamlEditor.Components.Controllers.Interfaces;

namespace Vs.YamlEditor.Components.Shared
{
    public partial class YamlEditor
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string CssClass { get; set; }
        [Parameter]
        public EventCallback<KeyboardEvent> OnKeyUp { get; set; }

        [Inject]
        public IMonacoController MonacoController { get; set; }

        private MonacoEditor _monacoEditor { get; set; }

        private const string _language = "yaml";

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                MonacoController.Language = _language;
                MonacoController.MonacoEditor = _monacoEditor;
            }
            base.OnAfterRenderAsync(firstRender);
        }

        private StandaloneEditorConstructionOptions EditorConstructionOptions(MonacoEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = _language,
                GlyphMargin = true
            };
        }

        public async Task SetDeltaDecoration(IEnumerable<ModelDeltaDecoration> deltaDecorations)
        {
            await MonacoController.SetDeltaDecorations(deltaDecorations.ToArray());
        }

        public async Task ResetDeltaDecorations()
        {
            await MonacoController.ResetDeltaDecorations();
        }

        public async Task SetValue(string value)
        {
            await _monacoEditor.SetValue(value);
        }

        public async Task<string> GetValue()
        {
            return await _monacoEditor.GetValue();
        }
    }
}
