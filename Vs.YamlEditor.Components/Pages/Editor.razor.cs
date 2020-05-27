using BlazorMonaco;
using BlazorMonaco.Bridge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Vs.YamlEditor.Components.Pages
{
    public partial class Editor
    {
        private string Url { get; set; } = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5.yaml";
        private string Value { get; set; }
        private string TypeOfContent { get; set; }
        private readonly IDictionary<string, bool> _types = new Dictionary<string, bool> {
            { "Rule", true },
            { "Content", true },
            { "Routing", true },
            { "Layer", false }
        };

        private MonacoEditor _monacoEditor { get; set; }

        private StandaloneEditorConstructionOptions EditorConstructionOptions(MonacoEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "yaml"
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

        private async void TestError()
        {
            var range = new BlazorMonaco.Bridge.Range { StartLineNumber = 25, StartColumn = 1, EndLineNumber = 60, EndColumn = 126 };
            var options = new { InlineClassName = "redDecoration" };
            //to enable after BlazorMonaco Update
            //await _monacoEditor.SetDeltaDecoration(range, options);
        }

        private async void RemoveError()
        {
            //to enable after BlazorMonaco Update
            //await _monacoEditor.ResetDeltaDecorations();
        }
    }
}
