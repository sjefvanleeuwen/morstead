using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vs.Core.Diagnostics;
using Vs.VoorzieningenEnRegelingen.Core.Model;
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
        private const string HeaderSubject = "onderwerp";
        private const string HeaderOrganization = "organisatie";
        private const string HeaderType = "type";
        private const string HeaderDomain = "domein";
        private const string HeaderVersion = "versie";
        private const string HeaderStatus = "status";
        private const string HeaderYear = "jaar";
        private const string HeaderSource = "bron";
        private const string Step = "stap";
        private const string StepDescription = "omschrijving";
        private const string StepFormula = "formule";
        private const string StepValue = "waarde";
        private const string StepSituation = "situatie";
        private const string StepBreak = "recht";
        private const string StepChoice = "keuze";
        private static ConcurrentDictionary<string, YamlMappingNode> Maps = new ConcurrentDictionary<string, YamlMappingNode>();
        private readonly Dictionary<string, Parameter> _parameters;
        private readonly string _yaml;
        private readonly YamlMappingNode map;

        public YamlParser(string yaml, Dictionary<string, Parameter> parameters)
        {
            _parameters = parameters;
            _yaml = yaml;
            map = Map(_yaml);
        }

        private LineInfo _dummyLineInfo => new LineInfo(0, 0, 0);
        private DebugInfo _dummyDebugInfo => new DebugInfo(_dummyLineInfo, _dummyLineInfo);

        public StuurInformatie Header()
        {
            var stuurinformatie = new StuurInformatie();
            foreach (var item in ((YamlMappingNode)map.Children[new YamlScalarNode(HeaderAttribute)]).Children)
            {
                switch (item.Key.ToString())
                {
                    case HeaderSubject:
                        stuurinformatie.Onderwerp = item.Value.ToString();
                        break;
                    case HeaderOrganization:
                        stuurinformatie.Organisatie = item.Value.ToString();
                        break;
                    case HeaderType:
                        stuurinformatie.Type = item.Value.ToString();
                        break;
                    case HeaderDomain:
                        stuurinformatie.Domein = item.Value.ToString();
                        break;
                    case HeaderVersion:
                        stuurinformatie.Versie = item.Value.ToString();
                        break;
                    case HeaderStatus:
                        stuurinformatie.Status = item.Value.ToString();
                        break;
                    case HeaderYear:
                        stuurinformatie.Jaar = item.Value.ToString();
                        break;
                    case HeaderSource:
                        stuurinformatie.Bron = item.Value.ToString();
                        break;
                    default:
                        throw new Exception($"unknown header identifider {item.Key.ToString()}");
                }
            }
            return stuurinformatie;
        }

        public IEnumerable<Step> Flow()
        {
            var steps = new List<Step>();
            int key = 0;
            foreach (var step in (YamlSequenceNode)map.Children[new YamlScalarNode(FlowAttribute)])
            {
                var debugInfoStep = DebugInfo.MapDebugInfo(step.Start, step.End);
                string stepid = "", description = "", formula = "", value = "", situation = "";
                var @break = null as IBreak;
                IEnumerable<IChoice> choices = null;
                foreach (var stepInfo in ((YamlMappingNode)step).Children)
                {
                    switch (stepInfo.Key.ToString())
                    {
                        case Step:
                            stepid = stepInfo.Value.ToString();
                            break;
                        case StepDescription:
                            description = stepInfo.Value.ToString();
                            break;
                        case StepFormula:
                            formula = stepInfo.Value.ToString();
                            break;
                        case StepValue:
                            value = stepInfo.Value.ToString();
                            break;
                        case StepSituation:
                            situation = stepInfo.Value.ToString();
                            break;
                        case StepBreak:
                            @break = new Break() { Expression = stepInfo.Value.ToString() };
                            break;
                        case StepChoice:
                            choices = GetSituations(stepInfo.Value);
                            break;
                        default:
                            throw new Exception($"unknown step identifider {stepInfo.Key.ToString()}");
                    }
                }
                steps.Add(new Step(debugInfoStep,key++, stepid, description, formula, value, situation, @break, choices));
            }
            return steps;
        }

        private IEnumerable<IChoice> GetSituations(YamlNode node)
        {
            var result = new List<IChoice>();
            foreach (var choiceInfo in ((YamlSequenceNode)node).Children)
            {
                var choiceInfoItems = ((YamlMappingNode)choiceInfo).Children;
                if (choiceInfoItems.Count != 1)
                {
                    throw new Exception($"multiple step choice identifiders found; {choiceInfoItems.Count}");
                }
                var choiceInfoItem = choiceInfoItems.First();
                switch (choiceInfoItem.Key.ToString())
                {
                    case SituationAttribute:
                        result.Add(new Choice() { Situation = choiceInfoItem.Value.ToString() });
                        break;
                    default:
                        throw new Exception($"unknown step choice identifider {choiceInfoItem.Key.ToString()}");
                }
            }
            return result;
        }

        public IEnumerable<Table> Tabellen()
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

        public IEnumerable<Formula> Formulas()
        {
            var formulas = new List<Formula>();
            foreach (var tabel in (YamlSequenceNode)map.Children[new YamlScalarNode(FormulasAttribute)])
            {
                foreach (var row in ((YamlMappingNode)tabel).Children)
                {
                    var variableName = row.Key;
                    var functions = new List<Function>();
                    if (row.Value.GetType() == typeof(YamlSequenceNode))
                    {
                        foreach (var situation in ((YamlSequenceNode)row.Value).Children)
                        {
                            var f = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                            var s = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == SituationAttribute).Value;
                            var function = new Function(DebugInfo.MapDebugInfo(f.Start, f.End), s.ToString(), f.ToString().Replace("'", "\""));
                            functions.Add(function);
                        }
                    }
                    else
                    {
                        var f = ((YamlMappingNode)row.Value).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                        var function = new Function(DebugInfo.MapDebugInfo(f.Start, f.End), f.ToString().Replace("'", "\""));
                        functions.Add(function);
                    }
                    formulas.Add(new Formula(DebugInfo.MapDebugInfo(variableName.Start, variableName.End), variableName.ToString(), functions));
                }
            }
            return formulas;
        }
    }
}
