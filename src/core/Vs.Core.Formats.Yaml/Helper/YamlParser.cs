﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Vs.Core.Formats.Yaml.Helper
{
    //TODO Refactor with IYamlConvertible
    public static class YamlParser
    {
        static readonly ConcurrentDictionary<string, string> UrlContentCache = new ConcurrentDictionary<string, string>();

        public static async Task<IDictionary<string, object>> RenderYamlToObject(string yaml)
        {
            yaml = await ParseHelper(yaml);
            YamlMappingNode node = Map(yaml);
            return RenderYamlMappingNodeToObject(node);
        }

        public static async Task<string> ParseHelper(string yaml)
        {
            if (yaml.StartsWith("http"))
            {
                if (UrlContentCache.ContainsKey(yaml))
                {
                    return UrlContentCache[yaml];
                }
                using var client = new HttpClient();

                using var response = await client.GetAsync(yaml);
                using var streamToReadFrom = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(streamToReadFrom);
                return streamReader.ReadToEnd();
            }
            return yaml;
        }

        private static YamlMappingNode Map(string body)
        {
            using (var input = new StringReader(body))
            {
                // Load the stream
                var yaml = new YamlStream();
                yaml.Load(input);
                //return the rootnode
                return (YamlMappingNode)yaml.Documents[0].RootNode;
            }
        }

        private static object RenderYamlNodeToObject(YamlNode node)
        {
            if (node is YamlMappingNode)
            {
                return RenderYamlMappingNodeToObject(node as YamlMappingNode);
            }
            if (node is YamlSequenceNode)
            {
                return RenderYamlSequenceNodeToObject(node as YamlSequenceNode);
            }
            if (node is YamlScalarNode)
            {
                return node.ToString();
            }
            throw new ArgumentOutOfRangeException($"The YamlNode provided is not defined to be processed, type: {node.GetType()}");
        }

        private static Dictionary<string, object> RenderYamlMappingNodeToObject(YamlMappingNode node)
        {
            var result = new Dictionary<string, object>();
            foreach (var item in node.Children)
            {
                result.Add(item.Key.ToString().ToLower(), RenderYamlNodeToObject(item.Value));
            }
            return result;
        }

        private static List<object> RenderYamlSequenceNodeToObject(YamlSequenceNode node)
        {
            var result = new List<object>();
            foreach (var item in node.Children)
            {
                result.Add(RenderYamlNodeToObject(item));
            }
            return result;
        }
    }
}
