using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vs.Core.Diagnostics;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Helpers;
using Vs.Rules.Core.Model;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace Vs.Rules.Core
{
    /// <summary>
    /// Parses YAML's to the internal model for further interpretation.
    /// For the debug info, the original line/col indexes to the Yaml file are preserved so a debugger script editor 
    /// can be implemented on the parsed model during interpretation.
    /// </summary>
    public class YamlRuleParser
    {
        public const string FormulasAttribute = "formules";
        public const string FormulaAttribute = "formule";
        public const string SituationAttribute = "situatie";
        public const string TablesAttribute = "tabellen";
        public const string FlowAttribute = "berekening";
        public const string HeaderAttribute = "stuurinformatie";
        public const string HeaderSubject = "onderwerp";
        public const string HeaderOrganization = "organisatie";
        public const string HeaderType = "type";
        public const string HeaderDomain = "domein";
        public const string HeaderVersion = "versie";
        public const string HeaderStatus = "status";
        public const string HeaderYear = "jaar";
        public const string HeaderSource = "bron";
        public const string Step = "stap";
        public const string StepDescription = "omschrijving";
        public const string StepFormula = "formule";
        public const string StepValue = "waarde";
        public const string StepSituation = "situatie";
        public const string StepBreak = "recht";
        public const string StepChoice = "keuze";
        public const string EvaluateTable = "evalueer";
        private static ConcurrentDictionary<string, YamlMappingNode> Maps = new ConcurrentDictionary<string, YamlMappingNode>();
        private readonly Dictionary<string, Parameter> _parameters;
        private readonly string _yaml;
        private readonly YamlMappingNode map;

        public YamlRuleParser(string yaml, Dictionary<string, Parameter> parameters)
        {
            _parameters = parameters;
            _yaml = yaml;
            try
            {
                map = Map(_yaml);
            }
            catch (Exception ex)
            {
                throw new FormattingExceptionCollection("Invalid Yaml File", new List<FormattingException>() { new RootFormattingException("Invalid Yaml File",
                        DebugInfo.Default)});
            }
            var rootElements = new List<string>()
            {
                HeaderAttribute, FormulasAttribute, TablesAttribute, FlowAttribute
            };
            // FormattingExceptionCollection formattingExceptions = new FormattingExceptionCollection();
            List<FormattingException> exceptions = new List<FormattingException>();
            foreach (var child in map.Children)
            {
                if (!rootElements.Contains(child.Key.ToString()))
                {
                    exceptions.Add(new RootFormattingException($"Unexpected root property '{child.Key}:'",
                        new DebugInfo().MapDebugInfo(child.Key.Start, child.Key.End)));
                }
            }
            if (exceptions.Any())
            {
                throw new FormattingExceptionCollection("Your Yaml contains multiple exceptions.", exceptions);
            }
        }

        public StuurInformatie Header()
        {
            var stuurinformatie = new StuurInformatie();
            var node = map.Children.Where(p => p.Key.ToString() == HeaderAttribute);
            var notSet = new List<string>()
            {
                HeaderSubject,HeaderOrganization,HeaderType,HeaderDomain,HeaderVersion,HeaderStatus,HeaderYear,HeaderSource
            };
            if (node.Count() == 0)
            {
                throw new FlowFormattingException($"'{HeaderAttribute}:' section is undefined.", new DebugInfo().MapDebugInfo(new Mark(), new Mark()));
            }
            var debugInfo = new DebugInfo().MapDebugInfo(node.ElementAt(0).Key.Start, node.ElementAt(0).Key.End);
            YamlMappingNode seq;
            try
            {
                seq = ((YamlMappingNode)map.Children[new YamlScalarNode(HeaderAttribute)]);
            }
            catch (Exception)
            {
                throw new FlowFormattingException($"'{HeaderAttribute}:' is empty and expects the following mandatory properties {string.Join(',', notSet)}", debugInfo);
            }
            foreach (var item in seq.Children)
            {
                switch (item.Key.ToString())
                {
                    case HeaderSubject:
                        stuurinformatie.Onderwerp = item.Value.ToString();
                        notSet.Remove(HeaderSubject);
                        break;
                    case HeaderOrganization:
                        stuurinformatie.Organisatie = item.Value.ToString();
                        notSet.Remove(HeaderOrganization);
                        break;
                    case HeaderType:
                        stuurinformatie.Type = item.Value.ToString();
                        notSet.Remove(HeaderType);
                        break;
                    case HeaderDomain:
                        stuurinformatie.Domein = item.Value.ToString();
                        notSet.Remove(HeaderDomain);
                        break;
                    case HeaderVersion:
                        stuurinformatie.Versie = item.Value.ToString();
                        notSet.Remove(HeaderVersion);
                        break;
                    case HeaderStatus:
                        stuurinformatie.Status = item.Value.ToString();
                        notSet.Remove(HeaderStatus);
                        break;
                    case HeaderYear:
                        stuurinformatie.Jaar = item.Value.ToString();
                        notSet.Remove(HeaderYear);
                        break;
                    case HeaderSource:
                        stuurinformatie.Bron = item.Value.ToString();
                        notSet.Remove(HeaderSource);
                        break;
                    default:
                        throw new FlowFormattingException($"unknown property in {HeaderAttribute} definition: '{item.Key.ToString()}:'", new DebugInfo().MapDebugInfo(item.Key.Start, item.Key.End));
                }
            }
            // check if all items are set.
            /*
            var check = stuurinformatie.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string) &&
                string.IsNullOrEmpty((string)pi.GetValue(stuurinformatie)))
                .Select(p => p.Name)
            */
            if (notSet.Any())
            {
                var s = string.Join(", ", notSet);
                throw new HeaderFormattingException($"{HeaderAttribute} expects {s} fields to be set.", new DebugInfo().MapDebugInfo(node.ElementAt(0).Key.Start,node.ElementAt(0).Key.End));
            }

            return stuurinformatie;
        }

        public IEnumerable<Step> Flow()
        {
            var steps = new List<Step>();
            int key = 0;
            var node = map.Children.Where(p => p.Key.ToString() == FlowAttribute);
            if (node.Count() == 0)
            {
                throw new FlowFormattingException($"'{FlowAttribute}:' section is undefined.", new DebugInfo().MapDebugInfo(new Mark(), new Mark()));
            }
            var debugInfo = new DebugInfo().MapDebugInfo(node.ElementAt(0).Key.Start, node.ElementAt(0).Key.End);
            YamlSequenceNode seq;
            try
            {
                seq = (YamlSequenceNode)map.Children[new YamlScalarNode(FlowAttribute)];
            }
            catch (Exception)
            {
                throw new FlowFormattingException($"'{FlowAttribute}:' section expects at least 1 ' - {Step}:' sequence", debugInfo);
            }
            foreach (var step in (YamlSequenceNode)map.Children[new YamlScalarNode(FlowAttribute)])
            {
                var debugInfoStep = new DebugInfo().MapDebugInfo(step.Start, step.End);
                string stepid = "", description = "", formula = "", value = "", situation = "";
                var @break = null as IBreak;
                var evaluateTables = new List<IEvaluateTable>();
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
                        case EvaluateTable:
                            evaluateTables.Add(new EvaluateTable() { Name = stepInfo.Value.ToString() } );
                            break;
                        default:
                            throw new StepFormattingException($"unknown property in step definition: '{stepInfo.Key.ToString()}:'",new DebugInfo().MapDebugInfo(stepInfo.Key.Start, stepInfo.Key.End));
                    }
                }
                /* not mandatory 
                if (string.IsNullOrEmpty(description))
                    throw new FlowFormattingException($"'- {Step}: {stepid}' should define a '{StepDescription}:' property", debugInfo);
                */
                if (!string.IsNullOrEmpty(value) && choices != null)
                    throw new StepFormattingException($"Within section '{FlowAttribute}:', '- {Step}: {stepid}' section specifies '{StepValue}:' and '{StepChoice}:' but only 1 can be defined at a time.", debugInfo);
                steps.Add(new Step(debugInfoStep, key++, stepid, description, formula, value, situation, @break, choices, evaluateTables));
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
                    throw new StepFormattingException($"multiple step choice identifiders found; {choiceInfoItems.Count}", new DebugInfo().MapDebugInfo(node.Start,node.End));
                }
                var choiceInfoItem = choiceInfoItems.First();
                switch (choiceInfoItem.Key.ToString())
                {
                    case SituationAttribute:
                        result.Add(new Choice() { Situation = choiceInfoItem.Value.ToString() });
                        break;
                    default:
                        throw new StepFormattingException($"unknown step choice identifider {choiceInfoItem.Key.ToString()}",new DebugInfo().MapDebugInfo(choiceInfoItem.Key.Start, choiceInfoItem.Key.End));
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
                var debugInfoTable = new DebugInfo().MapDebugInfo(tabel.Start, tabel.End);
                var debugInfo = new DebugInfo(
                    start: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index),
                    end: new LineInfo(line: tabel.Start.Line, col: tabel.Start.Column, index: tabel.Start.Index)
                );
                var situations = new List<ISituation>();
                var tableName = ((YamlMappingNode)tabel).ElementAt(0).Value.ToString();
                int j = 1;
                if (((YamlMappingNode)tabel).ElementAt(1).Key.ToString() == SituationAttribute)
                {
                    foreach (var situation in ((YamlMappingNode)tabel).ElementAt(1).Value.ToString().Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray())
                    {
                        situations.Add(new Situation(situation));
                    }
                    j = 2;
                }
                var columns1 = ((YamlMappingNode)tabel).ElementAt(j).Key.ToString().Split(',').Select(sValue => sValue.Trim()).ToArray();
                var rows = new List<Row>();
                var columnsDebugInfo = new DebugInfo().MapDebugInfo(((YamlMappingNode)tabel).ElementAt(j).Key.Start, ((YamlMappingNode)tabel).ElementAt(j).Key.End);
                foreach (var row in (YamlSequenceNode)(((YamlMappingNode)tabel).ElementAt(j).Value))
                {
                    var rowdebugInfo = new DebugInfo().MapDebugInfo(row.Start, row.End);
                    var columns = new List<Column>();
                    foreach (var column in (YamlSequenceNode)row)
                    {
                        var info = new DebugInfo().MapDebugInfo(column.Start, column.End);
                        columns.Add(new Column(info, ((YamlScalarNode)column).Value));
                    }
                    rows.Add(new Row(rowdebugInfo, columns));
                }
                var columnTypes = new List<ColumnType>();
                for (int i = 0; i < columns1.Length; i++)
                {
                    columnTypes.Add(new ColumnType(columnsDebugInfo, columns1[i]));
                }
                tables.Add(new Table(debugInfoTable, tableName, columnTypes, rows, situations));
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
            YamlSequenceNode formulasNode = null;
            try
            {
                formulasNode = (YamlSequenceNode)map.Children[new YamlScalarNode(FormulasAttribute)];
            }
            catch (KeyNotFoundException ex)
            {
                // no formulas specified in yaml.
                return formulas;
            }
            foreach (var functionNode in formulasNode)
            {
                foreach (var row in ((YamlMappingNode)functionNode).Children)
                {
                    if (string.IsNullOrEmpty(row.Value.ToString()))
                    {
                        throw new FlowFormattingException($"'{FormulaAttribute}:' '- {row.Key}:' section is undefined.", new DebugInfo().MapDebugInfo(row.Key.Start, row.Key.End));
                    }
                    var variableName = row.Key;
                    var functions = new List<Function>();
                    if (row.Value.GetType() == typeof(YamlSequenceNode))
                    {
                        foreach (var situation in ((YamlSequenceNode)row.Value).Children)
                        {
                            var f = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                            var s = ((YamlMappingNode)situation).Children.FirstOrDefault(p => p.Key.ToString() == SituationAttribute).Value;
                            var function = new Function(new DebugInfo().MapDebugInfo(f.Start, f.End), s.ToString(), f.ToString().Replace("'", "\""));
                            functions.Add(function);
                        }
                    }
                    else
                    {
                        var f = ((YamlMappingNode)row.Value).Children.FirstOrDefault(p => p.Key.ToString() == FormulaAttribute).Value;
                        var function = new Function(new DebugInfo().MapDebugInfo(f.Start, f.End), f.ToString().Replace("'", "\""));
                        functions.Add(function);
                    }
                    formulas.Add(new Formula(new DebugInfo().MapDebugInfo(variableName.Start, variableName.End), variableName.ToString(), functions));
                }
            }
            return formulas;
        }
    }
}
