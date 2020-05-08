using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Linq;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;

namespace Vs.Core.Layers.Helpers
{
    public static class YamlSourceHelper
    {
        public static void SetDefaultYaml(IYamlSourceController yamlSourceController, string ruleYaml, string contentYaml = null, string routingYaml = null)
        {
            yamlSourceController.SetYaml(YamlType.Rules, ruleYaml);
            if (contentYaml != null)
            {
                yamlSourceController.SetYaml(YamlType.Uxcontent, contentYaml);
            }
            if (routingYaml != null)
            {
                yamlSourceController.SetYaml(YamlType.Routing, routingYaml);
            }
        }

        public static void SetAllYamlFromUri(IYamlSourceController yamlSourceController, Uri uri)
        {
            var ruleYaml = QueryHelpers.ParseQuery(uri.Query).TryGetValue("rules", out var paramRule) ? paramRule.First() : null;
            if (ruleYaml != null)
            {
                yamlSourceController.SetYaml(YamlType.Rules, ruleYaml);
            }
            var contentYaml = QueryHelpers.ParseQuery(uri.Query).TryGetValue("content", out var paramContent) ? paramContent.First() : null;
            if (contentYaml != null)
            {
                yamlSourceController.SetYaml(YamlType.Uxcontent, contentYaml);
            }
            var routingYaml = QueryHelpers.ParseQuery(uri.Query).TryGetValue("routing", out var paramRouting) ? paramRouting.First() : null;
            if (routingYaml != null)
            {
                yamlSourceController.SetYaml(YamlType.Routing, routingYaml);
            }
            var layerYaml = QueryHelpers.ParseQuery(uri.Query).TryGetValue("layers", out var paramLayers) ? paramLayers.First() : null;
            if (layerYaml != null)
            {
                yamlSourceController.SetYaml(YamlType.Layer, paramLayers);
            }
        }

    }
}
