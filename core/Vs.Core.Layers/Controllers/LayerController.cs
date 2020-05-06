using Nager.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Formats.Yaml.Helper;
using Vs.Core.Layers.Exceptions;
using Vs.Core.Layers.Model;
using Vs.Core.Layers.Model.Interfaces;

namespace Vs.Core.Layers.Controllers
{
    public class LayerController
    {
        private const string _version = "version";
        private const string _layers = "layers";
        private const string _context = "context";
        private const string _language = "language";
        private static IEnumerable<string> LayerConfigurationRequiredKeys { get; } = new List<string> { _version, _layers };
        private static IEnumerable<string> LayerRequiredKeys { get; } = new List<string> { };
        private static IEnumerable<string> ContextRequiredKeys { get; } = new List<string> { _context };

        public ILayerConfiguration LayerConfiguration { get; set; }

        public LayerController(Uri layerUri)
        {
            Initialize(layerUri.ToString());
        }

        public LayerController(string layerYaml)
        {
            Initialize(layerYaml);
        }

        private void Initialize(string layerYaml)
        {
            var parsedResult = YamlParser.RenderContentYamlToObject(layerYaml);
            LayerConfiguration = TranslateParsedYamlToLayerConfiguration(parsedResult);
        }

        private static ILayerConfiguration TranslateParsedYamlToLayerConfiguration(IDictionary<string, object> parsedResult)
        {
            var layerConfiguration = new LayerConfiguration();
            ValidateRequiredKeysPresent(parsedResult.Keys, LayerConfigurationRequiredKeys);
            layerConfiguration.Version = parsedResult[_version].ToString();

            if (!(parsedResult[_layers] is IDictionary<string, object> layers))
            {
                throw new LayersYamlException("The parsed yaml does not have the correct type of content for layers");
            }

            layerConfiguration.Layers = TranslateParsedYamlToLayers(layers);
            return layerConfiguration;
        }

        private static IEnumerable<ILayer> TranslateParsedYamlToLayers(IDictionary<string, object> layers)
        {
            var result = new List<ILayer>();

            var keys = layers.Keys;
            ValidateRequiredKeysPresent(keys, LayerRequiredKeys);
            foreach (var key in keys)
            {
                if (!(layers[key] is IEnumerable<object> layer))
                {
                    throw new LayersYamlException("The parsed yaml does not have the correct type of content for layers");
                }

                result.Add(new Layer
                {
                    Name = key,
                    Contexts = TranslateParsedYamlToContexts(layer)
                });
            }

            return result;
        }

        private static IEnumerable<IContext> TranslateParsedYamlToContexts(IEnumerable<object> layer)
        {
            var result = new List<IContext>();

            foreach (var layerItem in layer) 
            {
                if (!(layerItem is IDictionary<string, object> context))
                {
                    throw new LayersYamlException("the parsed yaml does not have the correct type for all of the layers.");
                }

                result.Add(TranslateParsedYamlToContext(context));
            }

            return result;
        }

        private static IContext TranslateParsedYamlToContext(IDictionary<string, object> context)
        {
            var keys = context.Keys;
            ValidateRequiredKeysPresent(keys, ContextRequiredKeys);
            foreach (var key in keys)
            {
                if (!(context[key] is string value))
                {
                    throw new LayersYamlException("The parsed yaml does not have texts as values for the context.");
                }
            }

            try
            {
                return new Context
                {
                    Endpoint = new Uri(context[_context].ToString()),
                    Language = context.ContainsKey(_language) ?
                    (LanguageCode?)Enum.Parse(typeof(LanguageCode), context[_language].ToString(), true) :
                    null
                };
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is UriFormatException)
                {
                    throw new LayersYamlException($"The parsed yaml contains a value for {_context} that is not a value url.", ex);
                }
                else
                {
                    throw new LayersYamlException($"The language provided ({context[_language]}) is unknown", ex);
                }
            }
        }

        private static void ValidateRequiredKeysPresent(IEnumerable<string> present, IEnumerable<string> required)
        {
            var missing = required.Except(present);
            if (!missing.Any())
            {
                return;
            }

            throw new LayersYamlException(
                $"The parsed yaml content did not contain all required information: Missing: '{string.Join("', '", missing)}'");
        }
    }
}
