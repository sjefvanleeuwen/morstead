using BlazorMonaco;
using BlazorMonaco.Bridge;
using System.Net;

namespace Vs.YamlEditor.Components.Pages
{
    public partial class Editor
    {
        private string Url { get; set; } = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5.yaml";
        private string Value { get; set; }

        private MonacoEditor _editor { get; set; }

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

            await _editor.SetValue(Value);
        }

        private StandaloneEditorConstructionOptions EditorConstructionOptions(MonacoEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "yaml"
            };
        }
    }
}
