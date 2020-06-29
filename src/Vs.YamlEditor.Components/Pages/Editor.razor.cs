using Microsoft.AspNetCore.Components;
using System;
using System.IO;
using System.Net.Http;
using Vs.Core.Extensions;
using Vs.Core.Layers.Enums;
using Vs.YamlEditor.Components.Controllers.Interfaces;
using Vs.YamlEditor.Components.Helpers;

namespace Vs.YamlEditor.Components.Pages
{
    public partial class Editor
    {
        [Inject]
        public IValidationController ValidationController { get; set; }

        private string Url { get; set; } = "https://raw.githubusercontent.com/sjefvanleeuwen/morstead/master/src/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/YamlZorgtoeslag5.yaml";
        private string Value { get; set; }
        private string Type { get; set; }

        Shared.YamlEditor YamlEditor { get; set; }       

        private string GetStyleForType(YamlType type)
        {
            if (ValidationController.GetEnabledForType(type))
            {
                return string.Empty;
            }

            return "disabled";
        }

        private string GetDescription(YamlType yamlType)
        {
            return yamlType.GetDescription();
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
            await YamlEditor.SetValue(Value);
        }

        public async void StartSubmitCountdown()
        {
            var yaml = await YamlEditor.GetValue();
            var formattingExceptions = await ValidationController.StartSubmitCountdown(Type, yaml);
            await DeltaDecorationHelper.SetDeltaDecorationsFromExceptions(YamlEditor, formattingExceptions).ConfigureAwait(false);
        }

        public async void SubmitPage()
        {
            var yaml = await YamlEditor.GetValue();
            var formattingExceptions = await ValidationController.SubmitPage(Type, yaml);
            await DeltaDecorationHelper.SetDeltaDecorationsFromExceptions(YamlEditor, formattingExceptions).ConfigureAwait(false);
        }
    }
}
