using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vs.Core.Diagnostics;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    /// <summary>
    /// Parses YAML's to the internal model for further interpretation.
    /// For the debug info, the original line/col indexes to the Yaml file are preserved so a debugger script editor 
    /// can be implemented on the parsed model during interpretation.
    /// </summary>
    public class YamlParser
    {
        private const string FormulasAttribute = "formules";
        private const string FormulaAttribute = "formule";
        private const string SituationAttribute = "situatie";
        private const string TablesAttribute = "tabellen";
        private const string FlowAttribute = "berekening";
        private const string HeaderAttribute = "stuurinformatie";

        private static ConcurrentDictionary<string, YamlMappingNode> Maps = new ConcurrentDictionary<string, YamlMappingNode>();
        private readonly Dictionary<string, Parameter> _parameters;
        private readonly string _yaml;
        private readonly YamlMappingNode map;

        public YamlParser(string yaml, Dictionary<string,Parameter> parameters)
        {
            _parameters = parameters;
            _yaml = yaml;
            map = Map(_yaml);
        }

       public StuurInformatie Header()
        {
            var stuurinformatie = new StuurInformatie();
            foreach (var item in ((YamlMappingNode)map.Children[new YamlScalarNode(HeaderAttribute)]).Children)
            {
                switch (item.Key.ToString())
                {
                    case "onderwerp":
                        stuurinformatie.Onderwerp = item.Value.ToString();
                        break;
                    case "organisatie":
                        stuurinformatie.Organisatie = item.Value.ToString();
                        break;
                    case "type":
                        stuurinformatie.Type = item.Value.ToString();
                        break;
                    case "domein":
                        stuurinformatie.Domein = item.Value.ToString();
                        break;
                    case "versie":
                        stuurinformatie.Versie = item.Value.ToString();
                        break;
                    case "status":
                        stuurinformatie.Status = item.Value.ToString();
                        break;
                    case "jaar":
                        stuurinformatie.Jaar = item.Value.ToString();
                        break;
                    case "bron":
                        stuurinformatie.Bron = item.Value.ToString();
                        break;
                    default:
                        throw new Exception($"unknown header identifider {item.Key.ToString()}");
                }
            }
            return stuurinformatie;
        }

        public List<Step> Flow()
        {
            var steps = new List<Step>();
            int key = 0;
            foreach (var step in (YamlSequenceNode)map.Children[new YamlScalarNode(FlowAttribute)])
            {
                var debugInfoStep = DebugInfo.MapDebugInfo(step.Start, step.End);
                string stepid ="", description="", formula = "", situation="", @break="";
                foreach (var stepInfo in ((YamlMappingNode)step).Children)
                {
                    switch (stepInfo.Key.ToString())
                    {
                        case "stap":
                            stepid = stepInfo.Value.ToString();
                            break;
                        case "omschrijving":
                            description = stepInfo.Value.ToString();
                            break;
                        case "situatie":
                            situation = stepInfo.Value.ToString();
                            break;
                        case "formule":
                            formula = stepInfo.Value.ToString();
                            break;
                        case "recht":
                            @break = stepInfo.Value.ToString();
                            break;
                        default:
                            throw new Exception($"unknown step identifider {stepInfo.Key.ToString()}");
                    }
                }
                steps.Add(new Step(key++, stepid, description, formula, situation, @break));
            }
            return steps;
        }

        public List<Table> Tabellen()
        {
            var tables = new List<Table>();
            if (!map.Children.ContainsKey(new YamlScalarNode(TablesAttribute)))
                return tables;
            if (((YamlNode)map.Children[new YamlScalarNode(TablesAttribute)]).ToString() == string.Empty)
                return tables;
            foreach (var tabel in (YamlSequenceNode)map.Children[new YamlScalarNode(TablesAttribute)])
            {
                var debugInfoTable = DebugInfo.MapDebugInfo(tabel.Start, tabel.End);
                var debugInfo = new DebugInfo(
                    start: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index),
                    end: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index)
                );
                var tableName = ((YamlMappingNode)tabel).ElementAt(0).Value.ToString();
                var columns1 = ((YamlMappingNode)tabel).ElementAt(1).Key.ToString().Split(',').Select(sValue => sValue.Trim()).ToArray();
                var rows = new List<Row>();
                var columnsDebugInfo = DebugInfo.MapDebugInfo(((YamlMappingNode)tabel).ElementAt(1).Key.Start, ((YamlMappingNode)tabel).ElementAt(1).Key.End);
                foreach (var row in (YamlSequenceNode)(((YamlMappingNode)tabel).ElementAt(1).Value))
                {
                    var rowdebugInfo = DebugInfo.MapDebugInfo(row.Start, row.End);
                    var columns = new List<Column>();
                    foreach (var column in (YamlSequenceNode)row)
                    {
                        var info = DebugInfo.MapDebugInfo(column.Start, column.End);
                        columns.Add(new Column(info, ((YamlScalarNode)column).Value));
                    }
                    rows.Add(new Row(rowdebugInfo, columns));
                }
                var columnTypes = new List<ColumnType>();
                for (int i = 0; i < columns1.Length; i++)
                {
                    columnTypes.Add(new ColumnType(columnsDebugInfo, columns1[i]));
                }
                tables.Add(new Table(debugInfoTable, tableName, columnTypes, rows));
            }
            return tables;
        }

        public static YamlMappingNode Map(string body)
        {
            if (Maps.ContainsKey(body))
                return Maps[body];
            using (var input = new StringReader(body))
            {
                // Load the stream
                var yaml = new YamlStream();
                yaml.Load(input);
                // Examine the stream
                Maps.TryAdd(body, (YamlMappingNode)yaml.Documents[0].RootNode);
                return (YamlMappingNode)yaml.Documents[0].RootNode;
            }
        }

        public List<Formula> Formulas()
        {
            var funcs = new List<Formula>();
            foreach (var tabel in (YamlSequenceNode)map.Children[new YamlScalarNode(FormulasAttribute)])
            {
                foreach (var row in ((YamlMappingNode)tabel).Children)
                {
                    var variableName = row.Key;
                    List<Function> functions = new List<Function>();
                    if (row.Value.GetType() == typeof(YamlSequenceNode))
                    {
                        foreach (var situation in ((YamlSequenceNode)row.Value).Children)
                        {
                            var f = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                            var s = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == SituationAttribute).Value;
                            var function = new Function(DebugInfo.MapDebugInfo(f.Start,f.End), s.ToString(), f.ToString().Replace("'","\""));
                            functions.Add(function);
                        }
                    }
                    else
                    {
                        var f = ((YamlMappingNode)row.Value).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                        var function = new Function(DebugInfo.MapDebugInfo(f.Start, f.End), f.ToString().Replace("'", "\""));
                        functions.Add(function);
                    }
                    funcs.Add(new Formula(DebugInfo.MapDebugInfo(variableName.Start, variableName.End), variableName.ToString(), functions));
                }
            }
            return funcs;
        }
    }
}
