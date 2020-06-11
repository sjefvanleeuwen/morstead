using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Extensions;
using Vs.Core.Layers.Enums;

namespace Vs.YamlEditor.Components.Shared
{
    public partial class YamlTypeSelector
    {
        [Parameter]
        public IEnumerable<YamlType> DisabledTypes { get; set; } = new List<YamlType>();

        public string SelectedValue { get; set; } = string.Empty;

        private IDictionary<YamlType, bool> _types;

        private IDictionary<YamlType, bool> GetTypeDefinitions()
        {
            if (_types != null)
            {
                return _types;
            }

            _types = new Dictionary<YamlType, bool>();

            foreach (var yamlType in (YamlType[])Enum.GetValues(typeof(YamlType)))
            {
                _types.Add(yamlType, !DisabledTypes.ToList().Contains(yamlType));
            }
            return _types;
        }

        private static string GetDescription(YamlType yamlType)
        {
            return yamlType.GetDescription();
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
