using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Vs.Core.Extensions;
using Vs.Core.Layers.Enums;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared.Components
{
    public partial class YamlTypeSelector
    {
        [Parameter]
        public IEnumerable<string> EnabledTypes { get; set; } = new List<string>();

        public string SelectedValue { get; set; }

        private IDictionary<string, bool> _types;

        private IDictionary<string, bool> GetTypeDefinitions()
        {
            if (_types != null)
            {
                return _types;
            }

            _types = new Dictionary<string, bool>();

            var availableValidation = new List<YamlType> { YamlType.Rules };

            foreach (var yamlType in (YamlType[])Enum.GetValues(typeof(YamlType)))
            {
                _types.Add(yamlType.GetDescription(), availableValidation.Contains(yamlType));
            }
            return _types;
        }

        private static string GetStyleForType(bool enabled)
        {
            if (enabled)
            {
                return string.Empty;
            }

            return "disabled";
        }
    }
}
