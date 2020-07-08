using BlazorMonaco;
using BlazorMonaco.Bridge;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Vs.YamlEditor.Components.Controllers.ApiCalls;

namespace Vs.YamlEditor.Components.Shared
{
    public partial class YamlDiffEditor
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string CssClass { get; set; }
        [Parameter]
        public string OriginalValue { get; set; }
        [Parameter]
        public string ModifiedValue { get; set; }
        [Parameter]
        public EventCallback<KeyboardEvent> OnKeyUp { get; set; }
        [Parameter]
        public EventCallback OnDidInit { get; set; }

        private MonacoDiffEditor _monacoDiffEditor { get; set; }

        private const string _language = "yaml";
        private string OriginalEditorId { get; set; }
        private string ModifiedEditorId { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                OriginalEditorId = await _monacoDiffEditor.GetOriginalEditor();
                ModifiedEditorId = await _monacoDiffEditor.GetModifiedEditor();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private DiffEditorConstructionOptions DiffEditorConstructionOptions(MonacoDiffEditor editor)
        {
            return new DiffEditorConstructionOptions
            {
                AutomaticLayout = true,
                GlyphMargin = true
            };
        }

        private DiffEditorData DiffEditorData(MonacoDiffEditor editor)
        {
            return new DiffEditorData
            {
                Language = _language,
                OriginalValue = OriginalValue,
                ModifiedValue = ModifiedValue
            };
        }

        public async Task<string> GetOriginalValue()
        {
            return await _monacoDiffEditor.GetValue(OriginalEditorId);
        }

        public async Task SetOriginalValue(string value)
        {
            await _monacoDiffEditor.SetValue(OriginalEditorId, value);
        }

        public async Task<string> GetModifiedValue()
        {
            return await _monacoDiffEditor.GetValue(ModifiedEditorId);
        }

        public async Task SetModifiedValue(string value)
        {
            await _monacoDiffEditor.SetValue(ModifiedEditorId, value);
        }

        public async Task Layout()
        {
            await _monacoDiffEditor.Layout();
        }
    }
}
