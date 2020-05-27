using BlazorMonaco;
using BlazorMonaco.Bridge;
using System.Collections.Generic;
using System.Net;

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
            using (WebClient client = new WebClient())
            {
                try
                {
                    Value = client.DownloadString(Url);
                }
                catch
                {
                    Value = "Laden mislukt. Klopt de url?";
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
            var range = new Range { StartLineNumber = 25, StartColumn = 1, EndLineNumber = 60, EndColumn = 126 };
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
