using BlazorMonaco;
using BlazorMonaco.Bridge;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Vs.YamlEditor.Components.Controllers.Interfaces;

namespace Vs.YamlEditor.Components.Pages
{
    public partial class Editor
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public IMonacoController MonacoController { get; set; }

        private string Url { get; set; } = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5.yaml";
        private readonly string _language = "yaml";
        private string Value { get; set; }
        private string TypeOfContent { get; set; }
        private readonly IDictionary<string, bool> _types = new Dictionary<string, bool> {
            { "Rule", true },
            { "Content", true },
            { "Routing", true },
            { "Layer", false }
        };

        private MonacoEditor _monacoEditor { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                MonacoController.Language = _language;
                MonacoController.MonacoEditor = _monacoEditor;
            }
            base.OnAfterRender(firstRender);
        }

        private StandaloneEditorConstructionOptions EditorConstructionOptions(MonacoEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = _language
            };
        }

        private bool GetEnabledForType(string type)
        {
            return _types.ContainsKey(type) && _types[type];
        }

        private string GetStyleForType(string type)
        {
            if (GetEnabledForType(type))
            {
                return string.Empty;
            }

            return "disabled";
        }

        private async void LoadUrl()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(Url);
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            Value = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Value = @$"Laden mislukt. Klopt de url?
Details:
{ex}";
                }
            }

            await _monacoEditor.SetValue(Value);
        }

        private void SubmitPage()
        {
            if (string.IsNullOrWhiteSpace(TypeOfContent))
            {
                return;
            }

            //handle submit
        }

        private void TestError()
        {
            var range = new BlazorMonaco.Bridge.Range { StartLineNumber = 25, StartColumn = 1, EndLineNumber = 60, EndColumn = 40 };
            var options = new { InlineClassName = "redDecoration" };
            //TODO to enable after BlazorMonaco Update
            MonacoController.SetDeltaDecorations(range, options);
            MonacoController.SetHoverText(range, "Foutmelding", "Dit is de foutmelding");
        }

        private void RemoveError()
        {
            MonacoController.ResetDeltaDecorations();
        }
    }
}
