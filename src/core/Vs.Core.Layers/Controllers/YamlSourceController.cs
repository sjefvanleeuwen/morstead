﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;
using Vs.Core.Layers.Exceptions;

namespace Vs.Core.Layers.Controllers
{
    public class YamlSourceController : IYamlSourceController
    {
        private readonly IList<YamlInformation> _yamlInformations = new List<YamlInformation>();

        private readonly ILayerController _layerController;

        public YamlSourceController(ILayerController layerController)
        {
            _layerController = layerController;
        }

        public async Task SetYaml(YamlType yamlType, string yaml, Dictionary<string, object> filter = null)
        {
            if (yaml.StartsWith("http"))
            {
                await SetUri(yamlType, yaml, filter);
                return;
            }
            await SetString(yamlType, yaml, filter);
        }

        private async Task SetUri(YamlType yamlType, string yamlUri, Dictionary<string, object> filter = null, bool directlySet = true)
        {
            try
            {
                var uri = new Uri(yamlUri);

                await AttachYamlInformation(new YamlInformation
                {
                    YamlType = yamlType,
                    YamlUri = uri,
                    Filter = filter,
                    DirectlySet = directlySet
                });
            }
            catch (Exception ex)
            {
                throw new LayersYamlException($"The value suppied '{yamlUri}' is not a valid Uri.", ex);
            }
        }

        private async Task SetString(YamlType yamlType, string yaml, Dictionary<string, object> filter = null, bool directlySet = true)
        {
            await AttachYamlInformation(new YamlInformation
            {
                YamlType = yamlType,
                Yaml = yaml,
                Filter = filter,
                DirectlySet = directlySet
            });
        }

        private async Task AttachYamlInformation(YamlInformation yamlInformation)
        {
            var foundMatch = _yamlInformations.FirstOrDefault(y =>
                y.YamlType == yamlInformation.YamlType &&
                y.DirectlySet == yamlInformation.DirectlySet &&
                y.MatchesFilter(yamlInformation.Filter));
            if (foundMatch != null)
            {
                _yamlInformations.Remove(foundMatch);
            }
            _yamlInformations.Add(yamlInformation);

            if (yamlInformation.YamlType == YamlType.Layer)
            {
                await ParseLayerInformation(yamlInformation.Yaml ?? yamlInformation.YamlUri.ToString());
            }
        }

        private async Task ParseLayerInformation(string yaml)
        {
            await _layerController.Initialize(yaml);
            var layerConfiguration = _layerController.LayerConfiguration;
            foreach (var layer in layerConfiguration.Layers)
            {
                if (!Enum.TryParse(layer.Name, true, out YamlType yamlType))
                {
                    throw new LayersYamlException($"The provided type '{layer.Name}' is unknown.");
                }
                foreach (var context in layer.Contexts)
                {
                    await AttachYamlInformation(new YamlInformation
                    {
                        YamlType = yamlType,
                        YamlUri = context.Endpoint,
                        //TODO should be done by reflection
                        Filter = context.Language == null ? null : new Dictionary<string, object> { { "Language", context.Language } },
                        DirectlySet = false
                    });
                }
            }
        }

        public string GetYaml(YamlType yamlType, Dictionary<string, object> filter = null)
        {
            string result = null;

            var candidates = _yamlInformations.Where(y => y.YamlType == yamlType);
            if (filter != null)
            {
                candidates = candidates.Where(y => y.MatchesFilter(filter));
            }
            else
            {
                var candidatesWithoutFilter = candidates.Where(y => y.Filter == null);
                if (candidatesWithoutFilter.Any())
                {
                    candidates = candidatesWithoutFilter;
                }
            }

            if (candidates.Any())
            {
                result =
                    candidates.FirstOrDefault(y => y.Yaml != null && y.DirectlySet)?.Yaml ??
                    candidates.FirstOrDefault(y => y.YamlUri != null && y.DirectlySet)?.YamlUri.ToString() ??
                    candidates.FirstOrDefault(y => y.YamlUri != null && !y.DirectlySet)?.YamlUri.ToString();
            }

            if (result == null)
            {
                throw new LayersYamlException($"The requested yaml was not found");
            }

            return result;
        }

        private class YamlInformation
        {
            public YamlType YamlType;
            public string Yaml;
            public Uri YamlUri;
            public bool DirectlySet;
            public Dictionary<string, object> Filter;

            public bool MatchesFilter(Dictionary<string, object> filterValues)
            {
                if (filterValues == null)
                {
                    if (Filter != null)
                    {
                        return false;
                    }
                    return true;
                }

                foreach (var filterValue in filterValues)
                {
                    if (Filter == null || !Filter.ContainsKey(filterValue.Key) || (!Filter[filterValue.Key]?.Equals(filterValue.Value) ?? false))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
