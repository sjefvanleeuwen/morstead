using System;
using System.IO;
using System.Net.Http;
using Vs.Core.Extensions;
using Vs.Core.Layers.Enums;
using Vs.YamlEditor.Components.Controllers;

namespace Vs.YamlEditor.Components.Pages
{
    public partial class Editor
    {
        private string Url { get; set; } = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/Vs.VoorzieningenEnRegelingen.Core.TestData/YamlScripts/Zorgtoeslag5.yaml";
        private string Value { get; set; }

        
        private ValidationController _validationController;
        private string _selectedValue;

        private ValidationController ValidationController
        {
            get
            {
                if (_validationController == null)
                {
                    _validationController = new ValidationController();
                }
                return _validationController;
            }
        }

        private string SelectedValue { get => _selectedValue; set { _selectedValue = value; ValidationController.SelectedValue = value; } }

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

            await ValidationController.YamlEditor.SetValue(Value);
        }
    }
}
